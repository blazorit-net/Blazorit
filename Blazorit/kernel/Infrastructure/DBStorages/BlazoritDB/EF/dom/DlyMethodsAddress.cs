using System;
using System.Collections.Generic;

namespace Blazorit.Infrastructure.DBStorages.BlazoritDB.EF.dom;

/// <summary>
/// The table is for shipping methods where the address offered by the system is common to all users
/// </summary>
public partial class DlyMethodsAddress
{
    public long Id { get; set; }

    public long MethodId { get; set; }

    public long AddressId { get; set; }

    public virtual DlyDeliveryAddress Address { get; set; } = null!;

    public virtual DlyDeliveryMethod Method { get; set; } = null!;
}
