using System;
using System.Collections.Generic;

namespace Blazorit.Infrastructure.DBStorages.BlazoritDB.EF.dom;

public partial class ProdCategory
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string? PrefixSku { get; set; }

    public virtual ICollection<ProdProduct> ProdProducts { get; } = new List<ProdProduct>();
}
