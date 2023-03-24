using ArduinoReader.Models.DataContext;
using ArduinoReader.Models.Models;
using ArduinoReader.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoReader.Repository.Implementation
{
    public class SensorMeasurementRepository : ISensorMeasurementRepository 
    {

        private readonly PlantDataContext _plantDataContext;

        public SensorMeasurementRepository(PlantDataContext plantDatContext)
        {
            _plantDataContext = plantDatContext;
        }

        public async Task<bool> AddFileToDatabase(DateTime dateOfMeasurement, double measurementValue, int SensorId, int MeasurementId)
        {

            try
            {

                _plantDataContext.SensorMeasurement.Add(new SensorMeasurement 
                {
                    dateOfMeasurement = dateOfMeasurement,
                    FK_measurementUnit_Id = MeasurementId,
                    FK_sensor_Id= SensorId,
                    measuermentValue= measurementValue,
                });

                _plantDataContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public void Dispose()
        {

            if(_plantDataContext != null)
            {
                this._plantDataContext.Dispose();
            }

        }
    }
}
