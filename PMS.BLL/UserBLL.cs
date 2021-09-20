using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.Models.DModels;
using PMS.Models.VModels;
using PMS.DAL;

namespace PMS.BLL
{
    public class UserBLL : BaseBLL<UserInfosModel>
    {
        UserDAL userDAL = new UserDAL();
        ViewUserRoleDAL vurDAL = new ViewUserRoleDAL();

        public List<ViewUserRoleModel> Login(string uName, string uPwd)
        {
            List<ViewUserRoleModel> roleList = new List<ViewUserRoleModel>();
            int userId = userDAL.Login(uName, uPwd);
            if (userId > 0)
            {
                //获取用户角色列表
                roleList = vurDAL.GetUserRoles(userId);
            }

            return roleList;
        }
    }
}
