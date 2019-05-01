using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LabSystem2.Infrastructure
{
    public static class UrlHelpers
    {
        public static string GenreIconPath(this UrlHelper helper, string genreIconFilename)
        {
            var genreIconFolder = AppConfig.GenreIconsFolderRelative;
            var path = Path.Combine(genreIconFolder, genreIconFilename);
            var absolutePath = helper.Content(path);
            return absolutePath;
        }

        public static string AlbumCoverPath(this UrlHelper helper, string albumFilename)
        {
            var albumCoverFolder = AppConfig.PhotosFolderRelative;
            var path = Path.Combine(albumCoverFolder, albumFilename);
            var absolutePath = helper.Content(path);
            return absolutePath;
        }
    }
}
