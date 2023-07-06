using MonefyClient.Application.DTOs;
using MonefyClient.Application.DTOs.InputDTOs;
using MonefyClient.Application.Services.Abstractions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MonefyClient.Application.Services.Implementations
{
    public class MonefyAccountAppService : IMonefyAccountAppService
    {
        private readonly HttpClient _httpClient;

        public MonefyAccountAppService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateAccount(InputAccountDTO account)
        {
            account.Incomes = new List<InputIncomeDTO>();
            account.Expenses = new List<InputExpenseDTO>();

            var content = new StringContent(JsonConvert.SerializeObject(account), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/", content);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
            }
        }
    }
}
