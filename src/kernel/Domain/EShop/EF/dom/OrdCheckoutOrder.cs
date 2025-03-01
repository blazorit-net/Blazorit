using System;
using System.Collections.Generic;
using Blazorit.Domain.Common;

namespace Blazorit.Domain.EShop.EF.dom;

/// <summary>
/// this table need for temporary storage info about order, while payment is being made
/// </summary>
public partial class OrdCheckoutOrder : BaseIdEntity
{

    public DateTime DateTimeCreated { get; set; }

    /// <summary>
    /// uniq token
    /// </summary>
    public string OrderToken { get; set; } = null!;

    /// <summary>
    /// if this field is canceled, than you can delete this row from table.
    /// </summary>
    public bool? Canceled { get; set; }

    public decimal PaymentAmount { get; set; }

    public long UserId { get; set; }

    public long DeliveryId { get; set; }

    public long PaymentMethodId { get; set; }
}
