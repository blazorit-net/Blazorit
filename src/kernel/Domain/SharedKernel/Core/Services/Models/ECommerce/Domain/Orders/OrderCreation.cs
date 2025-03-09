namespace Blazorit.Domain.SharedKernel.Core.Services.Models.ECommerce.Domain.Orders
{
    /// <summary>
    /// Data to create an order
    /// </summary>
    public class PaidOrder
    {
        public long UserId { get; set; }

        public string OrderToken { get; set; } = string.Empty;

        public decimal PaidAmount { get; set; }
        
        public string PaymentInfo { get; set; } = string.Empty;
    }
}
