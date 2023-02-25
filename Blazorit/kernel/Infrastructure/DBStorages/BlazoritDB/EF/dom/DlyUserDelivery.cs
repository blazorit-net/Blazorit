using System;
using System.Collections.Generic;

namespace Blazorit.Infrastructure.DBStorages.BlazoritDB.EF.dom;

public partial class DlyUserDelivery
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long MethodId { get; set; }

    public long AddressId { get; set; }

    public DateTime DateTimeCreated { get; set; }

    public virtual DlyDeliveryAddress Address { get; set; } = null!;

    public virtual DlyDeliveryMethod Method { get; set; } = null!;

    public virtual ICollection<OrdOrder> OrdOrders { get; } = new List<OrdOrder>();
}
