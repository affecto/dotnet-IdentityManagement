﻿using System;
using System.Collections.Generic;
using System.Linq;
using Affecto.IdentityManagement.Commanding.Commands;
using Affecto.IdentityManagement.Commanding.Validation;
using Affecto.IdentityManagement.Store.Model;
using Affecto.Patterns.Cqrs;

namespace Affecto.IdentityManagement.Commanding.CommandHandlers
{
    internal class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>, ICommandHandler<UpdateUserCustomPropertiesCommand>
    {
        private readonly IDbRepository repository;
        private readonly IAuditTrailWriter auditTrail;

        public UpdateUserCommandHandler(IDbRepository repository, IAuditTrailWriter auditTrail)
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

        public void Execute(UpdateUserCommand command)
        {
            IdAndNameSpecification specification = new IdAndNameSpecification();
            if (specification.IsSatisfiedBy(command))
            {
                User user = repository.GetUser(command.Id);
                user.Name = command.Name;
                user.IsDisabled = command.IsDisabled;
                repository.SaveChanges();

                auditTrail.AddEntry(command.Id, command.Name, "Käyttäjä päivitetty");
            }
            else
            {
                throw new ArgumentException(specification.GetReasonsForDissatisfactionSeparatedWithNewLine());
            }
        }

        public void Execute(UpdateUserCustomPropertiesCommand command)
        {
            User user = repository.GetUser(command.Id);
            if (command.CustomProperties != null && command.CustomProperties.Any())
            {                  
                user.CustomProperties.ToList().ForEach(prop => user.CustomProperties.Remove(prop));
                foreach (KeyValuePair<string, string> customProperty in command.CustomProperties)
                {
                    user.CustomProperties.Add(new CustomProperty
                    {
                        Id = Guid.NewGuid(),
                        Name = customProperty.Key,
                        Value = customProperty.Value
                    });
                }
            }

            repository.SaveChanges();
            auditTrail.AddEntry(command.Id, user.Name, "Käyttäjän custom propertyt päivitetty");
        }
           
    }
}