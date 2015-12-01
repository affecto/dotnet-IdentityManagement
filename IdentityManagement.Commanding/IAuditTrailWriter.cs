using System;

namespace Affecto.IdentityManagement.Commanding
{
    public interface IAuditTrailWriter
    {
        void AddEntry(Guid subjectId, string subjectName, string summary);
        void AddEntry(Guid subjectId, string subjectName, string secondarySubjectName, string summary);
    }
}