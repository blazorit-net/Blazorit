using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazorit.SharedKernel.Services.DTO.Identity {
    public class User {
        public long Id { get; set; }
        public string? UserName { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public DateTime DateCreated { get; set; }
        public string? Role { get; set; }

        //public string Email { get; set; }
    }
}
