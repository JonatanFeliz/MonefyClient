using MonefyClient.Application.DTOs.InputDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonefyClient.ViewModels.InputViewModels
{
    public class InputExpenseViewModel
    {
        public string? Description { get; set; }
        public decimal? Value { get; set; }
        public DateTime? Date { get; set; }
        public InputExpenseCategoryDTO? Category { get; set; }
        public Guid AccountId { get; set; }
    }
}
