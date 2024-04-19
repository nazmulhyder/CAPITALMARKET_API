using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class CustomConvert
    {
        public static List<T> DataSetToList<T>(DataTable dataset)
        {
            if(dataset.Rows.Count == 0) return new List<T>();
            string json = new JSONSerialize().getJSONSFromObject(dataset, true);
            return new JSONSerialize().GetListFromJSON<T>(json);
        }

        public static List<string> DataSetToStringList(DataTable table)
        {
            List<string> strings = new List<string>();

            var list = table.Rows.Cast<DataRow>()
               .Select(row => table.Columns.Cast<DataColumn>()
                  .Select(col => Convert.ToString(row[col]))
               .ToArray())
            .ToList();

            foreach (var item in list)
            {
                strings.Add(item.FirstOrDefault());
            }


            return strings;
        }

        public static DataTable ToDataTableUnordered<T>(T item)
        {
            DataTable dataTable = new DataTable();
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties().ToArray();

            List<PropertyInfo> orderedProperties = Props.ToList();

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

        public static T ObjectDifference<T>(T firstObject, T secondObject, T diffObject, PropertyInfo PKID)
        {
            //dynamic difference = new ExpandoObject();

            List<PropertyInfo> Props = typeof(T).GetProperties().ToList();
            foreach (PropertyInfo prop in Props)
            {
                if (prop == PKID)
                {
                    var val = prop.GetValue(firstObject, null);
                    prop.SetValue(diffObject, val, null);
                }
                else
                {
                    if (prop.PropertyType.Namespace.StartsWith("System") && !prop.PropertyType.Namespace.StartsWith("System.Collection"))
                    {
                        var firstValue = prop.GetValue(firstObject, null);
                        var secondValue = prop.GetValue(secondObject, null);
                        if (firstValue != null && secondValue != null)
                        {
                            if (!firstValue.Equals(secondValue))
                            {
                                prop.SetValue(diffObject, firstValue, null);
                                //   difference.
                            }
                            else
                            {
                                prop.SetValue(diffObject, null, null);
                            }
                        }
                        else if (firstValue != null && secondValue == null)
                        {
                            prop.SetValue(diffObject, firstValue, null);
                        }
                    }
                }
                // prop.SetValue(firstObject, difference, null);
            }
            return diffObject;
        }

        public static List<T> ListObjectDifference<T>(List<T> firstObject, List<T> secondObject, List<T> diffObject, PropertyInfo PKID)
        {
            //dynamic difference = new ExpandoObject();
            try
            {
                foreach (T obj in firstObject)
                {
                    var PKIDVal = PKID.GetValue(obj, null);
                    T secondObj = (from sObj in secondObject where PKID.GetValue(sObj, null).Equals(PKIDVal) select sObj).FirstOrDefault();
                    if (secondObj != null)
                    {
                        T diffObj = ObjectDifference<T>(obj, secondObj, secondObj, PKID);
                        diffObject.Add(diffObj);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return diffObject;
        }

        public static List<T> ListObjectRemoved<T>(List<T> existingObject, List<T> editedObject, List<T> deletedObject, PropertyInfo PKID)
        {
            //dynamic difference = new ExpandoObject();
            try
            {
                foreach (T obj in existingObject)
                {
                    bool isFound = false;
                    foreach (T obj2 in editedObject)
                    {
                        if (PKID.GetValue(obj, null).Equals(PKID.GetValue(obj2, null)))
                            isFound = true;
                    }
                    if (!isFound)
                    {
                        deletedObject.Add(obj);
                    }
                }


                List<PropertyInfo> Props = typeof(T).GetProperties().ToList();
                PropertyInfo prop = (from p in Props
                                     where p.Name.Contains("removalStatus") || p.Name.Contains("deletedStatus")
                                     select p).FirstOrDefault();
                foreach (T obj in deletedObject)
                {
                    prop.SetValue(obj, "D", null);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return deletedObject;
        }
    }
}
