using System;
using System.Collections.Generic;

namespace Blazorit.Infrastructure.DBStorages.BlazoritDB.EF.dom;

public partial class PmntPayment
{
    public long Id { get; set; }

    public decimal PaymentAmount { get; set; }

    public DateTime DateTimeCreate { get; set; }

    /// <summary>
    /// this field relation to id of ord_checkout_orders, but rows in ord_checkout_orders table can be deleted (whose canceled), than we have not Foreign key to ord_checkout_orders
    /// </summary>
    public long CheckoutOrderId { get; set; }

    public string OrderToken { get; set; } = null!;

    public string? PaymentInfo { get; set; }

    public bool IsPaid { get; set; }

    public long PaymentMethodId { get; set; }

    public virtual OrdOrder? OrdOrder { get; set; }

    public virtual PmntPaymentMethod PaymentMethod { get; set; } = null!;
}
