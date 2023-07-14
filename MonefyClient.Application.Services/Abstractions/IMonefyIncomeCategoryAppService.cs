using MonefyClient.Application.DTOs.InputDTOs;
using MonefyClient.Application.DTOs.OutputDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonefyClient.Application.Services.Abstractions
{
    public interface IMonefyIncomeCategoryAppService
    {
        Task<bool> AddIncomeCategory(InputIncomeCategoryDTO category);
        Task<IEnumerable<OutputIncomeCategoryDTO>> GetIncomeCategories();
        Task<bool> DeleteIncomeCategory(Guid id);
    }
}
