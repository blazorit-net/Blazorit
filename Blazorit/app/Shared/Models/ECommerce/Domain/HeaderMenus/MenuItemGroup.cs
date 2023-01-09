namespace Blazorit.Shared.Models.ECommerce.Domain.HeaderMenus
{
    public class MenuItemGroup
    {
        public MenuItemGroup(string title, IList<MenuItem> menuItems, IList<SubMenu> subMenus)
        {
            Title = title;
            MenuItems = menuItems;
            SubMenus = subMenus;
        }

        public MenuItemGroup(string title, IList<MenuItem> menuItems)
        {
            Title = title;
            MenuItems = menuItems;
        }

        public MenuItemGroup(string title, IList<SubMenu> subMenus)
        {
            Title = title;
            SubMenus = subMenus;
        }


        public string Title { get; private set; }

        public IList<MenuItem> MenuItems { get; private set; } = new List<MenuItem>();

        public IList<SubMenu> SubMenus { get; private set; } = new List<SubMenu>();
    }
}
