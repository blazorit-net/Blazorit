using Microsoft.AspNetCore.Components;

namespace Blazorit.Client.Base.Components {
    public interface IBlazoritComponent {
        [Parameter] string? Class { get; set; }

        string GetClass(string? classes) {
            return Support.Components.Methods.GetClass(this.Class, classes);
        }
    }


}
