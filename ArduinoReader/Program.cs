// See https://aka.ms/new-console-template for more information
using ArduinoReader.Config;
using ArduinoReader.Heartbeat;
using ArduinoReader.Sensor;
using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

internal class Program
{

    public readonly static string _environmentTag;

    private static IConfiguration _configuration;

    private static HeartbeatWriter _heartbeatWriter;

    public static ReaderConfig ReaderConfig { get; private set; }

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

        // Create both threads
        _heartbeatWriter = CreateHeartbeatWriter();
        _sensorDataWriter = CreateSensorDataWriter();

        // Begin both threads
        _heartbeatWriter.StartHeartbeat();
        _sensorDataWriter.StartSensorDataReader();

        Console.WriteLine("Press any key to stop process...");
        Console.ReadKey();

        _heartbeatWriter.StopHeartbeat();

    }

    private static IConfiguration SetConfiguration()
    {

        var startupArgs = Environment.GetCommandLineArgs();

        string env = null;
        string _environmentTag;

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
            Console.WriteLine("Startup arguements set");
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
        HeartbeatWriter heartbeatWriter = new HeartbeatWriter(1, "C:\\Users\\Paul\\EPOFiles\\Heartbeat", "C:\\Users\\Paul\\EPOFiles\\Heartbeat\\HeartbeatTest.txt");


        return heartbeatWriter;        
    }

    public static SensorDataWriter CreateSensorDataWriter()
    {

        SensorDataWriter sensorDataWriter = new SensorDataWriter("C:\\Users\\Paul\\EPOFiles\\SensorReadings");

        return sensorDataWriter;
    }
}

