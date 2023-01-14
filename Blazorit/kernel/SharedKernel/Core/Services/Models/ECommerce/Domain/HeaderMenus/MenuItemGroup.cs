namespace Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.HeaderMenus
{
    public class MenuItemGroup
    {
        public MenuItemGroup() { }

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


        public string Title { get; set; } = string.Empty;

        public IList<MenuItem> MenuItems { get; set; } = new List<MenuItem>();

        public IList<SubMenu> SubMenus { get; set; } = new List<SubMenu>();
    }
}
