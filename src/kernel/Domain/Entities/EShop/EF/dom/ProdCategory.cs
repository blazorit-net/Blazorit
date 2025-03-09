using System;
using System.Collections.Generic;
using Blazorit.Domain.Entities.Common;

namespace Blazorit.Domain.Entities.EShop.EF.dom;

public partial class ProdCategory : BaseIdEntity
{

    public string Name { get; set; } = null!;

    public string? PrefixSku { get; set; }

    public string FullName { get; set; } = null!;

    public virtual ICollection<ProdProduct> ProdProducts { get; } = new List<ProdProduct>();
}
