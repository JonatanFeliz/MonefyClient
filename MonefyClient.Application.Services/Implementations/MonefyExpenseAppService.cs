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
    public class MonefyExpenseAppService : IMonefyExpenseAppService
    {
        private readonly HttpClient _httpClient;

        public MonefyExpenseAppService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateExpense(Guid accountId, InputExpenseDTO expense)
        {
            var content = new StringContent(JsonConvert.SerializeObject(expense), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:7021/Expense/" + accountId, content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
