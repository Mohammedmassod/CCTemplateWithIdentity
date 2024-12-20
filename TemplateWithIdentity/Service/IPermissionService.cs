namespace TemplateWithIdentity.Service
{
    public interface IPermissionService
    {
        bool HasPermission(string permission);
        bool HasAnyPermission(IEnumerable<string> permissions);
        bool HasRole(string role);
        bool HasAnyRole(IEnumerable<string> roles);
    }

}
