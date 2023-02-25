using System;
using System.Collections.Generic;

namespace Blazorit.Infrastructure.DBStorages.BlazoritDB.EF.dom;

public partial class VwDlyUserDelivery
{
    public long? Id { get; set; }

    public long? UserId { get; set; }

    public string? Method { get; set; }

    public string? Address { get; set; }

    public string? Comment { get; set; }

    public DateTime? DateTimeCreated { get; set; }
}
