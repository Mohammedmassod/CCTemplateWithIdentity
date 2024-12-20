namespace TemplateWithIdentity.Service
{
    public class PermissionService : IPermissionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PermissionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool HasPermission(string permission)
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null) return false;

            return user.Claims.Any(c => c.Type == "Permission" && c.Value == permission);
        }

        public bool HasAnyPermission(IEnumerable<string> permissions)
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null) return false;

            return permissions.Any(permission => user.Claims.Any(c => c.Type == "Permission" && c.Value == permission));
        }

        public bool HasRole(string role)
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null) return false;

            return user.Claims.Any(c => c.Type == "role" && c.Value == role);
        }

        public bool HasAnyRole(IEnumerable<string> roles)
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null) return false;

            return roles.Any(role => user.Claims.Any(c => c.Type == "role" && c.Value == role));
        }
    }

}
