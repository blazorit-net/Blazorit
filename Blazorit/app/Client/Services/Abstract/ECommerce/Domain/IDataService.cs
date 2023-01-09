namespace Blazorit.Client.Services.Abstract.ECommerce.Domain {
    public interface IDataService {
        Task<IEnumerable<string>> GetHeaderMenu();
    }
}
