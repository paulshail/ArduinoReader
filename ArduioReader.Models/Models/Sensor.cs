﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoReader.Models.Models
{
    public class Sensor
    {

        [Key]
        public int PK_sensor_Id { get; init; }
        [ForeignKey("SensorType")]
        public int FK_sensorType_Id { get; init; }

    }
}
