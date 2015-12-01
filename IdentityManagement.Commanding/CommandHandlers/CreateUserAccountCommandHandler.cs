using System;
using Affecto.Authentication.Passwords;
using Affecto.IdentityManagement.Commanding.Commands;
using Affecto.IdentityManagement.Commanding.Validation;
using Affecto.IdentityManagement.Interfaces.Exceptions;
using Affecto.IdentityManagement.Store.Model;
using Affecto.Patterns.Cqrs;

namespace Affecto.IdentityManagement.Commanding.CommandHandlers
{
    internal class CreateUserAccountCommandHandler : ICommandHandler<CreateUserPasswordAccountCommand>, ICommandHandler<CreateExternalUserAccountCommand>
    {
        private readonly IDbRepository repository;
        private readonly IAuditTrailWriter auditTrail;

        public CreateUserAccountCommandHandler(IDbRepository repository, IAuditTrailWriter auditTrail)
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

        public void Execute(CreateUserPasswordAccountCommand command)
        {
            ValidateCommand(command);

            User user = repository.GetUser(command.UserId);
            ValidateAgainstExistingAccounts(user, Interfaces.Model.AccountType.Password, AccountType.Password, command.Name);

            // TODO: Validate against password policy

            var password = new Password(command.Password);

            repository.AddUserAccount(command.UserId, AccountType.Password, command.Name, password.Hash());
            repository.SaveChanges();

            AddAuditTrailEntry(command, user);
        }

        public void Execute(CreateExternalUserAccountCommand command)
        {
            if (command.Type == Interfaces.Model.AccountType.Password)
            {
                throw new ArgumentException("Account of type 'Password' cannot be created without providing password.");
            }

            ValidateCommand(command);
            AccountType mappedAccountType = Map(command.Type);
            User user = repository.GetUser(command.UserId);
            ValidateAgainstExistingAccounts(user, command.Type, mappedAccountType, command.Name);

            repository.AddUserAccount(command.UserId, mappedAccountType, command.Name);
            repository.SaveChanges();

            AddAuditTrailEntry(command, user);
        }

        private void ValidateAgainstExistingAccounts(User user, Interfaces.Model.AccountType originalAccountType, AccountType mappedAccountType,
            string accountName)
        {
            if (repository.AccountExists(mappedAccountType, accountName))
            {
                throw new UserAccountAlreadyAssignedException(accountName);
            }

            if (user.HasAccountType(mappedAccountType))
            {
                throw new UserAccountTypeAlreadyAssignedException(originalAccountType);
            }
        }

        private void AddAuditTrailEntry(IUserIdAndNameCommand command, User user)
        {
            auditTrail.AddEntry(command.UserId, user.Name, command.Name, "Käyttäjätunnus luotu");
        }

        private static void ValidateCommand(IUserIdAndNameCommand command)
        {
            var specification = new UserIdAndNameSpecification();
            if (!specification.IsSatisfiedBy(command))
            {
                throw new ArgumentException(specification.GetReasonsForDissatisfactionSeparatedWithNewLine());
            }
        }

        private static AccountType Map(Interfaces.Model.AccountType type)
        {
            return (AccountType) Enum.Parse(typeof(AccountType), type.ToString());
        }
    }
}