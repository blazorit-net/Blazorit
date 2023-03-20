using System;
using System.Collections.Generic;

namespace Blazorit.Infrastructure.DBStorages.BlazoritDB.EF.dom;

public partial class VwDlyDelivery
{
    public long? Id { get; set; }

    public DateTime? DateTimeCreate { get; set; }

    public long? UserId { get; set; }

    public long? MethodId { get; set; }

    public long? AddressId { get; set; }

    public string? Method { get; set; }

    public string? Address { get; set; }

    public string? Comment { get; set; }

    public decimal? DeliveryCost { get; set; }

    public DateOnly? DeliveryDate { get; set; }

    public DateTimeOffset? DeliveryTimeStart { get; set; }

    public DateTimeOffset? DeliveryTimeEnd { get; set; }
}
