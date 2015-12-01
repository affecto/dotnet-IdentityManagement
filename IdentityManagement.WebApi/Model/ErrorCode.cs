namespace Affecto.IdentityManagement.WebApi.Model
{
    public class ErrorCode
    {
        public const string UserAccountAlreadyAssigned = "USER_ACCOUNT_ALREADY_ASSIGNED";
        public const string GroupWithSameNameAlreadyExists = "GROUP_NAME_ALREADY_EXISTS";
        public const string OrganizationWithSameNameAlreadyExists = "ORGANIZATION_NAME_ALREADY_EXISTS";
        public const string RoleWithSameNameAlreadyExists = "ROLE_NAME_ALREADY_EXISTS";
    }
}