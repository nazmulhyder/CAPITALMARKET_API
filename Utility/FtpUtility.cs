using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class FileDefination
    {
        public string? FileName { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public decimal FileSizeInKB { get; set; }

    }
    public static class FtpUtility
    {
        public static List<FileDefination> FileList(string Url, string UserName, string Password)
        {
            try
            {
                var request = (FtpWebRequest)WebRequest.Create(Url);
                request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                request.Credentials = new NetworkCredential(UserName, Password);
                List<FileDefination> files = new List<FileDefination>();
                using (var response = (FtpWebResponse)request.GetResponse())
                {
                    using (var responseStream = response.GetResponseStream())
                    {
                        var reader = new StreamReader(responseStream);
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            if (string.IsNullOrWhiteSpace(line) == false)
                            {
                                if (!line.ToString().Contains("<DIR>"))
                                {
                                    string FullDate = line.Substring(0, 17).ToString().Remove(8, 1);
                                    var DateWithoutAMPM = DateTime.ParseExact(line.Substring(0, 15).ToString().Remove(8, 1), "MM-dd-yy hh:mm", null);

                                    if (FullDate.Substring(FullDate.Length - 2) == "PM") DateWithoutAMPM = DateWithoutAMPM.AddHours(12);

                                    files.Add(new FileDefination
                                    {
                                        FileName = line.Split(line.Substring(17, 22)).Last(),
                                        FileSizeInKB = Convert.ToInt32(line.Substring(17, 22)) / 1000,
                                        ModifiedOn = DateWithoutAMPM
                                    });
                                }

                            }
                        }

                    }
                }

                return files.OrderByDescending(f => f.ModifiedOn).ToList();
            }
            catch(Exception ex)
            {
                return new List<FileDefination>();
            }
           
        }

        public static string FileContent(string UrlPath, string UserName, string Password)
        {
            WebClient request = new WebClient();
            string url = UrlPath;
            request.Credentials = new NetworkCredential(UserName, Password);

            try
            {
                byte[] newFileData = request.DownloadData(url);
                string fileString = System.Text.Encoding.UTF8.GetString(newFileData);
                return fileString;
            }
            catch (WebException e)
            {
                return "";
            }
        }
    }
}
