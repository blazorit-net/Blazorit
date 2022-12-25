using System;
using System.Collections.Generic;

namespace Blazorit.Infrastructure.DBStorages.BlazoritDB.EF.dom;

public partial class WishWishlist
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public DateTime DateTimeCreate { get; set; }

    public virtual ICollection<WishWishlistList> WishWishlistLists { get; } = new List<WishWishlistList>();
}
