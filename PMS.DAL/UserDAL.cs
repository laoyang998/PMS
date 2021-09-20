using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.Models.DModels;
using System.Data.SqlClient;

namespace PMS.DAL
{
    public class UserDAL : BaseDAL<UserInfosModel>
    {
        /// <summary>
        /// 登录，返回UserID
        /// </summary>
        /// <param name="uName"></param>
        /// <param name="uPwd"></param>
        /// <returns></returns>
        public int Login(string uName, string uPwd)
        {
            string strWhere = " UserName=@UserName and UserPwd=@UserPwd and UserState=1";
            SqlParameter[] paras =
            {
                new SqlParameter("@UserName",uName),
                new SqlParameter("@UserPwd", uPwd)
            };
            UserInfosModel user = GetModel(strWhere, "UserID", paras);
            if (user != null)
                return user.UserID;
            else
                return 0;
        }
    }
}
