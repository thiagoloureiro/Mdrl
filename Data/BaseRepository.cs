using Microsoft.Extensions.Configuration;
using Util;

namespace Data
{
    public class BaseRepository
    {
        public static IConfigurationRoot Configuration { get; set; }

        public string Connstring = UtilsClass.GetConnectionString();
    }
}