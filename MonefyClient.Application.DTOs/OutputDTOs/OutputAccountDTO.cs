using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonefyClient.Application.DTOs.OutputDTOs
{
    public class OutputAccountDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        = string.Empty;
        public string Currency { get; set; }
        = string.Empty;
        public decimal Balance { get; set; }
        public IEnumerable<OutputIncomeDTO>? Incomes { get; set; }
        public IEnumerable<OutputExpenseDTO>? Expenses { get; set; }
    }
}
