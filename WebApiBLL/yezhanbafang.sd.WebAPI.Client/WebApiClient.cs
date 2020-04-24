using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace yezhanbafang.sd.WebAPI.Client
{
    public class WebApiClient
    {
        public async Task<string> CallWCF_Async(string bcin,string url)
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
        public string CallWCF_Syn(string bcin, string url)
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
