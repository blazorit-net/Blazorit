using System;
using System.Collections.Generic;
using Blazorit.Domain.Common;

namespace Blazorit.Domain.EShop.EF.dom;

public partial class WishWish : BaseEntity
{

    public long UserId { get; set; }

    public DateTime DateTimeCreate { get; set; }

    public virtual ICollection<WishWishList> WishWishLists { get; } = new List<WishWishList>();
}
