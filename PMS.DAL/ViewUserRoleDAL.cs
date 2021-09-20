using PMS.Models.VModels;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.DAL
{
   public class ViewUserRoleDAL:BQuery<ViewUserRoleModel>
    {
        public List<ViewUserRoleModel> GetUserRoles(int userId)
        {
            string strWhere = " UserId =@UserId";
            SqlParameter paraId = new SqlParameter("@UserId", userId);
            return GetModelList(strWhere, "UserId,RoleId,RoleName",paraId);
        }
    }
}
