﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoReader.Models.Models
{
    public class ClusterLocation
    {
        [Key]
        public int PK_clusterLocation_Id { get; init; }

        public string clusterLocationName { get; init; }


    }
}
