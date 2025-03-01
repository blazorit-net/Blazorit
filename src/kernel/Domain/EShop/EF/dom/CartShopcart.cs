using System;
using System.Collections.Generic;
using Blazorit.Domain.Common;

namespace Blazorit.Domain.EShop.EF.dom;

public partial class CartShopcart : BaseIdEntity
{
    public long UserId { get; set; }

    public DateTime? DateTimeCreate { get; set; }

    public virtual ICollection<CartShopcartList> CartShopcartLists { get; } = new List<CartShopcartList>();
}
