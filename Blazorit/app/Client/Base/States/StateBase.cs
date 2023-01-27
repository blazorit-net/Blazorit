using Blazorit.Client.Models.ECommerce.Domain.Carts;

namespace Blazorit.Client.Base.States {
    /// <summary>
    /// Base class for state containers
    /// some description here about state containers: https://learn.microsoft.com/en-us/aspnet/core/blazor/state-management?view=aspnetcore-7.0&pivots=server
    /// https://learn.microsoft.com/ru-ru/aspnet/core/blazor/state-management?view=aspnetcore-7.0&pivots=server
    /// </summary>
    /// <typeparam name="T">Model type</typeparam>
    public abstract class StateBase<T> where T: class, new() {
        private T state = new();

        public T State {
            get {
                return state;
            }

            set {
                state = value;
                NotifyStateChanged();
            }
        }

        public event Action? OnChange;

        public void NotifyStateChanged() => OnChange?.Invoke();
    }
}
