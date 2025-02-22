
using System.ComponentModel.DataAnnotations;
using Blazorit.Domain.Common;

namespace Blazorit.Domain.EShop.EF.ident
{
    public partial class User : BaseEntity
    {
        [StringLength(50)]
        public string UserName { get; set; } = null!;

        public byte[] PasswordHash { get; set; } = null!;

        public byte[] PasswordSalt { get; set; } = null!;

        public DateTime DateCreated { get; set; }

        [StringLength(100)]
        public string UserRole { get; set; } = null!;
    }
}
