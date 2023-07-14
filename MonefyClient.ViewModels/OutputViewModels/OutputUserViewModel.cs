using MonefyClient.Application.DTOs.OutputDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonefyClient.ViewModels.OutputViewModels
{
    public class OutputUserViewModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        = string.Empty;
        public string Email { get; set; }
        = string.Empty;
        public string Password { get; set; }
        = string.Empty;
        public decimal Balance { get; set; }
        public IEnumerable<OutputAccountDTO> Accounts { get; set; }
        = Enumerable.Empty<OutputAccountDTO>();
    }
}
