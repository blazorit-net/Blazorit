namespace Blazorit.SharedKernel.Core.Services.Models.ECommerce.Domain.HeaderMenus
{
    public class SubMenu
    {

        public SubMenu() { }

        public SubMenu(string title, IList<MenuItemGroup> menuItemGroups, IList<MenuItem> menuItems)
        {
            Title = title;
            MenuItemGroups = menuItemGroups;
            MenuItems = menuItems;
        }

        public SubMenu(string title, IList<MenuItemGroup> menuItemGroups)
        {
            Title = title;
            MenuItemGroups = menuItemGroups;
        }

        public SubMenu(string title, IList<MenuItem> menuItems)
        {
            Title = title;
            MenuItems = menuItems;
        }


        public string Title { get; set; } = string.Empty;

        public IList<MenuItemGroup> MenuItemGroups { get; set; } = new List<MenuItemGroup>();

        public IList<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
    }
}
