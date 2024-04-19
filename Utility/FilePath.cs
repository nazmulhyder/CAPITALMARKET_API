using AutoMapper.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class FilePath
    {
        //public static IConfiguration Configuration { get; set; }
        public static string GetFileUploadURL()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
            var config = builder.Build();
            string documentPath = config["DocumentFilePath:FileURL"];
            return documentPath;
        }
		public static string GetImportedFilesPath()
		{
			var builder = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json");
			var config = builder.Build();
			string documentPath = config["DocumentFilePath:ImportedFiles"];
			return documentPath;
		}
    public static string GetExportedFilesPath()
		{
			var builder = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json");
			var config = builder.Build();
			string documentPath = config["DocumentFilePath:ExportedFiles"];
			return documentPath;
		}
		public static string GetAuditInspectionFileUploadURL()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
            var config = builder.Build();
            string documentPath = config["DocumentFilePath:AuditInspectionURL"];
            return documentPath;
        }

        public static string GetMarginRequestFileUploadURL()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
            var config = builder.Build();
            string documentPath = config["DocumentFilePath:MarginRequestFileURL"];
            return documentPath;
        }
    }
}
