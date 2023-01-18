using System;
using System.Collections.Generic;

namespace Blazorit.Infrastructure.DBStorages.BlazoritDB.EF.dom;

public partial class ProdProduct
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    /// <summary>
    /// articul
    /// </summary>
    public string Sku { get; set; } = null!;

    public string Curr { get; set; } = null!;

    public decimal Price { get; set; }

    public DateTime DateTimeCreate { get; set; }

    public DateTime DateTimeModified { get; set; }

    public string? Description { get; set; }

    public long? CategoryId { get; set; }

    public string LinkPart { get; set; } = null!;

    public virtual ICollection<CartShopcartList> CartShopcartLists { get; } = new List<CartShopcartList>();

    public virtual ProdCategory? Category { get; set; }

    public virtual ICollection<OrdOrderList> OrdOrderLists { get; } = new List<OrdOrderList>();

    public virtual ICollection<ProdPicture> ProdPictures { get; } = new List<ProdPicture>();

    public virtual ICollection<WishWishList> WishWishLists { get; } = new List<WishWishList>();
}
