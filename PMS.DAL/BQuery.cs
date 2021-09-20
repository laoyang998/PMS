using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DBUnity;
using PMS.Common;

namespace PMS.DAL
{
    public class BQuery<T>
    {
        public T GetModel(string strWhere, string strCols, params SqlParameter[] paras)
        {
            //生成查询语句
            string selSql = CreateSql.CreateSelectSql<T>(strWhere, strCols);
            //生成reader对象
            SqlDataReader reader = SQLHelper.ExecuteReader(selSql, 1, paras);
            //转为实体对象
            T model = DbConvert.SqlDataReaderToModel<T>(reader, strCols);
            reader.Close();
            return model;
        }

        //根据ID获取实体信息
        public T GetById(int id, string strCols)
        {
            Type type = typeof(T);
            string strWhere = $"[{type.GetPrimary()}]=@Id";
            SqlParameter[] paras = { new SqlParameter("@Id", id) };
            return GetModel(strWhere, strCols, paras);
        }

        //根据条件判断存在
        public bool Exists(string strWhere, params SqlParameter[] paras)
        {
            Type type = typeof(T);
            string sql = $"SELECT COUNT(1) FROM {type.GetTName()} WHERE {strWhere}";
            object val = SQLHelper.ExecuteScalar(sql, 1, paras);
            if (val.GetInt() > 0)
                return true;
            else
                return false;
        }
        public bool ExistsByName(string sName, string vName)
        {
            string strWhere = $"{sName}=@{sName}";
            SqlParameter[] paras =
            {
                new SqlParameter($"@{sName}",vName)
            };

            return Exists(strWhere, paras);
        }
       
        //在同一级别下是否重名
        public bool ExistsByName(string sName,string vName,string sParent,int parId)
        {
            string strWhere = $"{sName}=@{sName}";
            if (parId > 0)
                strWhere += $" and {sParent}=@{sParent}";
            strWhere += " and isDelete = 0";
            SqlParameter[] paras =
            {
                new SqlParameter($"{sName}",vName),
                new SqlParameter($"{sParent}",parId)
            };
            return Exists(strWhere, paras);
        }

        public List<T> GetModelList(string strWhere,string strCols,params SqlParameter[] paras)
        {
            if (string.IsNullOrEmpty(strWhere))
            {
                strWhere = "1=1";
            }
            string selSql = CreateSql.CreateSelectSql<T>(strWhere, strCols);
            SqlDataReader reader = SQLHelper.ExecuteReader(selSql, 1, paras);
            List<T> list = DbConvert.SqlDataReaderToList<T>(reader, strCols);
            reader.Close();
            return list;
        }

        public List<T> GetModelList(string cols)
        {
            return GetModelList("IsDeleted=0", cols);
        }
        //返回带行号的列表
        public List<T> getRowModelList(string strWhere,string strCols,params SqlParameter[] paras)
        {
            if (string.IsNullOrEmpty(strWhere))
                strWhere = "1=1";
            //生成SQL语句
            string selSql = CreateSql.CreateSelectSql<T>(strWhere, strCols);
            SqlDataReader reader = SQLHelper.ExecuteReader(selSql, 1, paras);
            List<T> list = DbConvert.SqlDataReaderToList<T>(reader, strCols + ",Id");
            reader.Close();
            return list;
        }

        public DataTable GetList(string sql,int isProc,string strCols,params SqlParameter[] paras)
        {
            List<string> listCols = strCols.GetStrList(',', true);
            DataTable dt = SQLHelper.GetDataTable(sql, isProc, paras);
            return dt;
        }  
        
        public DataSet GetDs(string sql,int isProc,params SqlParameter[] paras)
        {
            DataSet ds = SQLHelper.GetDataSet(sql, isProc, paras);
            return ds;
        } 
        
        //分页查询
          public DataSet GetPageDs<S>(string strWhere,int typeId,string keywords,string strCols,string proName,int startIndex,int pageSize)
        {
            List<SqlParameter> listParas = new List<SqlParameter>();
            listParas.Add(new SqlParameter("@typeId", typeId));
            listParas.Add(new SqlParameter("@keywords", keywords));
            string sql = CreateSql.CreateRowsSelectSql<S>(strWhere, strCols);
            listParas.Add(new SqlParameter("@sql", sql));
            listParas.Add(new SqlParameter("@startIndex", startIndex));
            listParas.Add(new SqlParameter("@endIndex", startIndex + pageSize-1));
            DataSet ds = GetDs(proName, 2, listParas.ToArray());
            return ds;
        }
    }
}
