using System;
using System.Collections.Generic;

namespace Blazorit.Infrastructure.DBStorages.BlazoritDB.EF.dom;

public partial class VwDlyMethodsAddress
{
    public long? Id { get; set; }

    public long? MethodId { get; set; }

    public long? AddressId { get; set; }

    public string? Method { get; set; }

    public string? Address { get; set; }

    public string? Comment { get; set; }
}
