using ArduinoReader.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoReader.Repository.Interface
{
    public interface ISensorMeasurementRepository : IDisposable
    {

        public Task<bool> AddFileToDatabase(SensorMeasurementDTO sensorMeasurement);

    }
}
