using MonefyClient.Application.DTOs.InputDTOs;
using MonefyClient.Application.DTOs.OutputDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonefyClient.Application.Services.Abstractions
{
    public interface IMonefyExpenseCategoryAppService
    {
        Task<bool> AddExpenseCategory(InputExpenseCategoryDTO category);
        Task<IEnumerable<OutputExpenseCategoryDTO>> GetExpenseCategories();
        Task<bool> DeleteExpenseCategory(Guid id);
    }
}
