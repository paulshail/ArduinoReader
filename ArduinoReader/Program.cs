// See https://aka.ms/new-console-template for more information
using ArduinoReader.Base.Configuration;
using ArduinoReader.Heartbeat;
using ArduinoReader.Models.DataContext;
using ArduinoReader.Sensor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

internal class Program
{

    public static string _environmentTag;

    private static IConfiguration _configuration;

    private static HeartbeatWriter _heartbeatWriter;

    public static ReaderConfiguration ReaderConfig { get; private set; }

    private static SensorDataWriter _sensorDataWriter;

    private static void Main(string[] args)
    {

        Console.WriteLine("Begining Arduino Reader");
        Console.WriteLine("------------------------");

        _configuration = SetConfiguration();
       
        if(_configuration == null ) 
        {
            return;
        }

       
        // DI for services, not used
        // var serviceProvider = new ServiceCollection()
        //   .AddSingleton<IHeartbeatWriter, HeartbeatWriter>()
        // .AddSingleton<ISensorDataWriter, SensorDataWriter>()
        //.BuildServiceProvider();

        ReaderConfig = CreateReaderConfig();

        if(ReaderConfig == null)
        {
            return;
        }

        // Create both threads
        _heartbeatWriter = CreateHeartbeatWriter();
        _sensorDataWriter = CreateSensorDataWriter();

        Console.WriteLine("------------------------");

        // Begin both threads
        _heartbeatWriter.StartHeartbeat();
        Thread.Sleep(500);
        _sensorDataWriter.StartSensorDataReader();


        Console.WriteLine("Press any key to stop process...");
        Console.ReadKey();

        _heartbeatWriter.StopHeartbeat();

    }

    #region methods

    private static IConfiguration SetConfiguration()
    {

        var startupArgs = Environment.GetCommandLineArgs();

        string env = null;

        for (int i = 0; i < startupArgs.Length; i++)
        {
            if (startupArgs[i].Equals("-config"))
            {
                _environmentTag = startupArgs[i + 1];
                switch (startupArgs[i + 1])
                {
                    case "Live":
                        env = "appsettings.json";
                        break;
                    case "Test":
                        env = "appsettings.Development.json";
                        break;
                    default:
                        env = null;
                        break;
                }
            }
        }

        if(env == null)
        {
            Console.WriteLine("No startup arguements found. Check for startup arguements then retart the application");
            return null;
        }
        else
        {
            Console.WriteLine("Startup arguements found");
        }

        var configBuilder = new ConfigurationBuilder()
            .SetBasePath(Environment.CurrentDirectory)
            .AddJsonFile(env, optional: false, reloadOnChange: false);


        try
        {

            _configuration = configBuilder.Build();

            Console.WriteLine("Configuration set");

            return _configuration;

        }
        catch(Exception ex)
        {
            Console.WriteLine("Unable to set configuration.  Check for configuration file then restart the application.");
            return null;
        }
    }

    public static HeartbeatWriter CreateHeartbeatWriter()
    {
        // Hardcoded to test
        HeartbeatWriter heartbeatWriter = new HeartbeatWriter(ReaderConfig);


        return heartbeatWriter;        
    }

    public static SensorDataWriter CreateSensorDataWriter()
    {

        SensorDataWriter sensorDataWriter = new SensorDataWriter(ReaderConfig);

        return sensorDataWriter;
    }

    private static ReaderConfiguration CreateReaderConfig()
    {
        try
        {

            return new ReaderConfiguration {
                ConnectionString = _configuration.GetConnectionString(_environmentTag),
                HeartbeatFileLocation = _configuration.GetSection("HeartbeatFileLocation:" + _environmentTag).Value,
                HeartbeatFileName = _configuration.GetSection("HeartbeatFileName:" + _environmentTag).Value,
                HeartbeatInterval = int.Parse(_configuration.GetSection("HeartbeatInterval").Value),
                SensorReadingsFileLocation = _configuration.GetSection("SensorReadingsFileLocation:" + _environmentTag).Value,
                ErrorSensorReadingsFileLocation = _configuration.GetSection("ErrorSensorReadingFileLocation:" + _environmentTag).Value
            };

        }
        catch 
        {

            Console.WriteLine("The configuration file contained an error.  Check the file then restart the application");
            
            return null;
        } 
    }

    static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
    {
        Console.WriteLine(e.ExceptionObject.ToString());
        Console.WriteLine("Press Enter to continue");
        Console.ReadLine();
        Environment.Exit(1);
    }

    #endregion

}

