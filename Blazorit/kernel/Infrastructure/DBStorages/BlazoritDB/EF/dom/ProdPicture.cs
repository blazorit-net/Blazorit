using System;
using System.Collections.Generic;

namespace Blazorit.Infrastructure.DBStorages.BlazoritDB.EF.dom;

public partial class ProdPicture
{
    public long Id { get; set; }

    public string LinkPart { get; set; } = null!;

    public long ProductId { get; set; }

    public string PicSize { get; set; } = null!;

    public short OrderNum { get; set; }

    public string SiteLocation { get; set; } = null!;

    public virtual ProdProduct Product { get; set; } = null!;
}
