using System;
using System.Collections.Generic;
using Blazorit.Domain.Common;

namespace Blazorit.Domain.EShop.EF.dom;

public partial class CartShopcartList : BaseEntity
{
    public long CartId { get; set; }

    public long ProductId { get; set; }

    public int Quantity { get; set; }

    public DateTime DateTimeCreated { get; set; }

    public virtual CartShopcart Cart { get; set; } = null!;

    public virtual ProdProduct Product { get; set; } = null!;
}
