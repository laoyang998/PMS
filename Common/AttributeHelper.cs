using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMS.Common.CustomAttributes;
using System.Reflection;

namespace PMS.Common
{
  public static class AttributeHelper
    {

        //映射表名
        public static string GetTName(this Type type)
        {
            string tableName = string.Empty;
            object[] attrs = type.GetCustomAttributes(false);
            foreach(var attr in attrs)
            {
                if(attr is TableAttribute)
                {
                    TableAttribute tableAttribute = attr as TableAttribute;
                    tableName = tableAttribute.Name;
                }
            }

            if (string.IsNullOrEmpty(tableName))
            {
                tableName = type.Name;
            }

            return tableName;
        }

        //映射列名
        public static string GetColName(this PropertyInfo property)
        {
            string colName = string.Empty;
            object[] attrs = property.GetCustomAttributes(false);
            foreach(var attr in attrs)
            {
                if(attr is ColumnAttribute)
                {
                    ColumnAttribute colAttribute = attr as ColumnAttribute;
                    colName = colAttribute.ColName;
                }
            }
            if (string.IsNullOrEmpty(colName))
            {
                colName = property.Name;
            }
            return colName;
        }

        //判断主键是否自增
        public static bool IsIncrement(this Type type)
        {
            object[] attrs = type.GetCustomAttributes(false);
            foreach(var attr in attrs)
            {
                if(attr is PrimaryKeyAttribute )
                {
                    PrimaryKeyAttribute primaryKey = attr as PrimaryKeyAttribute;
                    return primaryKey.autoIncrement;
                }
            }
            return false;
        }

        //获取主键名
        public static string GetPrimary(this Type type)
        {
            object[] attrs = type.GetCustomAttributes(false);
            foreach(var attr in attrs)
            {
                if(attr is PrimaryKeyAttribute)
                {
                    PrimaryKeyAttribute primaryKey = attr as PrimaryKeyAttribute;
                    return primaryKey.Name;
                }
            }
            return null;
        }

        //判断是否为主键
        public static bool IsPrimary(this Type type,PropertyInfo property)
        {
            string primaryName = type.GetPrimary();
            string colName = property.GetColName();
            return (primaryName == colName);
        }
    }
}
