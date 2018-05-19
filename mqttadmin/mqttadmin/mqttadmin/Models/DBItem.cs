using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace mqttadmin.Models
{
    public class DBItem
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string Host { get; set; }
        public string Port { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
    }
}
