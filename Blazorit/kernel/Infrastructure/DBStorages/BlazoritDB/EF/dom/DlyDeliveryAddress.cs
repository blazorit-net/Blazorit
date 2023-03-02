using System;
using System.Collections.Generic;

namespace Blazorit.Infrastructure.DBStorages.BlazoritDB.EF.dom;

public partial class DlyDeliveryAddress
{
    public long Id { get; set; }

    public string Address { get; set; } = null!;

    public string? Comment { get; set; }

    public DateTime DateTimeCreated { get; set; }

    public virtual ICollection<DlyMethodsAddress> DlyMethodsAddresses { get; } = new List<DlyMethodsAddress>();

    public virtual ICollection<DlyUserDelivery> DlyUserDeliveries { get; } = new List<DlyUserDelivery>();
}
