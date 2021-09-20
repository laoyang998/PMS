using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.Common.CustomAttributes;

namespace PMS.Models.DModels
{
    [Table("RoleInfos")]
    [PrimaryKey("RoleId")]
  public  class RoleInfoModel
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Remark { get; set; }
        public int IsDeleted { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
