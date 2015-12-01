using System;
using System.Web.Http.Filters;
using Affecto.IdentityManagement.Interfaces.Exceptions;
using Affecto.IdentityManagement.WebApi.Model;
using Affecto.WebApi.Toolkit;

namespace Affecto.IdentityManagement.WebApi.RequestHandling
{
    public class WebApiRequestExceptionFilter : RequestExceptionFilter
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            Exception exception = actionExecutedContext.Exception;
            if (exception is UserAccountAlreadyAssignedException)
            {
                actionExecutedContext.Response = CreateStringContentResponse(ErrorCode.UserAccountAlreadyAssigned, "User account is already assigned to another user.");
            }
            else if (exception is GroupWithSameNameAlreadyExistsException)
            {
                actionExecutedContext.Response = CreateStringContentResponse(ErrorCode.GroupWithSameNameAlreadyExists, "Group with the same name already exists.");
            }
            else if (exception is OrganizationWithSameNameAlreadyExistsException)
            {
                actionExecutedContext.Response = CreateStringContentResponse(ErrorCode.OrganizationWithSameNameAlreadyExists, "Organization with the same name already exists.");
            }
            else if (exception is RoleWithSameNameAlreadyExistsException)
            {
                actionExecutedContext.Response = CreateStringContentResponse(ErrorCode.RoleWithSameNameAlreadyExists, "Role with the same name already exists.");
            }
            else
            {
                base.OnException(actionExecutedContext);
            }
        }
    }
}