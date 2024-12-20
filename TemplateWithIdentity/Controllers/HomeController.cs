using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Diagnostics;
using TemplateWithIdentity.Models;

namespace TemplateWithIdentity.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        // [AllowAnonymous]
        //[Authorize]
        //تحديد مسار مخصص لصفحة بحيث لايمكن الدخول اليها بدون تسجيل الدخول
/*        [Route("kldfjfvlkdfaskhndfavjkn")]
*/        public IActionResult Index()
        {
            // استرجاع الصلاحيات من الجلسة
            var rolesString = HttpContext.Session.GetString("UserRoles");
            var roles = rolesString?.Split(',') ?? Array.Empty<string>();

            // تمرير الصلاحيات إلى العرض
            ViewData["Roles"] = roles;
            // تنفيذ العمليات اللازمة هنا
            return View();
            
            
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}