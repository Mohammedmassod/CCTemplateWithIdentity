namespace TemplateWithIdentity.Helper
{
    public static class ApiRoutes
    {
        public const string BaseUrl = "https://api.example.com/"; // الرابط الأساسي للخادم

        public static class Users
        {
            public const string GetAll = "users";
            public const string GetById = "users/{0}"; // {0} => مكان معرف المستخدم
            public const string Create = "users";
            public const string Update = "users/{0}";
            public const string Delete = "users/{0}";
        }

        public static class Auth
        {
            public const string Login = "user/login";
            public const string GetById = "products/{0}";
            public const string Create = "products";
            public const string Update = "products/{0}";
            public const string Delete = "products/{0}";
        }
    }

}
