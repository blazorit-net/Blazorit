using System;
using System.Collections.Generic;
using Blazorit.Domain.Entities.Common;

namespace Blazorit.Domain.Entities.EShop.EF.dom;

public partial class OrdOrder : BaseIdEntity
{

    public long UserId { get; set; }

    public DateTime DateTimeCreate { get; set; }

    public long DeliveryId { get; set; }

    public long PaymentId { get; set; }

    public virtual DlyDelivery Delivery { get; set; } = null!;

    public virtual ICollection<OrdOrderList> OrdOrderLists { get; } = new List<OrdOrderList>();

    public virtual PmntPayment Payment { get; set; } = null!;
}
