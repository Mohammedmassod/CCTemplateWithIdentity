using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using TemplateWithIdentity.Service;

namespace TemplateWithIdentity.Helper
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlContent PermissionCheck(this IHtmlHelper htmlHelper, IEnumerable<string> permissions, Func<object, HelperResult> template)
        {
            var serviceProvider = htmlHelper.ViewContext.HttpContext.RequestServices;
            var permissionService = serviceProvider.GetService<IPermissionService>();

            if (permissionService.HasAnyPermission(permissions))
            {
                return template(null);
            }

            return HtmlString.Empty;
        }

        public static IHtmlContent RoleCheck(this IHtmlHelper htmlHelper, IEnumerable<string> roles, Func<object, HelperResult> template)
        {
            var serviceProvider = htmlHelper.ViewContext.HttpContext.RequestServices;
            var permissionService = serviceProvider.GetService<IPermissionService>();

            if (permissionService.HasAnyRole(roles))
            {
                return template(null);
            }

            return HtmlString.Empty;
        }
    }

}
