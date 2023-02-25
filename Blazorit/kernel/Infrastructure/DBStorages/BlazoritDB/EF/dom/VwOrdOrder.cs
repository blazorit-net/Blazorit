using System;
using System.Collections.Generic;

namespace Blazorit.Infrastructure.DBStorages.BlazoritDB.EF.dom;

public partial class VwOrdOrder
{
    public long? OrderId { get; set; }

    public long? UserId { get; set; }

    public long? DeliveryId { get; set; }

    public DateOnly? DateCreate { get; set; }

    public DateTime? DateTimeCreate { get; set; }

    public string? Sku { get; set; }

    public string? Name { get; set; }

    public string? Curr { get; set; }

    public decimal? ProductPrice { get; set; }

    public decimal? OrderPrice { get; set; }

    public int? Quantity { get; set; }
}
