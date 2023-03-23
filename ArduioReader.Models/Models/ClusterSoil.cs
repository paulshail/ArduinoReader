using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoReader.Models.Models
{
    public class ClusterSoil
    {
        [Key]
        public int PK_clusterSoil_Id { get; init; }

        public string clusterSoulType { get; init; }


    }
}
