using Newtonsoft.Json;
using System.Text;
using TemplateWithIdentity.Helper;
using TemplateWithIdentity.ViewModels;

namespace TemplateWithIdentity.Service
{
    public class ApiService<T>
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint;

        public ApiService(HttpClient httpClient, string endpoint)
        {
            _httpClient = httpClient;
            _endpoint = endpoint;
        }
        public async Task<string> LoginAsync(LoginVM loginVM)
        {
            var content = new StringContent(JsonConvert.SerializeObject(new { loginVM.UserName, loginVM.Pass }), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(ApiRoutes.Auth.Login, content);

            if (!response.IsSuccessStatusCode)
                return null;

            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            return result?.Token; // إرجاع التوكن
        }

        public async Task<List<T>> GetAllAsync()
        {
            var response = await _httpClient.GetAsync(_endpoint);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<T>>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync(string.Format(_endpoint + "/{0}", id));
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<T> CreateAsync(T entity)
        {
            var response = await _httpClient.PostAsJsonAsync(_endpoint, entity);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<bool> UpdateAsync(int id, T entity)
        {
            var response = await _httpClient.PutAsJsonAsync(string.Format(_endpoint + "/{0}", id), entity);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync(string.Format(_endpoint + "/{0}", id));
            return response.IsSuccessStatusCode;
        }
    }

}
