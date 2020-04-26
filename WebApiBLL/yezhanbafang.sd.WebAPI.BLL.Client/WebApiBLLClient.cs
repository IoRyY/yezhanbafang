using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace yezhanbafang.sd.WebAPI.BLL.Client
{
    public class WebApiBLLClient
    {
        public async Task<string> CallWebAPI_Async(string bcin, string url)
        {
            using (var client = new HttpClient())
            {
                HttpContent content = new StringContent(bcin);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
        }
        public string CallWebAPI_Syn(string bcin, string url)
        {
            using (var client = new HttpClient())
            {
                HttpContent content = new StringContent(bcin);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                HttpResponseMessage response = client.PostAsync(url, content).Result;
                response.EnsureSuccessStatusCode();//用来抛异常的
                string responseBody = response.Content.ReadAsStringAsync().Result;
                return responseBody;
            }
        }
    }
}
