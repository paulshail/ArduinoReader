﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoReader.Models.Models
{
    public class SensorType
    {
        [Key]
        public int PK_sensorType_Id { get; init; }

        public string sensorTypeName { get; init; }
    }
}
