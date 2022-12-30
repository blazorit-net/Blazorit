using System;
using System.Collections.Generic;

namespace Blazorit.Infrastructure.DBStorages.BlazoritDB.EF.dom;

public partial class WishWishList
{
    public long WishId { get; set; }

    public long ProductId { get; set; }

    public DateTime DateTimeCreate { get; set; }

    public virtual ProdProduct Product { get; set; } = null!;

    public virtual WishWish Wish { get; set; } = null!;
}
