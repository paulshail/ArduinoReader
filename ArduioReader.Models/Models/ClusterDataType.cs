﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoReader.Models.Models
{
    public class ClusterDataType
    {

        [Key]
        public int PK_cluserDataType_Id { get; init; }
        public string dataTypeName { get; init; }
        public string dataTypeSymbol { get; init; }

    }
}
