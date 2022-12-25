using System;
using System.Collections.Generic;

namespace Blazorit.Infrastructure.DBStorages.BlazoritDB.EF.dom;

public partial class WishWishlistList
{
    public long WishId { get; set; }

    public long ProductId { get; set; }

    public int? Quantity { get; set; }

    public virtual ProdProduct Product { get; set; } = null!;

    public virtual WishWishlist Wish { get; set; } = null!;
}
