using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MonefyClient.Application.DTOs;
using MonefyClient.Application.DTOs.InputDTOs;
using MonefyClient.Application.DTOs.OutputDTOs;
using MonefyClient.Application.Services.Abstractions;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonefyClient.Application.Services.Implementations
{
    public class MonefyUserAppService : IMonefyUserAppService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _log;
        private readonly string _myApi;

        public MonefyUserAppService(HttpClient httpClient, ILogger log, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _log = log;
            _myApi = configuration["MyApi:API"];
        }

        public async Task<UserToken?> ValidateLogin(InputUserDTO user) 
        {
            user.Name = "";

            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_myApi}User/Login", content);

            if (response.IsSuccessStatusCode) 
            {
                var token = await response.Content.ReadAsStringAsync();
                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
                var userId = jwt.Claims.First(c => c.Type == "UserId").Value;
                var name = jwt.Claims.First(c => c.Type == "Name").Value;

                _log.Information($"Valid user");
                return new UserToken(userId, name, token);
            }

            _log.Information($"Invalid user");
            return null;
        }

        public async Task<bool> CreateUser(InputUserDTO user)
        {
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_myApi}User/AddUser", content);

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
