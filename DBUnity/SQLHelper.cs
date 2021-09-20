using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DBUnity
{
  public  class SQLHelper
    {
        private static string connStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

        public static int ExecuteNonQuery(string sql, int cmdType, params SqlParameter[] parameters)
        {
            //select @@Identity 返回上一次插入记录时自动产生的ID
            int result = 0; //返回结果
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                //执行对象的脚本CMD
                SqlCommand cmd = BuilderCommand(conn, sql, cmdType, null, parameters);
                result = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }
            return result;
        }

        public static object ExecuteScalar(string sql, int cmdType, params SqlParameter[] parameters)
        {
            object result = null;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = BuilderCommand(conn, sql, cmdType, null, parameters);
                result = cmd.ExecuteScalar();//执行T-SQL并返回第一行第一列的值 
                cmd.Parameters.Clear();

                if (result == null || result == DBNull.Value)
                {
                    return null;
                }
                else
                {
                    return result;
                }
            }
        }

        public static SqlDataReader ExecuteReader(string sql, int cmdType, params SqlParameter[] parameters)
        {
            SqlConnection conn = new SqlConnection(connStr);
            SqlCommand cmd = BuilderCommand(conn, sql, cmdType, null, parameters);
            SqlDataReader reader;
            try
            {
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return reader;
            }
            catch (Exception ex)
            {
                conn.Close();
                throw new Exception("创建Reader对象发生异常", ex);
            }
        }

        public static DataTable GetDataTable(string sql, int cmdType, params SqlParameter[] parameters)
        {
            DataTable dt = null;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = BuilderCommand(conn, sql, cmdType, null, parameters);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
            }
            return dt;
        }

        public static DataSet GetDataSet(string sql, int cmdType, params SqlParameter[] parameters)
        {
            DataSet ds = null;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = BuilderCommand(conn, sql, cmdType, null, parameters);
                //数据适配器，CONN自动打开，断开式连接
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
            }
            return ds;
        }

        //执行事务
        public static bool ExecuteTrans(List<string> listSql)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                SqlCommand cmd = BuilderCommand(conn, "", 1, trans);
                try
                {
                    int count = 0;
                    for (int i = 0; i < listSql.Count; i++)
                    {
                        if (listSql[i].Length > 0)
                        {
                            cmd.CommandText = listSql[i];
                            cmd.CommandType = CommandType.Text;
                            count += cmd.ExecuteNonQuery();
                        }
                    }
                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception("执行事务操作异常", ex);
                }
            }
        }

        public static bool ExecuteTrans(List<CommandInfo> comList)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                SqlCommand cmd = BuilderCommand(conn, "", 1, trans);
                try
                {
                    int count = 0;
                    for (int i = 0; i < comList.Count; i++)
                    {
                        cmd.CommandText = comList[i].CommandText;
                        if (comList[i].IsProc)
                            cmd.CommandType = CommandType.StoredProcedure;
                        else
                            cmd.CommandType = CommandType.Text;
                        if (comList[i].Paras != null && comList[i].Paras.Length > 0)
                        {
                            cmd.Parameters.Clear();
                            foreach (var p in comList[i].Paras)
                            {
                                cmd.Parameters.Add(p);
                            }
                        }
                        count += cmd.ExecuteNonQuery();
                    }
                    trans.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception("执行事务操作异常", ex);
                }
            }
        }

        //委托
        public static T ExecuteTrans<T>(Func<IDbCommand, T> action)
        {
            using (IDbConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                IDbTransaction trans = conn.BeginTransaction();
                IDbCommand cmd = conn.CreateCommand();
                cmd.Transaction = trans;
                return action(cmd);
            }
        }

        private static SqlCommand BuilderCommand(SqlConnection conn, string sql, int cmdType,
            SqlTransaction trans, params SqlParameter[] paras)
        {
            if (conn == null) throw new ArgumentNullException("连接对象不能为空！");
            SqlCommand command = new SqlCommand(sql, conn);
            if (cmdType == 2)
                command.CommandType = CommandType.StoredProcedure;
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            if (trans != null)
                command.Transaction = trans;
            if (paras != null && paras.Length > 0)
            {
                command.Parameters.Clear();
                command.Parameters.AddRange(paras);
            }
            return command;
        }
    }
}
