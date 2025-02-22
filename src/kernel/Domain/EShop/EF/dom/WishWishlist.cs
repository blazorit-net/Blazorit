using System;
using System.Collections.Generic;
using Blazorit.Domain.Common;

namespace Blazorit.Domain.EShop.EF.dom;

public partial class WishWishList : BaseEntity
{
    public long WishId { get; set; }

    public long ProductId { get; set; }

    public DateTime DateTimeCreate { get; set; }

    public virtual ProdProduct Product { get; set; } = null!;

    public virtual WishWish Wish { get; set; } = null!;
}
