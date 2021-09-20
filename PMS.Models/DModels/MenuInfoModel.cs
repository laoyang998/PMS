using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.Common.CustomAttributes;

namespace PMS.Models.DModels
{
    [Table("MenuInfos")]
    [PrimaryKey("MId")]
    public  class MenuInfoModel
    {
        
        public int MId { get; set; }
        public string MName  { get; set; }
        public int ParentId { get; set; }
        public string ParentName { get; set; }
        public string Mkey { get; set; }
        public string MUrl { get; set; }
        public int IsTop { get; set; }
        public int MOrder { get; set; }
        public int IsDeleted { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }

    }
}
