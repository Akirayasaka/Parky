using Newtonsoft.Json;
using ParkyWeb.Repository.IRepository;
using System.Text;

namespace ParkyWeb.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IHttpClientFactory _clientFactory;

        public Repository(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<bool> CreateAsync(string url, T entity, string token = "")
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            if(entity != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            }
            else
            {
                return false;
            }

            var client = _clientFactory.CreateClient();

            #region 設定token於request header
            if ( token != null && token.Length != 0)
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            #endregion

            HttpResponseMessage response = await client.SendAsync(request);
            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }

        public async Task<bool> DeleteAsync(string url, int Id, string token = "")
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, url+Id);
            var client = _clientFactory.CreateClient();

            if(token != null && token.Length != 0)
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            HttpResponseMessage response = await client.SendAsync(request);
            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }

        public async Task<IEnumerable<T>> GetAllAsync(string url, string token = "")
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var client = _clientFactory.CreateClient();

            if (token != null && token.Length != 0)
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            HttpResponseMessage response = await client.SendAsync(request);
            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonString);
            }
            else
            {
                return null;
            }
        }

        public async Task<T> GetAsync(string url, int Id, string token = "")
        {
            var request =new HttpRequestMessage(HttpMethod.Get, url+Id);
            var client = _clientFactory.CreateClient();

            if (token != null && token.Length != 0)
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(jsonString);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> UpdateAsync(string url, T entity, string token = "")
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, url);
            if(entity != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            }
            else
            {
                return false;
            }

            var client = _clientFactory.CreateClient();

            if (token != null && token.Length != 0)
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            HttpResponseMessage response = await client.SendAsync(request);
            return response.StatusCode == System.Net.HttpStatusCode.OK;
        }
    }
}
