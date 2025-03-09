using System;
using System.Collections.Generic;
using Blazorit.Domain.Entities.Common;

namespace Blazorit.Domain.Entities.EShop.EF.dom;

public partial class PmntPaymentMethod : BaseIdEntity
{

    public string Method { get; set; } = null!;

    /// <summary>
    /// Is Cash On Delivery
    /// </summary>
    public bool IsCod { get; set; }

    public long Ordby { get; set; }

    public virtual ICollection<PmntPayment> PmntPayments { get; } = new List<PmntPayment>();
}
