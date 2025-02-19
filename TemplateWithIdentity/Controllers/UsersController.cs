using TemplateWithIdentity.Models;
using TemplateWithIdentity.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TemplateWithIdentity.Service;
using TemplateWithIdentity.Helper;
using static TemplateWithIdentity.Helper.ApiRoutes;

namespace TemplateWithIdentity.Controllers
{
    [Authorize]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers() => Ok(await _userService.GetUsersAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id) => Ok(await _userService.GetUserByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserVM user) => Ok(await _userService.CreateUserAsync(user));

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserVM user)
            => await _userService.UpdateUserAsync(id, user) ? NoContent() : BadRequest();

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
            => await _userService.DeleteUserAsync(id) ? NoContent() : BadRequest();
    }
}
