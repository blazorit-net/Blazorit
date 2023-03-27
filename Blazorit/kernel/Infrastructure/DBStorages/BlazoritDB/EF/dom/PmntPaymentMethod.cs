using System;
using System.Collections.Generic;

namespace Blazorit.Infrastructure.DBStorages.BlazoritDB.EF.dom;

public partial class PmntPaymentMethod
{
    public long Id { get; set; }

    public string Method { get; set; } = null!;

    /// <summary>
    /// Is Cash On Delivery
    /// </summary>
    public bool IsCod { get; set; }

    public long Ordby { get; set; }

    public virtual ICollection<PmntPayment> PmntPayments { get; } = new List<PmntPayment>();
}
