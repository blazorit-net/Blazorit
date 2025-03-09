namespace Blazorit.Domain.SharedKernel.Core.Services.Models.ECommerce.Domain.Deliveries
{
    /// <summary>
    /// Delivery cost
    /// </summary>
    public class DeliveryCost
    {
        public DeliveryCost() { }

        public DeliveryCost(decimal cost)
        {
            this.Cost = cost;
        }


        public decimal Cost { get; set; }

        /// <summary>
        /// Total cost for delivery address (or delivery method)
        /// </summary>
        public decimal TotalCost 
        { 
            get
            {
                return Cost;
            }
        }


        public string StrTotalCost
        {
            get
            {
                return TotalCost.ToString("N0");
            }
        }
    }
}
