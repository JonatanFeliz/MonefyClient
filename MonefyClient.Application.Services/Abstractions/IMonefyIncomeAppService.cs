using MonefyClient.Application.DTOs.InputDTOs;
using MonefyClient.Application.DTOs.OutputDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonefyClient.Application.Services.Abstractions
{
    public interface IMonefyIncomeAppService
    {
        Task<bool> AddIncome(Guid accountId, Guid categoryId, InputIncomeDTO income);
        Task<IEnumerable<OutputIncomeDTO>> GetUserIncomes();
        Task<OutputIncomeDTO> GetIncome(Guid id);
        Task<bool> UpdateIncome(Guid id, InputIncomeDTO income);
        Task<bool> DeleteIncome(Guid id);
    }
}
