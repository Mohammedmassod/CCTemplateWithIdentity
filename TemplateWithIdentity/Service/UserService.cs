using System.Text;
using Microsoft.AspNetCore.Identity.Data;
using Newtonsoft.Json;
using TemplateWithIdentity.Helper;
using TemplateWithIdentity.ViewModels;

namespace TemplateWithIdentity.Service
{
    public class UserService
    {
        private readonly ApiService<UserVM> _apiService;

        public UserService(ApiService<UserVM> apiService)
        {
            _apiService = apiService;
        }

        public Task<List<UserVM>> GetUsersAsync() => _apiService.GetAllAsync();
        public Task<UserVM> GetUserByIdAsync(int id) => _apiService.GetByIdAsync(id);
        public Task<UserVM> CreateUserAsync(UserVM user) => _apiService.CreateAsync(user);
        public Task<bool> UpdateUserAsync(int id, UserVM user) => _apiService.UpdateAsync(id, user);
        public Task<bool> DeleteUserAsync(int id) => _apiService.DeleteAsync(id);
    }

}
