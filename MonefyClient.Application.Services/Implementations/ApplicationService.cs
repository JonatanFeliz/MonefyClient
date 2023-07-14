using Microsoft.Extensions.Configuration;
using MonefyClient.Application.DTOs.InputDTOs;
using MonefyClient.Application.DTOs.Models;
using MonefyClient.Application.DTOs.OutputDTOs;
using MonefyClient.Application.Services.Abstractions;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MonefyClient.Application.Services.Implementations
{
    public class ApplicationService : IMonefyAppService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _log;
        private readonly string _myApi;
        private readonly string? token = Token.UserToken;
        public ApplicationService(HttpClient httpClient, ILogger log, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _log = log;
            _myApi = configuration["MyApi:API"];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        #region User
        public async Task<bool> AddUser(InputUserDTO user)
        {
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_myApi}User/AddUser", content);

            if (response.IsSuccessStatusCode)
            {
                _log.Information($"Create user {user.Name}");
                return true;
            }

            _log.Information($"User not create");
            return false;
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"{_myApi}User/DeleteUser/" + id);

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<OutputUserDTO> GetUser(Guid id)
        {
            var response = await _httpClient.GetAsync($"{_myApi}User/GetUser/" + id);

            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadAsAsync<OutputUserDTO>();
                return user;
            }
            else
            {
                return null;
            }
        }

        public async Task<UserToken?> Login(InputUserDTO user)
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

        public async Task Logout()
        {
            await _httpClient.PostAsync($"{_myApi}User/Logout", null);
        }

        public async Task<bool> UpdateUser(Guid id, InputUserDTO user)
        {
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_myApi}User/UpdateUser/" + id, content);

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        #endregion

        #region Account
        public async Task<bool> AddAccount(InputAccountDTO account)
        {
            var content = new StringContent(JsonConvert.SerializeObject(account), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_myApi}Account/AddAccount", content);

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<bool> DeleteAccount(Guid id)
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
                return null;
            }
        }

        public async Task<IEnumerable<OutputAccountDTO>> GetUserAccounts()
        {
            var response = await _httpClient.GetAsync($"{_myApi}Account/GetUserAccounts");

            if (response.IsSuccessStatusCode)
            {
                var accounts = await response.Content.ReadAsAsync<IEnumerable<OutputAccountDTO>>();
                return accounts;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<OutputAccountDTO>> GetTransactionsPerAccountDate(int year, int month, Guid? accountId= null)
        {
            var sb = new StringBuilder();
            sb.Append($"{_myApi}Account/GetTransactionPerAccountDate/");
            sb.Append(year);
            sb.Append('/');
            sb.Append(month);
            sb.Append("?accountId=");
            sb.Append(accountId);
            var response = await _httpClient.GetAsync(sb.ToString());

            if (response.IsSuccessStatusCode)
            {
                var accounts = await response.Content.ReadAsAsync<IEnumerable<OutputAccountDTO>>();
                return accounts;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> UpdateAccount(Guid id, InputAccountDTO account)
        {
            var content = new StringContent(JsonConvert.SerializeObject(account), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_myApi}Account/UpdateAccount/" + id, content);

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        #endregion

        #region Income
        public async Task<bool> AddIncome(Guid accountId, Guid categoryId, InputIncomeDTO income)
        {
            var content = new StringContent(JsonConvert.SerializeObject(income), Encoding.UTF8, "application/json");
            var sb = new StringBuilder();
            sb.Append($"{_myApi}Income/AddIncome/");
            sb.Append(accountId);
            sb.Append('/');
            sb.Append(categoryId);
            var response = await _httpClient.PostAsync(sb.ToString(), content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<OutputIncomeDTO>> GetUserIncomes()
        {
            var response = await _httpClient.GetAsync($"{_myApi}Account/GetUserAccounts");

            if (response.IsSuccessStatusCode)
            {
                var accounts = await response.Content.ReadAsAsync<IEnumerable<OutputAccountDTO>>();
                IEnumerable<OutputIncomeDTO> incomes = Enumerable.Empty<OutputIncomeDTO>();
                foreach (var account in accounts)
                {
                    if(account.Incomes != null) incomes = incomes.Concat(account.Incomes);
                }
                return incomes;
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                return null;
            }
        }

        public async Task<OutputIncomeDTO> GetIncome(Guid id)
        {
            var response = await _httpClient.GetAsync($"{_myApi}Income/GetIncome/" + id);

            if (response.IsSuccessStatusCode)
            {
                var income = await response.Content.ReadAsAsync<OutputIncomeDTO>();
                return income;
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                return null;
            }
        }

        public async Task<bool> UpdateIncome(Guid id, InputIncomeDTO income)
        {
            var content = new StringContent(JsonConvert.SerializeObject(income), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_myApi}Income/UpdateIncome/" + id, content);

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<bool> DeleteIncome(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"{_myApi}Income/DeleteIncome/" + id);

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }
        #endregion

        #region IncomeCategory
        public async Task<bool> AddIncomeCategory(InputIncomeCategoryDTO category)
        {
            var content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_myApi}IncomeCategory/AddIncomeCategory", content);

            if (response.IsSuccessStatusCode)
            {
                _log.Information($"Create category {category.Name}");
                return true;
            }

            _log.Information($"Category not create");
            return false;
        }

        public async Task<IEnumerable<OutputIncomeCategoryDTO>> GetIncomeCategories()
        {
            var response = await _httpClient.GetAsync($"{_myApi}IncomeCategory/GetIncomeCategories");

            if (response.IsSuccessStatusCode)
            {
                var categories = await response.Content.ReadAsAsync<IEnumerable<OutputIncomeCategoryDTO>>();
                return categories;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeleteIncomeCategory(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"{_myApi}IncomeCategory/DeleteIncomeCategory/" + id);

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }
        #endregion

        #region Expense
        public async Task<bool> AddExpense(Guid accountId, Guid categoryId, InputExpenseDTO expense)
        {
            var content = new StringContent(JsonConvert.SerializeObject(expense), Encoding.UTF8, "application/json");
            var sb = new StringBuilder();
            sb.Append($"{_myApi}Expense/AddExpense/");
            sb.Append(accountId);
            sb.Append('/');
            sb.Append(categoryId);
            var response = await _httpClient.PostAsync(sb.ToString(), content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<OutputExpenseDTO>> GetUserExpenses()
        {
            var response = await _httpClient.GetAsync($"{_myApi}Account/GetUserAccounts");

            if (response.IsSuccessStatusCode)
            {
                var accounts = await response.Content.ReadAsAsync<IEnumerable<OutputAccountDTO>>();
                var expenses = Enumerable.Empty<OutputExpenseDTO>();
                foreach (var account in accounts)
                {
                    if (account.Expenses != null) expenses = expenses.Concat(account.Expenses);
                }
                return expenses;
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                return null;
            }
        }

        public async Task<OutputExpenseDTO> GetExpense(Guid id)
        {
            var response = await _httpClient.GetAsync($"{_myApi}Expense/GetExpense/" + id);

            if (response.IsSuccessStatusCode)
            {
                var expense = await response.Content.ReadAsAsync<OutputExpenseDTO>();
                return expense;
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                return null;
            }
        }

        public async Task<bool> UpdateExpense(Guid id, InputExpenseDTO expense)
        {
            var content = new StringContent(JsonConvert.SerializeObject(expense), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_myApi}Expense/UpdateExpense/" + id, content);

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<bool> DeleteExpense(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"{_myApi}Expense/DeleteExpense/" + id);

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }
        #endregion

        #region ExpenseCategory
        public async Task<bool> AddExpenseCategory(InputExpenseCategoryDTO category)
        {
            var content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_myApi}ExpenseCategory/AddExpenseCategory", content);

            if (response.IsSuccessStatusCode)
            {
                _log.Information($"Create category {category.Name}");
                return true;
            }

            _log.Information($"Category not create");
            return false;
        }

        public async Task<IEnumerable<OutputExpenseCategoryDTO>> GetExpenseCategories()
        {
            var response = await _httpClient.GetAsync($"{_myApi}ExpenseCategory/GetExpenseCategories");

            if (response.IsSuccessStatusCode)
            {
                var categories = await response.Content.ReadAsAsync<IEnumerable<OutputExpenseCategoryDTO>>();
                return categories;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeleteExpenseCategory(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"{_myApi}ExpenseCategory/DeleteExpenseCategory/" + id);

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        #endregion
    }
}
