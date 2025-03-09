namespace Blazorit.Domain.SharedKernel.Infrastructure.Repositories.Models.ECommerce.Admin.Products
{
    public class Category
    {
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string PrefixSku { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;
    }
}
