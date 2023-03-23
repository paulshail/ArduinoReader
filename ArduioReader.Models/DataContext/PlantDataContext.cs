using ArduinoReader.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoReader.Models.DataContext
{
    public class PlantDataContext : DbContext
    {


        #region Tables

        public DbSet<Cluster> Cluster { get; init; }
        public DbSet<ClusterCrop> ClusterCrops { get; init; }
        public DbSet<ClusterData> ClusterData { get; init; }

        #endregion
    }
}
