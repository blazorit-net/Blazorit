using System;
using System.Collections.Generic;
using Blazorit.Domain.Entities.Common;

namespace Blazorit.Domain.Entities.EShop.EF.dom;

public partial class WishWish : BaseIdEntity
{

    public long UserId { get; set; }

    public DateTime DateTimeCreate { get; set; }

    public virtual ICollection<WishWishList> WishWishLists { get; } = new List<WishWishList>();
}
