using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonefyClient.Application.DTOs.InputDTOs
{
    public class InputExpenseDTO
    {
        public string Description { get; set; }
        = string.Empty;
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
        public InputExpenseCategoryDTO? Category { get; set; }
    }
}
