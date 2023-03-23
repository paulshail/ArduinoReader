using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoReader.Config
{
    public class ReaderConfig
    {

        public ReaderConfig() { }


        public ReaderConfig(string connectionString, string heartbeatFileLocation, string heartbeatFileName, double heartbeatInterval)
        {
            ConnectionString = connectionString;
            HeartbeatFileLocation = heartbeatFileLocation;
            HeartbeatFileName = heartbeatFileName;
            HeartbeatInterval = heartbeatInterval;
        }

        public string ConnectionString { get; set; }

        public string HeartbeatFileLocation { get; set; }

        public string HeartbeatFileName { get; set; }

        public double HeartbeatInterval { get; set; }

    }
}
