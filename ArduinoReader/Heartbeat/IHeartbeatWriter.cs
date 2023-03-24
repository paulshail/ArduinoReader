using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoReader.Heartbeat
{
    public interface IHeartbeatWriter
    {

        public void StartHeartbeat();

        public void StopHeartbeat();

        public void WriteHeartbeat();

    }
}
