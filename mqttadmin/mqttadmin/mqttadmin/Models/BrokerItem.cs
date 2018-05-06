using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace mqttadmin.Models
{
    public class BrokerItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
