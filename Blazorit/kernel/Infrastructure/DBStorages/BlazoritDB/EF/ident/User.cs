
namespace Blazorit.Infrastructure.DBStorages.BlazoritDB.EF.ident
{
    public partial class User {
        public long Id { get; set; }

        public string UserName { get; set; } = null!;

        public byte[] PasswordHash { get; set; } = null!;

        public byte[] PasswordSalt { get; set; } = null!;

        public DateTime DateCreated { get; set; }

        public string UserRole { get; set; } = null!;
    }
}
