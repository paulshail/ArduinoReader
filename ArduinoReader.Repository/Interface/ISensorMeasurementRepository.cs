using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoReader.Repository.Interface
{
    public interface ISensorMeasurementRepository
    {

        public Task<bool> AddFileToDatabase(DateTime dateOfMeasurement, double measurementValue, int SensorId, int MeasurementId);

    }
}
