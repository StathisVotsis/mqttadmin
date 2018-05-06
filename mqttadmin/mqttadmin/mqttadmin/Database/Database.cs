using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using mqttadmin.Models;
using SQLite;
using Windows.Storage;
using Xamarin.Forms.PlatformConfiguration;

namespace mqttadmin.Database
{
    public class Database
    {
        static string DatabasePath
        {
            get
            {
                var filename = "mqttbroker.db";
#if SILVERLIGHT
    // Windows Phone 8
    var path = filename;
#else

#if __ANDROID__
    string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); ;
#else
#if __IOS__
        // we need to put in /Library/ on iOS5.1 to meet Apple's iCloud terms
        // (they don't want non-user-generated data in Documents)
        string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
        string libraryPath = Path.Combine (documentsPath, "..", "Library");
#else
                // UWP
                string libraryPath = "";
#endif
#endif
                var path = Path.Combine(libraryPath, filename);
#endif
                return path;
            }
        }
        static object locker = new object();

        static SQLiteConnection database = new SQLiteConnection(DatabasePath);

        /// <summary>
        /// Create our Broker Item Database table.
        /// </summary>
        /// <param name="connection">Connection.</param>
        public Database()
        {
            // create the tables
            database.CreateTable<BrokerItem>();
        }

        /// <summary>
        /// Gets all of the medicine items from our database.
        /// </summary>
        /// <returns>The items.</returns>
        public IEnumerable<BrokerItem> GetItems()
        {
            // Set a mutual-exclusive lock on our database, while 
            // retrieving items.
            lock (locker)
            {
                return (from i in database.Table<BrokerItem>() select i).ToList();
            }
        }
        /// <summary>
        /// Gets a specific medicine item from the database.
        /// </summary>
        /// <returns>The item.</returns>
        /// <param name="id">Identifier.</param>
        public BrokerItem GetItem(int id)
        {
            // Set a mutual-exclusive lock on our database, while 
            // retrieving items.
            lock (locker)
            {
                return database.Table<BrokerItem>().FirstOrDefault(x => x.Id == id);
            }
        }

        /// <summary>
        /// Saves the medicine item currently being edited.
        /// </summary>
        /// <returns>The item.</returns>
        /// <param name="item">Item.</param>
        public int SaveItem(BrokerItem item)
        {
            // Set a mutual-exclusive lock on our database, while 
            // saving/updating our medicine item.
            lock (locker)
            {
                if (item.Id != 0)
                {
                    database.Update(item);
                    return item.Id;
                }
                else
                {
                    return database.Insert(item);
                }
            }
        }

        /// <summary>
        /// Deletes a specific medicine item from the database.
        /// </summary>
        /// <returns>The item.</returns>
        /// <param name="id">Identifier.</param>
        public int DeleteItem(int id)
        {
            // Set a mutual-exclusive lock on our database, while 
            // deleting our medicine item.
            lock (locker)
            {
                return database.Delete<BrokerItem>(id);
            }
        }
    }
}
