using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class ListtoDataTableConverter
    {
        public static DataTable ToDataTable<T>(List<T> items)
        {
            if (items == null) return new DataTable();
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public static DataTable ToDataTable<T>(T item)
        {
            DataTable dataTable = new DataTable();
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties().ToArray();

            List<PropertyInfo> orderedProperties = Props.OrderBy(o => o.Name).ToList();

            foreach (PropertyInfo prop in orderedProperties)
            // foreach (var prop in properties)
            {
                if (prop.PropertyType.Namespace.StartsWith("System") && !prop.PropertyType.Namespace.StartsWith("System.Collection"))
                {
                    if (!prop.Name.ToLower().Contains("maker") && !prop.Name.ToLower().Contains("approvalstatus") && !prop.Name.ToLower().Contains("makedate") && !prop.Name.ToLower().Contains("approvalreqsetid") && !prop.Name.ToLower().Contains("utl_"))
                    {
                        DataColumn column = new DataColumn(prop.Name);
                        column.AllowDBNull = true;
                        dataTable.Columns.Add(column);
                        //dataTable.Columns.Add(prop.Name);
                    }
                }

            }

            int NoOfColumn = dataTable.Columns.Count, ColumnCount = 0;
            object[] values = new object[(NoOfColumn)];

            // for (int i = 0; (i<= (Props.Length - 1)); i++)
            foreach (var prop in orderedProperties)
            {
                // inserting property values to datatable rows
                if (prop.PropertyType.Namespace.StartsWith("System") && !prop.PropertyType.Namespace.StartsWith("System.Collection"))
                {
                    if (!prop.Name.ToLower().Contains("maker") && !prop.Name.ToLower().Contains("approvalstatus") && !prop.Name.ToLower().Contains("makedate") && !prop.Name.ToLower().Contains("approvalreqsetid") && !prop.Name.ToLower().Contains("utl_"))
                    {
                        if (prop.GetValue(item, null) == null)
                            values[ColumnCount++] = DBNull.Value;
                        else
                            values[ColumnCount++] = prop.GetValue(item, null);

                        //values[ColumnCount++] = prop.GetValue(item, null);
                    }
                }
            }
            dataTable.Rows.Add(values);

            // put a breakpoint here and check datatable
            return dataTable;
        }
    }
}
