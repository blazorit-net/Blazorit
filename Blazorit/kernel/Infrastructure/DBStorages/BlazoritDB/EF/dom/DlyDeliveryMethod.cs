using System;
using System.Collections.Generic;

namespace Blazorit.Infrastructure.DBStorages.BlazoritDB.EF.dom;

public partial class DlyDeliveryMethod
{
    public long Id { get; set; }

    public string Method { get; set; } = null!;

    public bool EnterAddress { get; set; }

    public virtual ICollection<DlyMethodsAddress> DlyMethodsAddresses { get; } = new List<DlyMethodsAddress>();

    public virtual ICollection<DlyUserDelivery> DlyUserDeliveries { get; } = new List<DlyUserDelivery>();
}
