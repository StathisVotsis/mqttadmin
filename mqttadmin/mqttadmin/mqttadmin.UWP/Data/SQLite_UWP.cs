using mqttadmin.Data;
using mqttadmin.UWP.Data;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(mqttadmin.UWP.Data.SQLite_UWP))]

namespace mqttadmin.UWP.Data
{
    public class SQLite_UWP : ISQLite
    {
        public SQLiteConnection GetConnection()
        {

            string documentPath = ApplicationData.Current.LocalFolder.Path;
            string path = Path.Combine(documentPath, "mqttadmin.db1");

            return new SQLiteConnection(path);
        }
    }
}
