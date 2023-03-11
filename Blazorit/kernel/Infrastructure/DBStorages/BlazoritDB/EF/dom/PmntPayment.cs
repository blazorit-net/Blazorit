using System;
using System.Collections.Generic;

namespace Blazorit.Infrastructure.DBStorages.BlazoritDB.EF.dom;

public partial class PmntPayment
{
    public long Id { get; set; }

    public decimal PaymentAmount { get; set; }

    public DateTime DateTimeCreate { get; set; }

    public virtual OrdOrder? OrdOrder { get; set; }
}
