namespace Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.HeaderMenus
{
    public class MenuItem
    {

        public MenuItem() { }

        public MenuItem(string title, string link)
        {
            Title = title;
            Link = link;
        }


        public string Title { get; set; } = string.Empty;

        public string Link { get; set; } = string.Empty;
    }
}
