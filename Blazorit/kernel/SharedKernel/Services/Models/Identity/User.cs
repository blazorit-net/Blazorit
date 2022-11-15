
namespace Blazorit.SharedKernel.Services.Models.Identity {
    public class User {
        public long Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = { 0 }; 
        public byte[] PasswordSalt { get; set; } = { 0 };
        public DateTime DateCreated { get; set; }
        public string Role { get; set; } = string.Empty;

        //public string Email { get; set; }
    }
}
