using System;
using System.Collections.Generic;

namespace Blazorit.Infrastructure.DBStorages.BlazoritDB.EF.dom;

public partial class DlyDelivery
{
    public long Id { get; set; }

    public DateTime DateTimeCreate { get; set; }

    public long UserDeliveryId { get; set; }

    public decimal DeliveryCost { get; set; }

    public DateOnly? DeliveryDate { get; set; }

    public DateTimeOffset? DeliveryTimeStart { get; set; }

    public DateTimeOffset? DeliveryTimeEnd { get; set; }

    public virtual OrdOrder? OrdOrder { get; set; }

    public virtual DlyUserDelivery UserDelivery { get; set; } = null!;
}
