using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using mqttadmin.Models;
using SQLite;


namespace mqttadmin.Database
{
    public class Database
    {
        static string DatabasePath
        {
            get
            {
                var sqliteFilename = "mqtt.db";
                var path="";
#if __IOS__
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                string libraryPath = Path.Combine(documentsPath, "..", "Library");
                path = Path.Combine(libraryPath, sqliteFilename);
#else
#if __ANDROID__
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                path = Path.Combine(documentsPath, sqliteFilename);
#endif
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
