using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Net.Http.Headers;
using TemplateWithIdentity.Models;
var builder = WebApplication.CreateBuilder(args);
//
builder.Services.AddDataProtection()
  .SetApplicationName("TemplateWithIdentity");
// Add services to the container.


//builder.Services.AddCors();
builder.Services.AddHttpClient("MyClient", client =>
{
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});
//اضافة خدمة حماية البيانات من اجل تشفير بيانات الجلسة وغيرها
builder.Services.AddDataProtection();
//

builder.Services.AddControllersWithViews()
    .AddSessionStateTempDataProvider();
builder.Services.AddDistributedMemoryCache();
//تحديد تكوين الجلسة
builder.Services.AddSession(options =>
{
    //options.Cookie.Name = ".AdventureWorks.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.Name = ".MyApp.Session"; // تعيين اسم فريد للكوكيز
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // تأكيد إرسال الكوكيز فقط عبر HTTPS
    options.Cookie.SameSite = SameSiteMode.Strict; // تقييد إرسال الكوكيز إلى نفس الموقع
});
//يتم استخدامه لتعيين الفاصل الزمني للتحقق من صحة طوابع أمان هوية ASP.NET.
/// <summary>
/// إذا مرت أكثر من دقيقة واحدة دون طلب
/// يوفر هذا فاصلًا زمنيًا صارمًا للتحقق من الصحة للكشف الفوري عن أي تغييرات تطرأ على حساب المستخدم.
/// </summary>
builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{
    options.ValidationInterval = TimeSpan.FromMinutes(1);
});


// تكوين سياسات الوصول
//builder.Services.AddAuthorization(options =>
//{
//    // تحديد سياسات التحكم في الوصول
//    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
//    options.AddPolicy("User", policy => policy.RequireRole("User"));
//    options.AddPolicy("SuperAdmin", policy => policy.RequireRole("SuperAdmin"));


//});
//


//

builder.Services.AddMvc(options =>
{
    options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
        (_) => "The field is required.");
}).AddSessionStateTempDataProvider();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
        // options.SlidingExpiration = true;//تجديد الجلسة مادام المستخدم يتفاعل مع التطبيق
        options.Events.OnRedirectToLogin = context =>
        {
            context.Response.Redirect("/Account/Login");
            return Task.CompletedTask;
        };
        options.Events.OnRedirectToAccessDenied = context =>
        {
            context.Response.Redirect("/Account/AccessDenied");
            return Task.CompletedTask;
        };

    });

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


// add session middleware
app.UseSession();

app.Use(async (context, next) =>
{
    await next.Invoke();
    // Check session timeout here
    if (context.Session.IsAvailable)
    {
        // Session has not expired, so skip

        return;
    }


    // Redirect to logout page
    context.Response.Redirect("/Logout");
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
