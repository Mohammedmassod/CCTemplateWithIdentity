namespace TemplateWithIdentity.Service
{
    public class HttpService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApiUrl;

        public HttpService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseApiUrl = configuration["BaseApiUrl"] ?? throw new ArgumentNullException("Base API URL is missing in appsettings.json");
        }

        private string BuildUrl(string endpoint) => $"{_baseApiUrl}{endpoint}";

        // جلب جميع البيانات
        public async Task<List<T>> GetAllAsync<T>(string endpoint)
        {
            return await _httpClient.GetFromJsonAsync<List<T>>(BuildUrl(endpoint));
        }

        // جلب عنصر واحد حسب الـ ID
        public async Task<T> GetByIdAsync<T>(string endpoint, int id)
        {
            return await _httpClient.GetFromJsonAsync<T>($"{BuildUrl(endpoint)}/{id}");
        }

        // إضافة عنصر جديد
        public async Task<T> AddAsync<T>(string endpoint, T item)
        {
            var response = await _httpClient.PostAsJsonAsync(BuildUrl(endpoint), item);
            return await response.Content.ReadFromJsonAsync<T>();
        }

        // تعديل عنصر
        public async Task<bool> UpdateAsync<T>(string endpoint, int id, T item)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BuildUrl(endpoint)}/{id}", item);
            return response.IsSuccessStatusCode;
        }

        // حذف عنصر
        public async Task<bool> DeleteAsync(string endpoint, int id)
        {
            var response = await _httpClient.DeleteAsync($"{BuildUrl(endpoint)}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
