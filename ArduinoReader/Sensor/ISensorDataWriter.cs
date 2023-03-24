using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoReader.Sensor
{
    public interface ISensorDataWriter
    {

        public void StartSensorDataReader();

        public void StopSensorDataReader();

        public void WriteSensorDataToDatabase();

    }
}
