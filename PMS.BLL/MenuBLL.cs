using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.Models.DModels;
using PMS.DAL;

namespace PMS.BLL
{
   public class MenuBLL:BaseBLL<MenuInfoModel>
    {
        MenuDAL menuDAL = new MenuDAL();

        public List<MenuInfoModel> GetMenuList(string roleIds)
        {
            
            List<MenuInfoModel> list = new List<MenuInfoModel>();

            if (string.IsNullOrEmpty(roleIds))
            {
                //加载所有菜单列表
                list = menuDAL.GetMenuList();
            }
            else
            {
                //加载角色对应的菜单列表
            }

            return list;
        }
    }
}
