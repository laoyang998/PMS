using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using PMS.Common.CustomAttributes;

namespace PMS.Models.DModels
{
    [Table("GoodsInfos")]
    [PrimaryKey("GoodsId")]
    public class GoodsInfoModel
    {
        public int GoodsId { get; set; }
        public string GoodsNo { get; set; }
        public string GoodsName { get; set; }
        public string GUnit { get; set; }
        public string GoodsBar { get; set; }
        public int IsStoped { get; set; }
        public Single BidPrice { get; set; }
        public Single Price { set; get; }
        public string Remark { get; set; }
        public int IsDelete { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
