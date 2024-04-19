using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Utility
{
    public class JSONSerialize
    {
        public static string ErrorMessage = "";
        public Exception Error = null;


  
    public JObject FormCollectionToJson(IFormCollection obj)
        {
            dynamic json = new JObject();
            if (obj.Keys.Any())
            {
                foreach (string key in obj.Keys)
                {   //check if the value is an array                 
                    if (obj[key].Count > 1)
                    {
                        JArray array = new JArray();
                        for (int i = 0; i < obj[key].Count; i++)
                        {
                            array.Add(obj[key][i]);
                        }
                        json.Add(key, array);
                    }
                    else
                    {
                        var value = obj[key][0];
                        json.Add(key, value);
                    }
                }
            }
            return json;
        }



        public List<T> GetListFromJSON<T>(string JSONString)
        {
            return JsonConvert.DeserializeObject<List<T>>(JSONString);
        }

        public T GetObjecFromJSON<T>(string JSONString)
        {
            return JsonConvert.DeserializeObject<T>(JSONString);
        }
        public string getJSONSFromObject(object obj, Boolean IgnoreReferenceLoopHandling)
        {
            try
            {
                ErrorMessage = "";

                if (IgnoreReferenceLoopHandling)
                {
                    return JsonConvert.SerializeObject(obj, Formatting.None,
                            new JsonSerializerSettings()
                            {
                                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                            });
                }
                else return JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }


        public string EncodeBase64(string plainText)
        {
            ErrorMessage = "";
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public string DecodeBase64(string EncodedString)
        {
            try
            {
                ErrorMessage = "";
                byte[] data = Convert.FromBase64String(EncodedString);
                return Encoding.UTF8.GetString(data);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return null;
            }
        }

    }
}
