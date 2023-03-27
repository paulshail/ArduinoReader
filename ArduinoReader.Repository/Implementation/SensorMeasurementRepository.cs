using ArduinoReader.Base.Configuration;
using ArduinoReader.Models.DataContext;
using ArduinoReader.Models.DTOs;
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


        public SensorMeasurementRepository(PlantDataContext plantDataContext)
        {
            _plantDataContext = plantDataContext;

        }


        public bool AddFileToDatabase(SensorMeasurementDTO measurement)
        {

            try
            {

                _plantDataContext.SensorMeasurement.Add(new SensorMeasurement 
                {
                    dateOfMeasurement = measurement.dateOfMeasurement,
                    FK_measurementUnit_Id = measurement.measurementId,
                    FK_sensor_Id= measurement.sensorId,
                    measurementValue= measurement.measurementValue
                });

                _plantDataContext.SaveChanges();

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
