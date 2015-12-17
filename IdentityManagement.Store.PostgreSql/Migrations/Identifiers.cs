using System;

namespace Affecto.IdentityManagement.Store.PostgreSql.Migrations
{
    public class Identifiers
    {
        public static readonly Guid AdministratorRoleId = Guid.Parse("B5A20242-A695-4E24-9173-88AF3A90D2E9");
        public static readonly Guid OrganizationAdministratorRoleId = Guid.Parse("70089FF9-548F-4A45-854C-ED9F56B13EA6");
        public static readonly Guid BasicUserRoleId = Guid.Parse("0CCF78EA-10EA-4B00-A16B-9776C77CA46F");

        public static readonly Guid UserMaintenancePermissionId = Guid.Parse("185CF94C-70E8-4A3F-8624-103A5E6ED322");
        public static readonly Guid ViewAllUsersPermissionId = Guid.Parse("904C9937-6B86-479C-BC48-AA6EA884C3F9");
        public static readonly Guid ViewUserOrganizationUsersPermissionId = Guid.Parse("CA758A25-5412-4A73-95BC-622F1F92D7D6");
        public static readonly Guid ManageAdministratorUsersPermissionId = Guid.Parse("6F2CA7EA-1D66-4A04-BDBB-C298D17EED5B");
    }
}