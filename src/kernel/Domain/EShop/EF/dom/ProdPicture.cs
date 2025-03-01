using System;
using System.Collections.Generic;
using Blazorit.Domain.Common;

namespace Blazorit.Domain.EShop.EF.dom;

public partial class ProdPicture : BaseIdEntity
{

    public string LinkPart { get; set; } = null!;

    public long ProductId { get; set; }

    public string PicSize { get; set; } = null!;

    public short OrderNum { get; set; }

    public string SiteLocation { get; set; } = null!;

    public virtual ProdProduct Product { get; set; } = null!;
}
