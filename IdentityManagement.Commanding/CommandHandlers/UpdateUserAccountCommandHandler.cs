using System;
using Affecto.IdentityManagement.Commanding.Commands;
using Affecto.IdentityManagement.Commanding.Validation;
using Affecto.IdentityManagement.Interfaces.Exceptions;
using Affecto.IdentityManagement.Store.Model;
using Affecto.Patterns.Cqrs;

namespace Affecto.IdentityManagement.Commanding.CommandHandlers
{
    internal class UpdateUserAccountCommandHandler : ICommandHandler<UpdateUserAccountCommand>
    {
        private readonly IDbRepository repository;
        private readonly IAuditTrailWriter auditTrail;

        public UpdateUserAccountCommandHandler(IDbRepository repository, IAuditTrailWriter auditTrail)
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

        public void Execute(UpdateUserAccountCommand command)
        {
            var specification = new UserIdAndNameSpecification();
            if (!specification.IsSatisfiedBy(command))
            {
                throw new ArgumentException(specification.GetReasonsForDissatisfactionSeparatedWithNewLine());
            }

            AccountType type = Map(command.Type);

            User user = repository.GetUser(command.UserId);
            Account account = repository.GetUserAccount(command.UserId, type);

            ThrowIfAccountNameIsAlreadyAssigned(type, account.Name, command.Name);

            account.Name = command.Name;

            repository.SaveChanges();

            auditTrail.AddEntry(command.UserId, user.Name, command.Name, "Käyttäjätunnus päivitetty");
        }

        private static AccountType Map(Interfaces.Model.AccountType type)
        {
            return (AccountType)Enum.Parse(typeof(AccountType), type.ToString());
        }

        private void ThrowIfAccountNameIsAlreadyAssigned(AccountType type, string name, string newName)
        {
            if (!name.Equals(newName, StringComparison.InvariantCultureIgnoreCase) && repository.AccountExists(type, newName))
            {
                throw new UserAccountAlreadyAssignedException(newName);
            }
        }
    }
}