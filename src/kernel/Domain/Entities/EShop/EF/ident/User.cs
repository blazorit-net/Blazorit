
using System.ComponentModel.DataAnnotations;
using Blazorit.Domain.Entities.Common;

namespace Blazorit.Domain.Entities.EShop.EF.ident
{
    public partial class User : BaseIdEntity
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
