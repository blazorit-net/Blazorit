namespace Blazorit.Shared.Models.ECommerce.Domain.HeaderMenus
{
    public class MenuItem
    {

        public MenuItem(string title, string link)
        {
            Title = title;
            Link = link;
        }


        public string Title { get; private set; }

        public string Link { get; private set; }
    }
}
