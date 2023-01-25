using Microsoft.AspNetCore.Components;

namespace Blazorit.Client.Base.Components {
    public interface IBlazoritComponent {
        [Parameter] string? Class { get; set; }

        string GetClass(string? classes) {
            return Support.Comps.Methods.GetClass(this.Class, classes);
        }
    }


}
