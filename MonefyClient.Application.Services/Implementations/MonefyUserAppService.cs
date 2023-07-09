using Microsoft.AspNetCore.Http;
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

        public async Task<string?> ValidateLogin(InputUserDTO user) //Poner async al llamar a la API
        {
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("url/login", content);

            if (response.IsSuccessStatusCode) 
            {
                // Guardar el token
                //var token = response.
                var token = "Hola";
                _log.Information($"Valid user");
                return token;
            }

            _log.Information($"Invalid user");
            return null;
        }

        public async Task<bool> CreateUser(InputUserDTO user)
        {
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("endpoint/AddUser", content);

            if (response.IsSuccessStatusCode)
            {
                _log.Information($"Create user");
                return true;
            }

            _log.Information($"User not create");
            return false;
        }
    }
}
