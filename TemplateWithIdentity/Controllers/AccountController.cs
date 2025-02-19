using TemplateWithIdentity.Models;
using TemplateWithIdentity.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using NuGet.Protocol.Plugins;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using NuGet.Common;
using TemplateWithIdentity.Helper;
using TemplateWithIdentity.Service;

namespace TemplateWithIdentity.Controllers
{
    public class AccountController : Controller
    {

        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly HttpClient _httpClient;



        public AccountController(
            IDataProtectionProvider dataProtectionProvider, IHttpClientFactory httpClientFactory
            )
        {

            _dataProtectionProvider = dataProtectionProvider;
            _httpClient = httpClientFactory.CreateClient("MyClient");

        }

        //دالة التي :، يمكن استخدام عنوان IP العام للجهاز كمعرف، وذلك باستخدام الكود التالي:
        private string GetDeviceId()
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress;
            return ipAddress.ToString();
            // استخدام معرف ملف تعريف المستخدم (User Agent) الخاص بالمتصفح كمعرف للجهاز
            /* var userAgent = HttpContext.Request.Headers["User-Agent"].ToString();
                return userAgent;*/
        }
        private bool IsLoggedInFromAnotherDevice()
        {
            var existingDeviceId = HttpContext.Session.GetString("DeviceId");
            return !string.IsNullOrEmpty(existingDeviceId) && existingDeviceId != GetDeviceId();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(login.Pass) || string.IsNullOrEmpty(login.UserName))
                {
                    // قد ترغب في إضافة رسالة خطأ هنا أو إرجاع رد مناسب
                    return BadRequest("البيانات غير مكتملة.");
                }
                /*var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);*/
               // var result = await signInManager.PasswordSignInAsync(login.UserName, login.Pass, login.RememberMe, lockoutOnFailure: true);
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync("https://localhost:7200/api/v1/user/login\r\n", login);

                if (response.IsSuccessStatusCode)
                {
                    var result1 = await response.Content.ReadFromJsonAsync<LoginResponse>();
                    // Store the token (e.g., in session or local storage)
                    HttpContext.Session.SetString("JwtToken", result1.Token);


                    // Extract roles and permissions from token
                    var roles = JwtHelper.GetRolesFromToken(result1.Token);
                    var permissions = JwtHelper.GetPermissionsFromToken(result1.Token);

                    HttpContext.Session.SetString("UserRoles", string.Join(",", roles));
                    HttpContext.Session.SetString("UserPermissions", string.Join(",", permissions));


                    var existingDeviceId = HttpContext.Session.GetString("DeviceId");
                    if (!string.IsNullOrEmpty(existingDeviceId) && existingDeviceId != GetDeviceId())
                    {
                        ModelState.AddModelError(string.Empty, "You are already logged in from another browser.");
                        return View(login);
                    }
                 
                    string Name = (login.UserName);
                    string PhoneNumber = (login.UserName);
                    //Store the protected values in the session
                    HttpContext.Session.SetString("Name", Name);
                    HttpContext.Session.SetString("PhoneNumber", PhoneNumber);
                    // توجيه المستخدم إلى الصفحة الرئيسية
                    return RedirectToAction("Index", "Home");

                }
                else {

                  
                    ModelState.AddModelError(string.Empty, "اسم المستخدم او كلمة السر خطاء ");
                    return View(login);

                }

            }
            else
                {
                    ModelState.AddModelError(string.Empty, "اسم المستخدم او كلمة السر خطاء ");
                    return View(login);
                }

            }
        public async Task<ActionResult> Logout()
        {
            // Remove current device id from session
            //await signInManager.SignOutAsync();
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("id");
            HttpContext.Session.Remove("Name");
            HttpContext.Session.Remove("PhoneNumber");
            HttpContext.Session.Remove("JwtToken");


            ViewBag.Message = "لقد تم تسجيل الخروج";
            return RedirectToAction("Login", "Account");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }

       
    }


       
    

