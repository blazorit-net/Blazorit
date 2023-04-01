﻿namespace Blazorit.Client.Shared.Routes.ECommerce.Domain
{
    /// <summary>
    /// Page link container
    /// </summary>
    public static class ConstPage
    {

        public const string ADMIN_INDEX         = "admin";
        public const string ADMIN_ACCOUNT       = "admin/account";
        public const string ADMIN_LOGIN         = "login";
        public const string ADMIN_REGISTER       = "signup";

        public const string USER_ACCOUNT        = "account";
        public const string USER_LOGIN          = "login";
        public const string USER_REGISTR        = "signup";
        
        public const string SHOPCART            = "shopcart";        
        public const string PRODUCT             = "product";
        public const string CHECKOUT            = "checkout";
        public const string PROCESSED_ORDER     = "processed-order";

        public const string PAYMENT             = "virtual-payment-page"; // this page is for testing (not production)
    }
}
