using MonefyClient.Application.DTOs.OutputDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonefyClient.ViewModels.OutputViewModels
{
    public class OutputAccountViewModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        = string.Empty;
        public string Currency { get; set; }
        = string.Empty;
        public decimal Balance { get; set; }
        public IEnumerable<OutputIncomeDTO> Incomes { get; set; }
        = Enumerable.Empty<OutputIncomeDTO>();
        public IEnumerable<OutputExpenseDTO> Expenses { get; set; }
        = Enumerable.Empty<OutputExpenseDTO>();
    }
}
