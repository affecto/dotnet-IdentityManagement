using System;
using Affecto.Authentication.Passwords;
using Affecto.IdentityManagement.Commanding.Commands;
using Affecto.IdentityManagement.Commanding.Validation;
using Affecto.IdentityManagement.Interfaces.Exceptions;
using Affecto.IdentityManagement.Store.Model;
using Affecto.Patterns.Cqrs;

namespace Affecto.IdentityManagement.Commanding.CommandHandlers
{
    internal class ChangeUserPasswordCommandHandler : ICommandHandler<ChangeUserPasswordCommand>
    {
        private readonly IDbRepository repository;
        private readonly IAuditTrailWriter auditTrail;

        public ChangeUserPasswordCommandHandler(IDbRepository repository, IAuditTrailWriter auditTrail)
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

        public void Execute(ChangeUserPasswordCommand command)
        {

            if (string.IsNullOrEmpty(command.Password))
            {
                throw new ArgumentException("Password cannot be changed without providing new password.");
            }

            User user = repository.GetUser(command.Id);
            Account account = repository.GetUserAccount(command.Id, AccountType.Password);
            var password = new Password(command.Password); // TODO: Validate against password policy
            account.Password = password.Hash();
            repository.SaveChanges();

            auditTrail.AddEntry(command.Id, user.Name, "Salasana vaihdettu");
        }

    }
}