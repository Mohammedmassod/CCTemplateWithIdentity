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

namespace TemplateWithIdentity.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<Users> signInManager;
        private readonly UserManager<Users> userManager;
        private readonly IDataProtectionProvider _dataProtectionProvider;
        private readonly HttpClient _httpClient;



        public AccountController(SignInManager<Users> signInManager,UserManager<Users> userManager, 
            IDataProtectionProvider dataProtectionProvider, IHttpClientFactory httpClientFactory)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
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

                    //var tokenHandler = new JwtSecurityTokenHandler();
                    //var token = HttpContext.Session.GetString("JwtToken");
                    //var jwtToken = tokenHandler.ReadJwtToken(token);
                   // var username = jwtToken.Claims.First(claim => claim.Type == "Admin").Value;


                    var existingDeviceId = HttpContext.Session.GetString("DeviceId");
                    if (!string.IsNullOrEmpty(existingDeviceId) && existingDeviceId != GetDeviceId())
                    {
                        ModelState.AddModelError(string.Empty, "You are already logged in from another browser.");
                        return View(login);
                    }
                 /*   HttpContext.Session.SetString("deviceId", GetDeviceId());
                    //اذا استخدمنا الثانية
                    var deviceId = GetDeviceId();
                  

                    if (IsLoggedInFromAnotherDevice() == deviceId)
                    {
                        ModelState.AddModelError(string.Empty, "تم تسجيل الدخول من جهاز آخر.");
                        return View(login);
                    }
                    HttpContext.Session.SetString("DeviceId", deviceId);*/
                   /* var tokenHandler = new JwtSecurityTokenHandler();
                    var jwtToken = tokenHandler.ReadJwtToken(token);
                    var username = jwtToken.Claims.First(claim => claim.Type == "unique_name").Value;
                    var username = jwtToken.Claims.First(claim => claim.Type == "unique_name").Value;*/


                    string Name = (login.UserName);
                    string PhoneNumber = (login.UserName);
                    //Store the protected values in the session
                    HttpContext.Session.SetString("Name", Name);
                    HttpContext.Session.SetString("PhoneNumber", PhoneNumber);
                    // توجيه المستخدم إلى الصفحة الرئيسية
                    return RedirectToAction("Index", "Home");

                }
                else {

                    /*if (result.Succeeded)
                    {
                        // store the user name in session
                        var user = await userManager.FindByNameAsync(login.UserName);
                        if (user != null && await userManager.CheckPasswordAsync(user, login.Pass))
                        {
                            // Check if user is already logged in from another browser
                            var existingDeviceId = HttpContext.Session.GetString("DeviceId");
                            if (!string.IsNullOrEmpty(existingDeviceId) && existingDeviceId != GetDeviceId())
                            {
                                ModelState.AddModelError(string.Empty, "You are already logged in from another browser.");
                                return View(login);
                            }
                            HttpContext.Session.SetString("deviceId", GetDeviceId());

                            //اذا استخدمنا الثانية
                            var deviceId = GetDeviceId();

                            //// تحقق من عدم تسجيل الدخول من جهاز آخر باستخدام معرف الجهاز المستخدم الحالي
                            //if (IsLoggedInFromAnotherDevice(deviceId))
                            //{
                            //    ModelState.AddModelError(string.Empty, "تم تسجيل الدخول من جهاز آخر.");
                            //    return View(model);
                            //}

                            // تسجيل الدخول
                            // ...

                            // تخزين معرف الجهاز في الجلسة
                            HttpContext.Session.SetString("DeviceId", deviceId);

                            // توجيه المستخدم إلى الصفحة الرئيسية
                            return RedirectToAction("Index", "Home");
                        }

                        //private bool IsLoggedInFromAnotherDevice(string currentDeviceId)
                        //{
                        //    // استعلام قاعدة البيانات أو ذاكرة التخزين المؤقت للتحقق من عدم تسجيل الدخول من جهاز آخر باستخدام معرف الجهاز الحالي
                        //    // ...
                        //}

                        //
                        // Store device id in session
                        //var protector = _dataProtectionProvider.CreateProtector("UserProperties");
                        //لكي تحفظها مشفرة داخل الجلسة
                        // Protect the user's name and email before storing them in the session
                        //string protectedName = protector.Protect(user.Name);
                        //string protectedPhoneNumber = protector.Protect(user.PhoneNumber);
                        //string Name = (user.Name);
                        //string PhoneNumber = (user.PhoneNumber);


                        //Store the protected values in the session
                        //HttpContext.Session.SetString("Name", Name);
                        //HttpContext.Session.SetString("PhoneNumber", PhoneNumber);
                    }*/
                    ModelState.AddModelError(string.Empty, "اسم المستخدم او كلمة السر خطاء ");
                    return View(login);

                }

            }

            // اذا تقفل الحساب ماذا يفعل النظام
            //if (result.IsLockedOut)
            //{
            //    /*var forgotPassLink = Url.Action(nameof(ForgotPassword), "Account", new { }, Request.Scheme);
            //    var content = string.Format("Your account is locked out, to reset your password, please click this link: {0}", forgotPassLink);
            //    var message = new Message(new string[] { userModel.Email }, "Locked out account information", content, null);
            //    await _emailSender.SendEmailAsync(message);*/
            //    ModelState.AddModelError("", "تم قفل الحساب");
            //    return View();
            //}
            else
                {
                    ModelState.AddModelError(string.Empty, "اسم المستخدم او كلمة السر خطاء ");
                    return View(login);
                }

            }
        public async Task<ActionResult> Logout()
        {
            // Remove current device id from session
            await signInManager.SignOutAsync();
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("id");
            HttpContext.Session.Remove("Name");
            HttpContext.Session.Remove("PhoneNumber");

            ViewBag.Message = "لقد تم تسجيل الخروج";
            return RedirectToAction("Login", "Account");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }

       
    }


       
    

