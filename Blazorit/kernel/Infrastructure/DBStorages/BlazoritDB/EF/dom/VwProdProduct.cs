using System;
using System.Collections.Generic;

namespace Blazorit.Infrastructure.DBStorages.BlazoritDB.EF.dom;

public partial class VwProdProduct
{
    public long? Id { get; set; }

    public string? Name { get; set; }

    public string? Sku { get; set; }

    public string? Curr { get; set; }

    public decimal? Price { get; set; }

    public DateOnly? DateCreate { get; set; }

    public DateOnly? DateModified { get; set; }

    public DateTime? DateTimeCreate { get; set; }

    public DateTime? DateTimeModified { get; set; }

    public string? Description { get; set; }

    public string? LinkPart { get; set; }

    public bool? IsOnSite { get; set; }

    public string? Category { get; set; }

    public string? CategoryFullName { get; set; }
}
