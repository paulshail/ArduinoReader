using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoReader.Models.DTOs
{
    public class SensorMeasurementDTO
    {

        public DateTime dateOfMeasurement { get; set; }

        public double measurementValue { get; set; }

        public int sensorId { get; set; }

        public int measurementId { get; set; }

    }
}
