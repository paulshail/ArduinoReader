using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoReader.Sensor
{
    public class SensorDataWriter
    {

        #region vars

        private readonly string _folderLocation;

        private readonly BackgroundWorker _sensorDataWriterWorker;

        #endregion

        public SensorDataWriter(string folderLocation)
        {
            
            _folderLocation= folderLocation;

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

            Console.Write("Simulating reading...");

            Console.WriteLine("All readings done!");

            Thread.Sleep(10000);

        }


        public void StartSensorDataReader()
        {
            _sensorDataWriterWorker.RunWorkerAsync();
        }


    }

}
