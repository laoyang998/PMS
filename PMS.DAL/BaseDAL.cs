using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBUnity;
using PMS.Common;
using System.Data.SqlClient;

namespace PMS.DAL
{
    public class BaseDAL<T> : BQuery<T> where T : class
    {
        #region 添加
        public int Add(T t, string strCols, int isReturn)
        {
            if (t == null) return 0;

            SqlModel insert = CreateSql.GetInsertSqlAndParas<T>(t, strCols, isReturn);
            //执行SQL命令
            if (isReturn == 0)
                return SQLHelper.ExecuteNonQuery(insert.Sql, 1, insert.SqlParaArray);
            else
            {
                object oId = SQLHelper.ExecuteScalar(insert.Sql, 1, insert.SqlParaArray);
                if (oId != null && oId.ToString() != "")
                    return oId.GetInt();
                else
                    return 0;
            }
        }
        //批量插入
        public bool AddList(List<T> list, string strCols)
        {
            if (list == null || list.Count == 0)
                return false;
            List<CommandInfo> comList = new List<CommandInfo>();
            foreach (T t in list)
            {
                SqlModel insert = CreateSql.GetInsertSqlAndParas<T>(t, strCols, 0);
                CommandInfo com = new CommandInfo(insert.Sql, false, insert.SqlParaArray);
                comList.Add(com);
            }
            return SQLHelper.ExecuteTrans(comList);
        }
        #endregion

        #region 修改
        public bool Update(T t, string strCols)
        {
            if (t == null) return false;
            else
                return Update(t, strCols, "");
        }

        public bool Update(T t, string strCols, string strWhere, params SqlParameter[] paras)
        {
            if (t == null) return false;

            SqlModel update = CreateSql.GetUpdateSqlAndParas<T>(t, strCols, strWhere);
            List<SqlParameter> listParas = update.SqlParaArray.ToList();
            if (paras != null && paras.Length > 0)
            {
                listParas.AddRange(paras);
            }
            return SQLHelper.ExecuteNonQuery(update.Sql, 1, listParas.ToArray()) > 0;
        }

        public bool UpdateList(List<T> list, string strCols)
        {
            if (list == null || list.Count == 0)
                return false;
            List<CommandInfo> comList = new List<CommandInfo>();
            foreach (T t in list)
            {
                SqlModel update = CreateSql.GetUpdateSqlAndParas<T>(t, strCols, "");
                CommandInfo com = new CommandInfo(update.Sql, false, update.SqlParaArray);
                comList.Add(com);
            }
            return SQLHelper.ExecuteTrans(comList);
        }
        #endregion

        #region 删除
        public bool Delete(int id, int delType)
        {
            Type type = typeof(T);
            string strWhere = $"[{type.GetPrimary()}]=@Id";
            SqlParameter[] paras =
            {
                new SqlParameter("@Id",id)
            };
            return Delete(delType, strWhere, paras);
        }

        public bool Delete(int actType,string strWhere,SqlParameter[] paras)
        {
            Type type = typeof(T);
            string delSql = "";
            if (actType == 1)
                delSql = CreateSql.CreateDeleteSql<T>(strWhere);
            else
                delSql = $"update [{type.GetTName()}] set IsDeleted=1 where {strWhere}";
            List<CommandInfo> list = new List<CommandInfo>();
            list.Add(new CommandInfo() {
                CommandText = delSql,
                IsProc = false,
                Paras = paras
            });
            return SQLHelper.ExecuteTrans(list);
        }

        public bool DeleteList(List<int> idList,int actType)
        {
            Type type = typeof(T);
            List<CommandInfo> comList = new List<CommandInfo>();
            foreach(int id in idList)
            {
                string strWhere = $"[{type.GetPrimary()}]=@Id";
                string delSql = "";
                if (actType == 1)
                    delSql = CreateSql.CreateDeleteSql<T>(strWhere);
                else
                    delSql = $"update [{type.GetTName()}] set isDeleted=1 where {strWhere}";
                SqlParameter[] paras= {
                    new SqlParameter("@Id",id)
                };
                CommandInfo com = new CommandInfo(delSql, false, paras);
                comList.Add(com);
            }
            return SQLHelper.ExecuteTrans(comList);
        }
        #endregion
    }
}
