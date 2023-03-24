using ArduinoReader.Base.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ArduinoReader.Heartbeat
{
    public class HeartbeatWriter : IHeartbeatWriter
    {

        #region vars

        private readonly int _heartbeatInterval;
        private readonly string _fileLocation;
        private readonly string _fileName;

        private readonly BackgroundWorker _heartbeatWorker;
        
        #endregion


        public HeartbeatWriter(ReaderConfiguration readerConfig)
        {
            // Convert minutes to milliseconds
            _heartbeatInterval = readerConfig.HeartbeatInterval * 60000;
            _fileLocation = readerConfig.HeartbeatFileLocation;
            _fileName = readerConfig.HeartbeatFileName;

            _heartbeatWorker= new BackgroundWorker();
            _heartbeatWorker.DoWork += _heartbeatWorker_DoWork;
            _heartbeatWorker.WorkerSupportsCancellation= true;
            _heartbeatWorker.RunWorkerCompleted += _heartbeatWorker_RunWorkerCompleted;

        }

        private void _heartbeatWorker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            // Recall itself to begin loop again
            StartHeartbeat();
        }

        private void _heartbeatWorker_DoWork(object? sender, DoWorkEventArgs e)
        {
            Console.Write("Writing heartbeat... ");

            WriteHeartbeat();

            Console.WriteLine("Heartbeat updated");
            // Sleep for a minute
            Thread.Sleep(_heartbeatInterval);

        }



        // Can be called from console app
        public void StartHeartbeat()
        {
            _heartbeatWorker.RunWorkerAsync();
        }

        // Turn off thread
        public void StopHeartbeat()
        {
            _heartbeatWorker.CancelAsync();
        }

        public void WriteHeartbeat()
        {

            if(!File.Exists(_fileName))
            {
                  using(StreamWriter sw = File.CreateText(_fileName))
                {
                    sw.WriteLine("Arduino Reader Heartbeat: " + DateTime.Now.ToString());  
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(_fileName))
                {
                    sw.WriteLine(DateTime.Now.ToString());
                }
            }

        }

    }
}
