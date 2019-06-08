using Innowave.FreedomAdmin.DataContext.Interfaces;
using Innowave.FreedomAdmin.DataModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using Innowave.FreedomAdmin.DataModel.Model;
using System.Linq;

namespace Innowave.FreedomAdmin.DataContext.Repositories
{
    public class MenuRepository : RepositoryBase, IMenuRepository
    {
        public MenuRepository(IDbTransaction transaction) : base(transaction)
        {

        }

        public async Task<MenusByUsers> GetMenusByUsers(Guid userId, Guid? productId)
        {
            if (!productId.HasValue)
            {
                var menus = await Connection.QueryAsync<Menus>($"SELECT * FROM Menus ORDER BY OrderNo ASC", transaction: Transaction);
                var subMenus = await Connection.QueryAsync<SubMenus>($"SELECT * FROM SubMenus", transaction: Transaction);

                List<Menus> menuItems = new List<Menus>();
                List<SubMenus> SubMenuItems ;
                Menus menuItem;

                foreach (var menu in menus)
                {
                    menuItem = new Menus();
                    SubMenuItems = new List<SubMenus>();
                    menuItem = menu;

                    var subMenu = subMenus.Where(c => c.MenuId == menu.Id)
                        .OrderBy(c => c.OrderNo).ToList();

                    SubMenuItems.AddRange(subMenu);
                    menuItem.SubMenus = SubMenuItems;
                    menuItems.Add(menuItem);
                }
                var result = new MenusByUsers()
                {
                    Menus = menuItems
                };

                return result;
            }
            return null;
        }
    }
}
