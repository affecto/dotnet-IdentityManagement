using System;
using Affecto.IdentityManagement.Commanding.Commands;
using Affecto.IdentityManagement.Commanding.Validation;
using Affecto.IdentityManagement.Store.Model;
using Affecto.Patterns.Cqrs;

namespace Affecto.IdentityManagement.Commanding.CommandHandlers
{
    internal class RemoveUserAccountCommandHandler : ICommandHandler<RemoveUserAccountCommand>
    {
        private readonly IDbRepository repository;
        private readonly IAuditTrailWriter auditTrail;

        public RemoveUserAccountCommandHandler(IDbRepository repository, IAuditTrailWriter auditTrail)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            if (auditTrail == null)
            {
                throw new ArgumentNullException("auditTrail");
            }
            this.repository = repository;
            this.auditTrail = auditTrail;
        }

        public void Execute(RemoveUserAccountCommand command)
        {
            var specification = new UserIdAndNameSpecification();
            if (!specification.IsSatisfiedBy(command))
            {
                throw new ArgumentException(specification.GetReasonsForDissatisfactionSeparatedWithNewLine());
            }

            User user = repository.GetUser(command.UserId);
            AccountType type = Map(command.Type);
            if (user.HasAccount(type, command.Name))
            {
                user.RemoveUserAccount(type, command.Name);
                repository.SaveChanges();

                auditTrail.AddEntry(command.UserId, user.Name, command.Name, "Käyttäjätunnus poistettu");
            }
        }

        private static AccountType Map(Interfaces.Model.AccountType type)
        {
            return (AccountType)Enum.Parse(typeof(AccountType), type.ToString());
        }
    }
}