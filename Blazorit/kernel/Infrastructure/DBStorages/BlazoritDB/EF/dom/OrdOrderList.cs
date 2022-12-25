using System;
using System.Collections.Generic;

namespace Blazorit.Infrastructure.DBStorages.BlazoritDB.EF.dom;

public partial class OrdOrderList
{
    public long OrderId { get; set; }

    public long ProductId { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public virtual OrdOrder Order { get; set; } = null!;

    public virtual ProdProduct Product { get; set; } = null!;
}
