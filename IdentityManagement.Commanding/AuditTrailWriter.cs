using System;
using Affecto.AuditTrail.Interfaces;
using Affecto.Authentication.Claims;

namespace Affecto.IdentityManagement.Commanding
{
    internal class AuditTrailWriter : IAuditTrailWriter
    {
        private readonly IAuditTrailService auditTrailService;
        private readonly IAuthenticatedUserContext authenticatedUser;

        public AuditTrailWriter(IAuditTrailService auditTrailService, IAuthenticatedUserContext authenticatedUser)
        {
            if (auditTrailService == null)
            {
                throw new ArgumentNullException("auditTrailService");
            }
            if (authenticatedUser == null)
            {
                throw new ArgumentNullException("authenticatedUser");
            }
            this.auditTrailService = auditTrailService;
            this.authenticatedUser = authenticatedUser;
        }

        public void AddEntry(Guid subjectId, string subjectName, string summary)
        {
            if (authenticatedUser.IsSystemUser)
            {
                auditTrailService.CreateEntry(subjectId, summary, subjectName, authenticatedUser.UserName);
            }
            else
            {
                auditTrailService.CreateEntry(subjectId, authenticatedUser.UserId, summary, subjectName, authenticatedUser.UserName);
            }
        }

        public void AddEntry(Guid subjectId, string subjectName, string secondarySubjectName, string summary)
        {
            if (string.IsNullOrEmpty(subjectName))
            {
                throw new ArgumentException("subjectName");
            }
            if (string.IsNullOrEmpty(secondarySubjectName))
            {
                throw new ArgumentException("secondarySubjectName");
            }
            var subject = string.Format("{0}: {1}", subjectName, secondarySubjectName);
            AddEntry(subjectId, subject, summary);
        }
    }
}