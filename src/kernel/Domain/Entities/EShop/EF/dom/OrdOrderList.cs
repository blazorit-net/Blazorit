using System;
using System.Collections.Generic;
using Blazorit.Domain.Entities.Common;

namespace Blazorit.Domain.Entities.EShop.EF.dom;

public partial class OrdOrderList : BaseIdEntity
{

    public long OrderId { get; set; }
    public long ProductId { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public virtual OrdOrder Order { get; set; } = null!;

    public virtual ProdProduct Product { get; set; } = null!;
}
