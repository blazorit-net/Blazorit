using System;
using System.Collections.Generic;
using Blazorit.Domain.Entities.Common;

namespace Blazorit.Domain.Entities.EShop.EF.dom;

public partial class DlyDeliveryAddress : BaseIdEntity
{
    public string Address { get; set; } = null!;

    public string? Comment { get; set; }

    public DateTime DateTimeCreated { get; set; }

    public virtual ICollection<DlyMethodsAddress> DlyMethodsAddresses { get; } = new List<DlyMethodsAddress>();

    public virtual ICollection<DlyUserDelivery> DlyUserDeliveries { get; } = new List<DlyUserDelivery>();
}
