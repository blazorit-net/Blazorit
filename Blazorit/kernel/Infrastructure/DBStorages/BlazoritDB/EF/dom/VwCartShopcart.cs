using System;
using System.Collections.Generic;

namespace Blazorit.Infrastructure.DBStorages.BlazoritDB.EF.dom;

public partial class VwCartShopcart {
    public long? CartId { get; set; }

    public long? UserId { get; set; }

    public DateOnly? DateCreate { get; set; }

    public DateTime? DateTimeCreate { get; set; }

    public long? ProductId { get; set; }

    public string? Sku { get; set; }

    public string? Name { get; set; }

    public string? Curr { get; set; }

    public decimal? ProductPrice { get; set; }

    public string? PicLinkPart { get; set; }

    public int? Quantity { get; set; }

    public DateTime? DateTimeModified { get; set; }
}
