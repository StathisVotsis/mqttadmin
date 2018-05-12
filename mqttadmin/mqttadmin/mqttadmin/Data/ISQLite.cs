using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace mqttadmin.Data
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
