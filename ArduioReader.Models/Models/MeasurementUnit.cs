using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoReader.Models.Models
{
    public class MeasurementUnit
    {
        [Key]
        public int PK_measurmentUnit_Id { get; init; }

        public string measurementUnit { get; init; }

        public string measurementSymbol { get; init; }

    }
}
