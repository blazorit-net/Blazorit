using System;
using System.Collections.Generic;

namespace Blazorit.Infrastructure.DBStorages.BlazoritDB.EF.dom;

public partial class CartShopcartList
{
    public long CartId { get; set; }

    public long ProductId { get; set; }

    public int Quantity { get; set; }

    public DateTime DateTimeModified { get; set; }

    public virtual CartShopcart Cart { get; set; } = null!;

    public virtual ProdProduct Product { get; set; } = null!;
}
