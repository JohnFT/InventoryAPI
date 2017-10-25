using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventory.Business.Entities;
using System.Net.Http;
using Newtonsoft.Json;

namespace Inventory.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Login();
            Console.ReadKey();
        }

        static async void Login()
        {
            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync("http://localhost:64343/api/Session/Login?userName=CRodriguez&passwd=123"))
            using (HttpContent content = response.Content)
            {
                string result = await content.ReadAsStringAsync();

                if (result != null)
                {
                    Result<Session> res = JsonConvert.DeserializeObject<Result<Session>>(result, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                    if(res.Success == 500)
                    {
                        Console.WriteLine(res.Message);
                    }
                    if (res.Success == 200)
                    {
                        if(res.Data == null)
                        {
                            Console.WriteLine(res.Message);
                        }
                        else
                        {
                            Console.WriteLine(res.Data.Token + " - " + res.Data.User.FirstName);
                            GetUser(res.Data.Token);
                        }
                    }
                }
            }
        }

        static async void GetUser(string token)
        {
            HttpClient client = new HttpClient();
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri("http://localhost:64343/api/User/GetUser?id=1"),
                Method = HttpMethod.Get,
            };
            request.Headers.Add("Authorization", token);
            using (HttpResponseMessage response = await client.SendAsync(request))
            using (HttpContent content = response.Content)
            {
                string result = await content.ReadAsStringAsync();

                if (result != null)
                {
                    Result<User> res = JsonConvert.DeserializeObject<Result<User>>(result, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                    if (res.Success == 500)
                    {
                        Console.WriteLine(res.Message);
                    }
                    if (res.Success == 200)
                    {
                        if (res.Data == null)
                        {
                            Console.WriteLine(res.Message);
                        }
                        else
                        {
                            Console.WriteLine(res.Data.FirstName + " " + res.Data.LastName);
                        }
                    }
                }
            }
        }
    }
}
