﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonefyClient.Application.DTOs.OutputDTOs
{
    public class OutputIncomeDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; }
        = string.Empty;
        public decimal Value { get; set; }
        public DateTime? Date { get; set; }
        public OutputIncomeCategoryDTO? Category { get; set; }
    }
}
