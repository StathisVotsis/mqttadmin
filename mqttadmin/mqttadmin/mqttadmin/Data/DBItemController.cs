using mqttadmin.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace mqttadmin.Data
{
    public class DBItemController
    {
        private static object locker = new object();
        private SQLiteConnection database;

        public DBItemController()
        {
            this.database = DependencyService.Get<ISQLite>().GetConnection();
            this.database.CreateTable<DBItem>();
        }

        public DBItem GetDBItem()
        {
            lock(locker)
            {
                if(this.database.Table<DBItem>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return this.database.Table<DBItem>().First();
                }
            }
        }

        public int SaveDBItem (DBItem item)
        {
            lock (locker)
            {
                if (item.Id != 0)
                {
                    this.database.Update(item);
                    return item.Id;
                }
                else
                {
                    return this.database.Insert(item);
                }
            }
        }

        public int DeleteDBItem(int id)
        {
            lock (locker)
            {
                return this.database.Delete<DBItem>(id);
                 //database.DropTable<DBItem>();
                //return 1;
            }
        }
    }
}
