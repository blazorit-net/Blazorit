using System;
using System.Collections.Generic;
using Blazorit.Domain.Common;

namespace Blazorit.Domain.EShop.EF.dom;

public partial class DlyDeliveryMethod : BaseIdEntity
{
    public string Method { get; set; } = null!;

    public bool EnterAddress { get; set; }

    public virtual ICollection<DlyMethodsAddress> DlyMethodsAddresses { get; } = new List<DlyMethodsAddress>();

    public virtual ICollection<DlyUserDelivery> DlyUserDeliveries { get; } = new List<DlyUserDelivery>();
}
