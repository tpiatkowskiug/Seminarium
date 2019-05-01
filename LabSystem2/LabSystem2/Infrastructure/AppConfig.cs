using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabSystem2.Infrastructure
{
    public class AppConfig
    {
        private static string _photosFolderRelative = ConfigurationManager.AppSettings["PhotosFolder"];
        public static string PhotosFolderRelative
        {
            get
            {
                return _photosFolderRelative;
            }
        }

        private static string _genreIconsFolderRelative = ConfigurationManager.AppSettings["GenreIconsFolder"];
        public static string GenreIconsFolderRelative
        {
            get
            {
                return _genreIconsFolderRelative;
            }
        }

    }
}
