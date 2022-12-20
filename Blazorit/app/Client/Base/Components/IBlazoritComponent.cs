using Microsoft.AspNetCore.Components;

namespace Blazorit.Client.Base.Components {
    public interface IBlazoritComponent {
        string? Class { get; set; }

        string GetClass(string? classes) {
            return Support.Comp.Methods.GetClass(this.Class, classes);
        }
    }


}
