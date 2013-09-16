using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;
using System.Collections;

namespace WLG.DAL
{
    /// <summary>
    /// 编写人: 罗林
    /// 功  能: DataTable 扩展功能
    /// 编写时间: 2012-12-17
    /// 
    /// 修改人：
    /// 修改原因：
    /// </summary>
    public class TypeConvert
    {
        /// <summary>
        /// DataTable 转换为List 集合
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <returns></returns>
        public static IList<T> ToList<T>(DataTable dt) where T : class,new()
        {
            List<PropertyInfo> prlist = new List<PropertyInfo>();
            Type t = typeof(T);
            //获得TResult 的Public 属性并加入到属性列表 找出TResult属性 对应的 DataTable的列名称
            Array.ForEach<PropertyInfo>(t.GetProperties(), p =>
            {
                if (dt.Columns.IndexOf(p.Name) != -1)
                    prlist.Add(p);
            });

            List<T> oblist = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T ob = Activator.CreateInstance<T>();
                //找到对应的数据 并赋值
                prlist.ForEach(p =>
                {
                    if (row[p.Name] != DBNull.Value)
                        p.SetValue(ob, row[p.Name], null);
                });
                oblist.Add(ob);
            }
            return oblist;
        }

        /// <summary>
        /// List 集合转换为DataTable
        /// </summary>
        /// <param name="value">List 集合</param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(IList<T> value) where T : class,new()
        {
            List<PropertyInfo> pList = new List<PropertyInfo>();
            Type type = typeof(T);
            DataTable dt = new DataTable();
            //把所有的public属性加入到集合并添加DataTable的列
            Array.ForEach<PropertyInfo>(type.GetProperties(), p =>
            {
                pList.Add(p);
                dt.Columns.Add(p.Name, p.PropertyType);
            });
            foreach (var item in value)
            {
                DataRow row = dt.NewRow();
                pList.ForEach(p => row[p.Name] = p.GetValue(item, null));
                dt.Rows.Add(row);
            }
            return dt;
        }

        /// <summary>
        /// DataTable转Json
        /// </summary>
        /// <param name="table">DataTable</param>
        /// <returns></returns>
        public static string DataTableToJSON(DataTable table)
        {
            StringBuilder json = new StringBuilder();
            json.Append("[");
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                json.Append("{");
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    string columnName = table.Columns[j].ColumnName;
                    string columnType = table.Columns[j].DataType.Name;

                    if (columnType == "Int32" || columnType == "Int16" || columnType == "Decimal")
                    {
                        json.AppendFormat("\"{0}\":\"{1}\"", columnName, row.IsNull(columnName) ? "" : row[columnName]);
                    }
                    else if (columnType == "Boolean")
                    {
                        json.AppendFormat("\"{0}\":{1}", columnName, row.IsNull(columnName) ? "" : row[columnName].ToString().ToLower());
                    }
                    else
                    {
                        json.AppendFormat("\"{0}\":\"{1}\"", columnName, row[columnName]);
                    }

                    if (j < table.Columns.Count - 1) json.Append(","); // add comma if not last column
                }
                json.Append("}");

                if (i < table.Rows.Count - 1) json.Append(","); // add comma if not last row
            }
            json.Append("]");
            return json.ToString();
        }

        /// <summary>
        /// DataTable转Arraylist
        /// </summary>
        /// <param name="data">DataTable</param>
        /// <returns></returns>
        public static ArrayList DataTable2ArrayList(DataTable data)
        {
            ArrayList array = new ArrayList();
            if (data != null)
            {
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    DataRow row = data.Rows[i];

                    Hashtable record = new Hashtable();
                    for (int j = 0; j < data.Columns.Count; j++)
                    {
                        object cellValue = row[j].ToString().Trim();
                        if (cellValue.GetType() == typeof(DBNull))
                        {
                            cellValue = null;
                        }
                        record[data.Columns[j].ColumnName.ToLower()] = cellValue;
                    }
                    array.Add(record);
                }
            }
            return array;
        }

    }
}
