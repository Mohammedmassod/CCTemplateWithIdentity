using TemplateWithIdentity.Models;
using TemplateWithIdentity.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TemplateWithIdentity.Data;

namespace TemplateWithIdentity.Controllers
{
    [Authorize]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<Users> userManager;
        private readonly RoleManager<Roles> roleManager;
        private readonly AppDbContext context;

        public UsersController(UserManager<Users> userManager,RoleManager<Roles> roleManager,AppDbContext context)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.context = context;
        }
        public async Task<IActionResult> Index(string type="",int ad=0)
        {

            List<UserVM> userlist = new List<UserVM>();
            var userInfo= userManager.Users.ToList();
            foreach(var item in userInfo)
            {
                UserVM userVM = new UserVM(item);
                userlist.Add(userVM);
            }
            if(!string.IsNullOrEmpty(type))
            {
                userlist = userlist.Where(x=>x.isClient=true).ToList();
            }
            if(ad!=0)
            {
                userlist = userlist.Where(x => x.UserName=="222").ToList();
            }
            return  View(userlist);
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserVM obj)
        {
            if(ModelState.IsValid)
            {
                if(obj.PassWord==obj.ConfirmPassWord && !string.IsNullOrEmpty(obj.PassWord))
                {
                    Users newUser = new Users(obj);
                    newUser.Email = "Admin@gmail.com";
                    newUser.Pass=obj.PassWord;
                    var userCreate = await userManager.CreateAsync(newUser,obj.PassWord);

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Password","كلمات المرور غير متطابقة"); 
                }
                
                
            }
            else { return View(obj); }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var currUser = await userManager.FindByIdAsync(id);
            if(currUser != null)
            {
                UserVM user = new UserVM(currUser);
                
                return View (user);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserVM obj)
        {
            if(ModelState.IsValid)
            {
                var currUser = await userManager.FindByIdAsync(obj.Id);
                if(currUser != null)
                {
                    currUser.Name = obj.Name;
                    currUser.PhoneNumber = obj.PhoneNumber;
                    currUser.IsClient = obj.isClient;
                    currUser.UserName=obj.UserName;

                    var updateUser= await userManager.UpdateAsync(currUser);
                
                
                    
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(obj);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string Id)
        {
            var currUser = await userManager.FindByIdAsync(Id);
            if(currUser != null)
            {
               await userManager.DeleteAsync(currUser);
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(string Id)
        {

            var userRes = await userManager.FindByIdAsync(Id);

            if (userRes != null)
            {
                UserVM user = new UserVM(userRes);
               
                return View(user);
            }
            else
            {
                var curruser = await userManager.GetUserAsync(User);
                if (curruser != null)
                {
                    var userRes2 = await userManager.FindByIdAsync(curruser.Id);
                    UserVM user = new UserVM(userRes2);
                    
                    return View(user);
                }

                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> ChPass(string Id)
        {
            var currUser = await userManager.FindByIdAsync(Id);
            if (currUser != null)
            {
                ProfileVM user = new ProfileVM(currUser);
                return View(user);
            }
            return RedirectToAction("Index");

        }
        [HttpPost]
        public async Task<IActionResult> ChPass(ProfileVM obj)
        {
            var user = await userManager.FindByIdAsync(obj.Id);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                if (obj.NewPass != obj.ConfirmPass)
                {
                    TempData["msg"] = "Confirm passwowrd is mistake";
                    ProfileVM user1 = new ProfileVM(user);
                    
                    return View(user1);

                }
                else
                {
                    string oldPass = user.Pass;
                    user.Pass = obj.NewPass;
                    await userManager.UpdateAsync(user);
                    var ch = await userManager.ChangePasswordAsync(user, oldPass, obj.NewPass);
                    if (ch.Succeeded)
                    {
                        TempData["ChangeMessage"] = "Success";
                        return RedirectToAction("Details", new { Id = user.Id });
                    }
                    else
                    {
                        user.Pass = oldPass;
                        await userManager.UpdateAsync(user);
                    }
                    return RedirectToAction("Index");
                }

            }

        }
        [HttpGet]
        public async Task<IActionResult> ChPass2(string Id)
        {
            var currUser = await userManager.FindByIdAsync(Id);
            if (currUser != null)
            {
                ProfileVM user = new ProfileVM(currUser);
                return View(user);
            }
            return RedirectToAction("Index");

        }
        [HttpPost]
        public async Task<IActionResult> ChPass2(ProfileVM obj)
        {
            ProfileVM prof;
            var CurrUser = await userManager.FindByIdAsync(obj.Id);
            if (CurrUser != null)
            {
                if (obj.NewPass == obj.ConfirmPass)
                {
                    var b = await userManager.CheckPasswordAsync(CurrUser, obj.OldPass);
                    if (b == true)
                    {
                        var f = await userManager.ChangePasswordAsync(CurrUser, obj.OldPass, obj.NewPass);
                        if (f.Succeeded)
                        {
                            CurrUser.Pass = obj.NewPass;
                            await userManager.UpdateAsync(CurrUser);

                            ViewBag.SucessMsg = "تم تغيير كلمة السر بنجاح";
                            return RedirectToAction("Index","Home");
                        }
                        else
                        {
                            ModelState.AddModelError("", "فشل في تغيير كلمة السر ");
                            return View(obj);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("OldPass", "كلمة المرور القديمة ليست صحيحة");
                        return View(obj);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "كلمات المرور غير متطابقة");
                    return View(obj);
                }
            }
            else
            {
                return RedirectToAction("Index");
            }

        }
        //[Authorize(Roles="Admin")]
        [HttpGet]
        public async Task<IActionResult> UpdateUserRoles(string id)
        {
            var User2 = await userManager.GetUserAsync(User);
           
            try
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    Users user = await userManager.FindByIdAsync(id);
                    if (user == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        var roles = await userManager.GetRolesAsync(user);
                        var allRoles = roleManager.Roles.ToList();
                        List<UpdateUserRolesVM> updateUserRolesVMs = new List<UpdateUserRolesVM>();
                        foreach (var role in allRoles)
                        {

                            var item = new UpdateUserRolesVM()
                            {
                                RoleId = role.Id,
                                RoleName = role.Name,
                                Name_Ar = role.Name_Ar,
                                IsSelected = false,
                                Id = 0
                            };
                           


                            foreach (var userrole in roles)
                            {
                                if (userrole == role.Name)
                                {
                                    item.IsSelected = true;
                                    item.Id = 1;
                                }
                            }
                            updateUserRolesVMs.Add(item);

                        }
                        
                        return View(new RolesInUserVM()
                        {
                            RolesList = updateUserRolesVMs,
                            Id = id,
                            UserName = user.UserName,
                            Name= user.Name,

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine("in catch in add roles to group: {0},INNER: {1}", ex.Message, ex.InnerException.Message);
                return RedirectToAction("Index");
            }
        }
       // [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> UpdateUserRoles(string id, RolesInUserVM obj)
        {
            try
            {
                if (obj == null)
                {
                    return BadRequest();
                }
                else
                {
                    Users user = await userManager.FindByIdAsync(id);
                    if (user == null)
                    {
                        return BadRequest();
                    }
                    else
                    {
                        var rolesInUser = await userManager.GetRolesAsync(user);
                        foreach (var role in obj.RolesList)
                        {
                            bool old = true;
                            if (role.Id == 0) old = false;
                            if (role.IsSelected ^ old)
                            {
                                Console.WriteLine("NEW :{0}  OLD: {1}", role.IsSelected, old);
                                if (role.IsSelected)
                                {
                                    var addRes = await userManager.AddToRoleAsync(user, role.RoleName);
                                    if (!addRes.Succeeded)
                                    {
                                        Console.WriteLine("====== Faild to add role: {0} to user: {1}", role.RoleName, user.UserName);
                                    }
                                    else
                                    {
                                        Console.WriteLine("====== Success to add role: {0} to user: {1}", role.RoleName, user.UserName);
                                    }
                                }
                                else
                                {
                                    var deleteRes = await userManager.RemoveFromRoleAsync(user, role.RoleName);
                                    if (!deleteRes.Succeeded)
                                    {
                                        Console.WriteLine("====== Faild to Delete role: {0} to user: {1}", role.RoleName, user.UserName);
                                    }
                                    else
                                    {
                                        Console.WriteLine("====== Success to Delete role: {0} to user: {1}", role.RoleName, user.UserName);
                                    }
                                }
                            }
                        }
                        return RedirectToAction("Index", "Users");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("in catch in add or remove roles from user: {0}", ex.Message);
                return RedirectToAction("IndexUser", "Users");
            }
        }
    }
}
