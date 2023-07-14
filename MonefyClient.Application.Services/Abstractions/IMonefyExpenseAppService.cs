using MonefyClient.Application.DTOs.InputDTOs;
using MonefyClient.Application.DTOs.OutputDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonefyClient.Application.Services.Abstractions
{
    public interface IMonefyExpenseAppService
    {
        Task<bool> AddExpense(Guid accountId, InputExpenseDTO expense);
        Task<IEnumerable<OutputExpenseDTO>> GetUserExpenses();
        Task<OutputExpenseDTO> GetExpense(Guid id);
        Task<bool> UpdateExpense(Guid id, InputExpenseDTO expense);
        Task<bool> DeleteExpense(Guid id);
    }
}
