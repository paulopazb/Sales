﻿

namespace Sales.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Plugin.Connectivity;
    using Sales.Common;
    using Sales.Common.Models;

    public class ApiService
    {
        public async Task<Response> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Please turn on your internet connectivity",
                };
            }

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");
            if (!isReachable)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "No Internet connection",
                };
            }

            return new Response
            {
                IsSuccess = true,
            };
        }
        public async Task<TokenResponse> GetToken(string urlBase, string username, string password)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var response = await client.PostAsync("Token",
                    new StringContent(string.Format(
                    "grant_type=password&username={0}&password={1}",
                    username, password),
                    Encoding.UTF8, "application/x-www-form-urlencoded"));
                var resultJSON = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TokenResponse>(
                    resultJSON);
                return result;
            }
            catch
            {
                return null;
            }
        }//Permite obtener el token
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
        public async Task<Response> GetList<T>(string urlBase, string prefix, string controller,string tokenType, string accessToken)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                var url = $"{prefix}{controller}";
                var response = await client.GetAsync(url);
                var answer = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = answer,
                    };
                }

                var list = JsonConvert.DeserializeObject<List<T>>(answer);
                return new Response
                {
                    IsSuccess = true,
                    Result = list,
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
        public async Task<Response> Post<T>(string urlBase, string prefix, string controller, T model)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = $"{prefix}{controller}";
                var response = await client.PostAsync(url, content);
                var answer = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = answer,
                    };
                }

                var obj = JsonConvert.DeserializeObject<T>(answer);
                return new Response
                {
                    IsSuccess = true,
                    Result = obj,
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
        public async Task<Response> Post<T>(string urlBase, string prefix, string controller, T model,string tokenType, string accessToken)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                var url = $"{prefix}{controller}";
                var response = await client.PostAsync(url, content);
                var answer = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = answer,
                    };
                }

                var obj = JsonConvert.DeserializeObject<T>(answer);
                return new Response
                {
                    IsSuccess = true,
                    Result = obj,
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
        public async Task<Response> Put<T>(string urlBase, string prefix, string controller, T model, int id)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = $"{prefix}{controller}/{id}";
                var response = await client.PutAsync(url, content);
                var answer = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = answer,
                    };
                }

                var obj = JsonConvert.DeserializeObject<T>(answer);
                return new Response
                {
                    IsSuccess = true,
                    Result = obj,
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
        public async Task<Response> Put<T>(string urlBase, string prefix, string controller, T model, int id, string tokenType, string accessToken)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                var url = $"{prefix}{controller}/{id}";
                var response = await client.PutAsync(url, content);
                var answer = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = answer,
                    };
                }

                var obj = JsonConvert.DeserializeObject<T>(answer);
                return new Response
                {
                    IsSuccess = true,
                    Result = obj,
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
        public async Task<Response> Delete(string urlBase, string prefix, string controller, int id)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = $"{prefix}{controller}/{id}";
                var response = await client.DeleteAsync(url);
                var answer = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = answer,
                    };
                }

                return new Response
                {
                    IsSuccess = true,
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
        public async Task<Response> Delete(string urlBase, string prefix, string controller, int id, string tokenType, string accessToken)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                var url = $"{prefix}{controller}/{id}";
                var response = await client.DeleteAsync(url);
                var answer = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = answer,
                    };
                }

                return new Response
                {
                    IsSuccess = true,
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
