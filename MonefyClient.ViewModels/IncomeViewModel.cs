using MonefyClient.Application.DTOs.InputDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonefyClient.ViewModels
{
    public class IncomeViewModel
    {
        public string? Description { get; set; }
        public decimal? Value { get; set; }
        public DateTime? Date { get; set; }
        public InputIncomeCategoryDTO? Category { get; set; }
    }
}
