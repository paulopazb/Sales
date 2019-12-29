

namespace Sales.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Sales.Common;
  
    public class ApiService
    {
        public async Task <Response> GetList<T>(string urlBase, string prefix,string controller) // Metodo que permite consumir listas de cualquier tipo es decir que es generico
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = $"{prefix}{controller}"; //Concatenacion
                var response = await client.GetAsync(url);// Await realiza una espera
                var answer = await response.Content.ReadAsStringAsync(); //Json en forma de string
                if (!response.IsSuccessStatusCode)// Si fallo la conexion 
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = answer,

                    };


                }
                var list = JsonConvert.DeserializeObject<List<T>>(answer);//answer contiene el string Json // List<T> es una lista de objetos, en este caso de Products
                return new Response
                {
                    IsSuccess = true,
                    Result=list,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,

                };
                
            }
        }
    }
}
