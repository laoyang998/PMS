using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.Models.DModels;

namespace PMS.DAL
{
   public class MenuDAL:BaseDAL<MenuInfoModel>
    {
        /// <summary>
        /// 加载获取所有的菜单
        /// </summary>
        /// <returns></returns>
        public List<MenuInfoModel> GetMenuList()
        {
            string strWhere = " order by MOrder";
            string cols = "Mid,MName,ParentId,MKey,MUrl,IsTop";
            return GetModelList(strWhere, cols);
        }

        public List<MenuInfoModel> GetMenuList(string roleIds,string cols)
        {
            string strWhere = $" MId in (select MId From RoleMenuInfos where RoleId in ({roleIds})) order by MOrder";
            return GetModelList(strWhere, cols);
        }
    }
}
