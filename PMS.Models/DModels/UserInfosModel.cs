using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using PMS.Common.CustomAttributes;

namespace PMS.Models.DModels
{
    [Table("UserInfos")]
    [PrimaryKey("UserID")]
    public class UserInfosModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserPwd { get; set; }
        public int UserState { get; set; }
        public string Creator { get; set; }
        public int IsDeleted { get; set; }
    }
}
