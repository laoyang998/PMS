using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace PMS.DAL
{
   public class SqlModel
    {
        public string Sql { get; set; }
        public SqlParameter[] SqlParaArray { get; set; }
    }
}
