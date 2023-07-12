using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonefyClient.ViewModels
{
    public class OutputAccountViewModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Currency { get; set; }
        public decimal Balance { get; set; }
    }
}
