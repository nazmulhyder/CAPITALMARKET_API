using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class ObjectComparison
    {
        public static List<string> GetChangedProperties(Object A, Object B)
        {
            if (A.GetType() != B.GetType())
            {
                throw new System.InvalidOperationException("Objects of different Type");
            }

            List<string> changedProperties = new List<string>();
            foreach (PropertyInfo info in A.GetType().GetProperties())
            {
                var propValueA = info.GetValue(A, null) == null ? "0" : info.GetValue(A, null);
                var propValueB = info.GetValue(B, null) == null ? "0" : info.GetValue(B, null);

                if (propValueA.ToString() != propValueB.ToString())
                {
                    try
                    {
                        double VA = Convert.ToDouble(propValueA);
                        double VB = Convert.ToDouble(propValueB);
                        if (VA != VB) changedProperties.Add(info.Name);
                    }
                    catch
                    {
                        changedProperties.Add(info.Name);
                        continue;
                    }


                }
            }
            return changedProperties;

            //List<string> changedProperties = ElaborateChangedProperties(A.GetType().GetProperties(), B.GetType().GetProperties(), A, B);
            //return changedProperties;
        }

        public static List<string> GetProperties(Object A)
        {

            List<string> changedProperties = new List<string>();
            foreach (PropertyInfo info in A.GetType().GetProperties())
            {
                changedProperties.Add(info.Name);
            }
            return changedProperties;

            //List<string> changedProperties = ElaborateChangedProperties(A.GetType().GetProperties(), B.GetType().GetProperties(), A, B);
            //return changedProperties;
        }
        public static List<string> ElaborateChangedProperties(PropertyInfo[] pA, PropertyInfo[] pB, Object A, Object B)
        {
            List<string> changedProperties = new List<string>();
            foreach (PropertyInfo info in pA)
            {
                object propValueA = info.GetValue(A, null);
                object propValueB = info.GetValue(B, null);
                if (propValueA != propValueB)
                {
                    changedProperties.Add(info.Name);
                }
            }
            return changedProperties;
        }

        public static T ObjectDifference<T>(T firstObject, T secondObject, T diffObject, PropertyInfo PKID)
        {
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
            }
            return diffObject;
        }

        public static List<T> ListObjectDifference<T>(List<T> firstObject, List<T> secondObject, List<T> diffObject, PropertyInfo PKID)
        {
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
    }
}
