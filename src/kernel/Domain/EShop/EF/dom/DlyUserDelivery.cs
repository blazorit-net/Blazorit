using System;
using System.Collections.Generic;
using Blazorit.Domain.Common;

namespace Blazorit.Domain.EShop.EF.dom;

public partial class DlyUserDelivery : BaseIdEntity
{

    public long UserId { get; set; }

    public long MethodId { get; set; }

    public long AddressId { get; set; }

    public DateTime DateTimeCreated { get; set; }

    public virtual DlyDeliveryAddress Address { get; set; } = null!;

    public virtual ICollection<DlyDelivery> DlyDeliveries { get; } = new List<DlyDelivery>();

    public virtual DlyDeliveryMethod Method { get; set; } = null!;
}
