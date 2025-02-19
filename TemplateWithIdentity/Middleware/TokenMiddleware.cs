using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace TemplateWithIdentity.Middleware
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHttpContextAccessor _contextAccessor;
        public TokenMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor)
        {
            _next = next;
            _contextAccessor = httpContextAccessor;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            _contextAccessor.HttpContext = context;
          var token =  context.Session.Get("JwtToken");
            if (token == null)
            {

                RedirectToPageResult.ReferenceEquals(context, _next);
            }
            // تمرير الطلب إلى Middleware التالي في السلسلة
            await _next(context);
        }
    }
}
