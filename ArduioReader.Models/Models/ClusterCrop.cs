﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoReader.Models.Models
{
    public class ClusterCrop
    {

        [Key]
        public int PK_clusterCrop_Id { get; init; }

        public string cropName { get; init; }
    }
}
