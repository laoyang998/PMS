using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using PMS.Common;
using System.Data.SqlClient;

namespace PMS.DAL
{
   public class CreateSql
    {
        public static SqlModel GetInsertSqlAndParas<T>(T t,string cols,int isReturn)
        {
            Type type = typeof(T);
            PropertyInfo[] properties = PropertyHelper.GetTypeProperties<T>(cols);
            string priName = type.GetPrimary();
            //生成要插入的列
            string columns = string.Join(",", properties.Where(p => p.Name != priName).Select(p => $"[{p.GetColName()}]"));
            //生成插入的参数
            string paraColumns = string.Join(",", properties.Where(p => p.Name != priName).Select(p => $"@{p.GetColName()}"));
            //参数组的生成
            SqlParameter[] arrParas = CreateParameters<T>(properties, t);
            string sql = $"INSERT INTO [{type.GetTName()}] ({columns}) VALUES({paraColumns}) ";
            if (isReturn == 1)
                sql += ";select @@identity";
            return new SqlModel() { Sql = sql, SqlParaArray = arrParas };
        }

        //生成UPDATE语句
        public static SqlModel GetUpdateSqlAndParas<T>(T t,string cols,string strWhere)
        {
            Type type = typeof(T);
            PropertyInfo[] properties = PropertyHelper.GetTypeProperties<T>(cols);
            string priName = type.GetPrimary();
            //生成更新列
            string columns = string.Join(",", properties.Where(
                p => p.Name != priName
                ).Select(p => string.Format("[{0}]=@{0}", p.GetColName())));
            //生成参数数组
            SqlParameter[] arrParas = CreateParameters<T>(properties, t);
            if (!string.IsNullOrEmpty(strWhere))
            {
                strWhere = $"{priName}=@{priName}";
            }
            //sql语句
            string sql = $"UPDATE [{type.GetTName()}] SET {columns} WHERE {strWhere}";
            return new SqlModel() { Sql = sql, SqlParaArray = arrParas };
        }

        //生成DELETE语句
        public static string CreateDeleteSql<T>(string strWhere)
        {
            Type type = typeof(T);
            string sql = $"DELETE FROM [{type.GetTName()}] WHERE 1=1";
            if (!string.IsNullOrEmpty(strWhere))
            {
                sql += strWhere;
            }
            return sql;
        }

        //生成查询语句
        public static string CreateSelectSql<T>(string strWhere,string cols)
        {
            Type type = typeof(T);
            PropertyInfo[] properties = PropertyHelper.GetTypeProperties<T>(cols);
            //string columns = string.Join(",", properties.Select(p => $"[{p.GetColName()}]"));
            if (string.IsNullOrEmpty(strWhere)) strWhere = "1=1";
            string sql = $"SELECT {cols} FROM [{type.GetTName()}] WHERE {strWhere}";
            return sql;
        }

        public static string CreateRowsSelectSql<T>(string strWhere,string cols)
        {
            Type type = typeof(T);
            PropertyInfo[] properties = PropertyHelper.GetTypeProperties<T>(cols);
            string priName = type.GetPrimary();
            if (string.IsNullOrEmpty(strWhere)) strWhere = "1=1";
            string sql=$"SELECT ROW_NUMBER() OVER ( ORDER BY {priName} ASC ) AS Id,{cols} FROM [{type.GetTName()} WHERE {strWhere}]";
            return sql;
        }

        //生成参数数组
        public static SqlParameter[] CreateParameters<T>(PropertyInfo[] properties,T t)
        {
          
            SqlParameter[] arrParas = properties.Select(
                p => new SqlParameter("@" + p.GetColName(), p.GetValue(t) ?? DBNull.Value)  //？？ 如果值为null 则取DBNull值 
                ).ToArray();
            return arrParas;
        }
    }
}
