using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Utility
{
    public static class SD
    {
        public static string FolderPath { get; set; } = @"C:\\UpdatedAssignmentData";
        public static string UrllWithFacebookCode { get; set; }
        public static string FacebookRedirectPage { get; set; } = "https%3A%2F%2Flocalhost%3A7061%2FHome%2F";
        public static string FacebookClientId { get; set; } = "978379916908004";
        public static string FacebookClientSecret { get; set; } = "6b0f9a4e82f8785a97583ea46eb45d7e&code";
    }
}
