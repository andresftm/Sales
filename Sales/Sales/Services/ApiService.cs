
namespace Sales.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Plugin.Connectivity;
    using Sales.Common.models;
    using Sales.Helpers;


    public class ApiService
    {
        public async Task<Response> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {
                    InSucces = false,
                    Message = Languages.ConectInternet,
                };
            }

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");
            if (!isReachable)
            {
                return new Response
                {
                    InSucces = false,
                    Message = Languages.ConectInternet,
                };
            }

            return new Response
            {
                InSucces = true,
            };

        }

        public async Task<Response> GetList<T>(string urlBase, string prefix, string controller)
        {
            try
            {
                var cliente = new HttpClient();
                cliente.BaseAddress = new Uri(urlBase);
                var url = $"{prefix}{controller}";
                var response = await cliente.GetAsync(url);
                var answer = await response.Content.ReadAsStringAsync();

                if(!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        InSucces = false,
                        Message = answer,
                    };
                }

                var list = JsonConvert.DeserializeObject<List<T>>(answer);

                return new Response
                {
                    InSucces = true,
                    Result = list,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    InSucces = false,
                    Message = ex.Message,
                };
            }
        }


    }
}
