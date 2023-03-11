using System;
using System.Collections.Generic;

namespace Blazorit.Infrastructure.DBStorages.BlazoritDB.EF.dom;

public partial class OrdOrder
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public DateTime DateTimeCreate { get; set; }

    public long DeliveryId { get; set; }

    public long PaymentId { get; set; }

    public virtual DlyUserDelivery Delivery { get; set; } = null!;

    public virtual ICollection<OrdOrderList> OrdOrderLists { get; } = new List<OrdOrderList>();

    public virtual PmntPayment Payment { get; set; } = null!;
}
