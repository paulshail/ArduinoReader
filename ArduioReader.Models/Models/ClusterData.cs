using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoReader.Models.Models
{
    public class ClusterData
    {

        [Key]
        public int PK_clusterData_Id { get; init; }

        public double measurementValue { get; init; }

        public DateTime dateOfMeasuerment { get; init; }

        public int FK_cluster_Id { get; init; }

        public int FK_clusterDataType { get; init; }

    }
}
