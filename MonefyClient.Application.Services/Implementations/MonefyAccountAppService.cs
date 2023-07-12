using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MonefyClient.Application.DTOs;
using MonefyClient.Application.DTOs.InputDTOs;
using MonefyClient.Application.DTOs.OutputDTOs;
using MonefyClient.Application.Services.Abstractions;
using MonefyClient.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MonefyClient.Application.Services.Implementations
{
    public class MonefyAccountAppService : IMonefyAccountAppService
    {
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;
        private readonly string _myApi;
        private readonly string? token = Token.UserToken;

        public MonefyAccountAppService(HttpClient httpClient, IMapper mapper, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _mapper = mapper;
            _myApi = configuration["MyApi:API"];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<bool> CreateAccount(InputAccountDTO account)
        {
            var content = new StringContent(JsonConvert.SerializeObject(account), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_myApi}Account/AddAccount", content);

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<bool> Delete(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"{_myApi}Account/DeleteAccount/" + id);

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<OutputAccountDTO> GetAccount(Guid id)
        {
            var response = await _httpClient.GetAsync($"{_myApi}Account/GetAccount/" + id);

            if (response.IsSuccessStatusCode)
            {
                var account = await response.Content.ReadAsAsync<OutputAccountDTO>();
                return account;
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                return null;
            }
        }

        public async Task<IEnumerable<OutputAccountDTO>> GetAccounts()
        {
            var response = await _httpClient.GetAsync($"{_myApi}Account/GetUserAccounts");

            if (response.IsSuccessStatusCode)
            {
                var accounts = await response.Content.ReadAsAsync<IEnumerable<OutputAccountDTO>>();
                foreach (var account in accounts)
                {
                    Console.WriteLine($"{account.Id}, {account.Name}, {account.Currency}, {account.Balance}, {account.CreatedAt.ToString("dd-MMM-yyyy")}, {account.Incomes.ToList()}, {account.Expenses.ToList()},");
                }
                //var mapped = _mapper.Map<IEnumerable<AccountViewModel>>(accounts);
                return accounts;
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                return null;
            }

        }

        public async Task<bool> Update(Guid id, InputAccountDTO account)
        {
            var content = new StringContent(JsonConvert.SerializeObject(account), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_myApi}Account/UpdateAccount/" + id, content);

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }
    }
}
