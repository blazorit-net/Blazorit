using System;
using System.Collections.Generic;
using Blazorit.Domain.Entities.Common;

namespace Blazorit.Domain.Entities.EShop.EF.dom;

public partial class DlyDelivery : BaseIdEntity
{
    public DateTime DateTimeCreate { get; set; }

    public long UserDeliveryId { get; set; }

    public decimal DeliveryCost { get; set; }

    public DateOnly? DeliveryDate { get; set; }

    public DateTimeOffset? DeliveryTimeStart { get; set; }

    public DateTimeOffset? DeliveryTimeEnd { get; set; }

    public virtual OrdOrder? OrdOrder { get; set; }

    public virtual DlyUserDelivery UserDelivery { get; set; } = null!;
}
