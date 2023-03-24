using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoReader.Base.Configuration
{
    public class ReaderConfiguration
    {

        public ReaderConfiguration() { }


        public ReaderConfiguration(string connectionString, string heartbeatFileLocation, string heartbeatFileName, int heartbeatInterval, string sensorReadingsFileLocation)
        {
            ConnectionString = connectionString;
            HeartbeatFileLocation = heartbeatFileLocation;
            HeartbeatFileName = heartbeatFileName;
            HeartbeatInterval = heartbeatInterval;
            SensorReadingsFileLocation = sensorReadingsFileLocation;
        }

        public string ConnectionString { get; set; }

        public string HeartbeatFileLocation { get; set; }

        public string HeartbeatFileName { get; set; }

        public int HeartbeatInterval { get; set; }

        public string SensorReadingsFileLocation { get; set; }

    }
}
