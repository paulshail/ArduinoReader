using ArduinoReader.Base.Configuration;
using ArduinoReader.Models.DataContext;
using ArduinoReader.Models.DTOs;
using ArduinoReader.Repository.Implementation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoReader.Sensor
{
    public class SensorDataWriter : ISensorDataWriter
    {

        #region vars

        private readonly ReaderConfiguration _readerConfiguration;

        private readonly BackgroundWorker _sensorDataWriterWorker;

        #endregion

        public SensorDataWriter(ReaderConfiguration readerConfig)
        {

            _readerConfiguration = readerConfig;

            _sensorDataWriterWorker= new BackgroundWorker();
            _sensorDataWriterWorker.DoWork += _sensorDataWriterWorker_DoWork;
            _sensorDataWriterWorker.WorkerSupportsCancellation = false;
            _sensorDataWriterWorker.RunWorkerCompleted += _sensorDataWriterWorker_RunWorkerCompleted;

        }

        private void _sensorDataWriterWorker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {

            StartSensorDataReader();

        }

        private void _sensorDataWriterWorker_DoWork(object? sender, DoWorkEventArgs e)
        {
            Console.WriteLine("Checking for sensor readings...");

            try
            {

                WriteSensorDataToDatabase();

            }
            catch
            {
                Console.WriteLine("Unexpected error while checking for sensor readings");
            }

            Console.WriteLine("Check complete");

            Thread.Sleep(60000);

        }


        public void StartSensorDataReader()
        {
            _sensorDataWriterWorker.RunWorkerAsync();
        }

        public void StopSensorDataReader()
        {
            throw new NotImplementedException();
        }

        public void WriteSensorDataToDatabase()
        {
            IEnumerable<string> sensorReadings = new ObservableCollection<string>();

            try
            {
                sensorReadings = Directory.GetFiles(_readerConfiguration.SensorReadingsFileLocation);
            }
            catch
            {
                Console.WriteLine("There was an error opening the file");
            }

            // Check if there is any files
            if (sensorReadings.Count() > 0)
            {



                foreach (string sensorReading in sensorReadings)
                {

                    bool errorInFile = false;

                    try
                    {
                        using (StreamReader sr = new StreamReader(sensorReading))
                        {



                            while (!sr.EndOfStream)
                            {
                                string toAdd = sr.ReadLine();
                                int lineCounter = 0;

                                if (toAdd != null)
                                {

                                    string[] measurementReading = toAdd.Split(',');

                                    SensorMeasurementDTO? measurementReadingDTO = ConvertToSensorMeasurementDTO(measurementReading);

                                    if (measurementReadingDTO != null)
                                    {
                                        using (SensorMeasurementRepository sensorRepo = new SensorMeasurementRepository(new PlantDataContext(_readerConfiguration.ConnectionString)))
                                        {
                                            bool addedToDatabase = sensorRepo.AddFileToDatabase(measurementReadingDTO);

                                            if (!addedToDatabase)
                                            {
                                                errorInFile = true;
                                            }
                                            else
                                            {
                                                Console.WriteLine("File: " + sensorReading + " added to database");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Unable to save reading in line: " + lineCounter + " to database of file: " + sensorReading);
                                        errorInFile = true;
                                    }
                                }

                                lineCounter++;



                            }
                        }


                    }

                    catch
                    {
                        Console.WriteLine("Error while reading file: " + sensorReading);
                    }

                    if (errorInFile)
                    {
                        try
                        {

                            string[] fileName = sensorReading.Split("\\");

                            string sensorDirectory = sensorReading;

                            // Get just the file name to set the directory to move the file
                            string errorDirectory = _readerConfiguration.ErrorSensorReadingsFileLocation + fileName[fileName.Length - 1];

                            using (FileStream fs = File.Create(sensorDirectory))
                            {
                                if (!File.Exists(errorDirectory))
                                {
                                    File.Move(sensorDirectory, errorDirectory);
                                }
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Error moving file: " + sensorReading + " Deleting the file");
                            File.Delete(sensorReading);
                        }
                    }
                    else
                    {
                        // No errors in file, can be deleted
                        Console.WriteLine("Deleting successfully read files");

                        // Don't delete for now
                        File.Delete(sensorReading);
                    }
                }
            }
            else
            {
                Console.WriteLine("No files to add to database");
            }

        }

        public SensorMeasurementDTO? ConvertToSensorMeasurementDTO(string[] readingToConvert)
        {

            if (readingToConvert.Length == 4) {

                try
                {
                    SensorMeasurementDTO toReturn = new SensorMeasurementDTO
                    {
                        dateOfMeasurement = DateTime.Parse(readingToConvert[0].Trim()),
                        measurementValue = Double.Parse(readingToConvert[1].Trim()),
                        sensorId = int.Parse(readingToConvert[2].Trim()),
                        measurementId = int.Parse((readingToConvert[3].Trim()))
                    };

                    return toReturn;
                }
                catch
                {
                    return null;
                }
            }

            return null;

        }

    }

}
