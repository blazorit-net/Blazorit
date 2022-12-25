﻿using System;
using System.Collections.Generic;

namespace Blazorit.Infrastructure.DBStorages.BlazoritDB.EF.dom;

public partial class ProdProduct
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    /// <summary>
    /// articul
    /// </summary>
    public string? Sku { get; set; }

    public string Curr { get; set; } = null!;

    public decimal Price { get; set; }

    public DateTime DateTimeCreate { get; set; }

    public DateTime DateTimeModified { get; set; }

    public virtual ICollection<CartShopcartList> CartShopcartLists { get; } = new List<CartShopcartList>();

    public virtual ICollection<OrdOrderList> OrdOrderLists { get; } = new List<OrdOrderList>();

    public virtual ICollection<WishWishlistList> WishWishlistLists { get; } = new List<WishWishlistList>();
}