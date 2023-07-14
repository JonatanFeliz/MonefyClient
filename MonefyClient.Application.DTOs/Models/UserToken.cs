using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonefyClient.Application.DTOs.Models
{
    public class UserToken
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }

        public UserToken(string id, string name, string token)
        {
            Id = id;
            Name = name;
            Token = token;
        }
    }
}
