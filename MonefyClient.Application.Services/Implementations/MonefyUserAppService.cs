using MonefyClient.Application.DTOs.InputDTOs;
using MonefyClient.Application.Services.Abstractions;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonefyClient.Application.Services.Implementations
{
    public class MonefyUserAppService : IMonefyUserAppService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _log;

        public MonefyUserAppService(HttpClient httpClient, ILogger log)
        {
            _httpClient = httpClient;
            _log = log;
        }

        public async Task<bool> ValidateLogin(InputUserDTO user) //Poner async al llamar a la API
        {
            // Mock
            //if (user.Email == "jf@gmail.com" && user.Password == "jfvueling")
            //{
            //    return true;
            //}
            //return false;

            // Real
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("url", content);

            if (response.IsSuccessStatusCode) 
            {
                // Guardar el token
                _log.Information($"Valid user");
                return true;
            }

            _log.Information($"Invalid user");
            return false;
        }

        public bool CreateUser(InputUserDTO user) //Poner async al llamar a la API
        {
            // Mock

            return true;
            // Real
            //var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            //var response = await _httpClient.PostAsync("url", content);

            //if (response.IsSuccessStatusCode)
            //{
            //    // Guardar el token
            //    _log.Information($"Create user");
            //    return true;
            //}

            //_log.Information($"User not create");
            //return false;
        }
    }
}
