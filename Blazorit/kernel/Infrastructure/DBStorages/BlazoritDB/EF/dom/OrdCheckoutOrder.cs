﻿using System;
using System.Collections.Generic;

namespace Blazorit.Infrastructure.DBStorages.BlazoritDB.EF.dom;

/// <summary>
/// this table need for temporary storage info about order, while payment is being made
/// </summary>
public partial class OrdCheckoutOrder
{
    public long Id { get; set; }

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
