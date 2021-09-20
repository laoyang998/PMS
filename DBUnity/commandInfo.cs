using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DBUnity
{
   public class CommandInfo
    {
        public string CommandText;
        public SqlParameter[] Paras;
        public bool IsProc;

        public CommandInfo() { }

        public CommandInfo(string comText, bool IsProc)
        {
            this.CommandText = comText;
            this.IsProc = IsProc;
        }

        public CommandInfo(string comText, bool IsProc, SqlParameter[] paras)
        {
            this.CommandText = comText;
            this.IsProc = IsProc;
            this.Paras = paras;
        }
    }
}
