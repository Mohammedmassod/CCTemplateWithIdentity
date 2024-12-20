using Microsoft.AspNetCore.Razor.TagHelpers;
using TemplateWithIdentity.Service;

namespace TemplateWithIdentity.Helper
{
    [HtmlTargetElement("permission-check")]
    public class PermissionTagHelper : TagHelper
    {
        private readonly IPermissionService _permissionService;

        public PermissionTagHelper(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HtmlAttributeName("permissions")]
        public string[] Permissions { get; set; }

        [HtmlAttributeName("roles")]
        public string[] Roles { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            bool hasPermission = Permissions != null && _permissionService.HasAnyPermission(Permissions);
            bool hasRole = Roles != null && _permissionService.HasAnyRole(Roles);

            if (!hasPermission && !hasRole)
            {
                output.SuppressOutput();
            }
        }
    }

}
