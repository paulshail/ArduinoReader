using ArduinoReader.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
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
        public DbSet<ClusterDataType> ClusterDataType { get; init; }
        public DbSet<ClusterLocation> ClusterLocation { get; init; }
        public DbSet<ClusterSoil> ClusterSoils { get; init; }
        public DbSet<MeasurementUnit> MeasurementUnit { get; init; }
        public DbSet<Sensor> Sensor { get; init; }
        public DbSet<SensorCluster> SensorCluster { get; init; }
        public DbSet<SensorMeasurement> SensorMeasurement { get; init; }
        public DbSet<SensorType> SensorType { get; init; }

        #endregion

        #region ctors

        public PlantDataContext(DbContextOptions<PlantDataContext> options) : base(options) { }

        #endregion

        #region Config

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        #endregion
    }
}
