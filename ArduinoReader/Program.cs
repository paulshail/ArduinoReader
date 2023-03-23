// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;

internal class Program
{

    private string _environmentTag;

    private IConfiguration _configuration;

    private static void Main(string[] args)
    {
        Console.WriteLine("Begining Arduino Reader");
        Console.WriteLine("------------------------");
        Console.WriteLine("Press escape to stop process");

        SetConfiguration();
       

    }

    private static void SetConfiguration()
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
            Console.WriteLine("No config file found");
        }
        else
        {
            Console.WriteLine("Config file set");
        }

        //var configBuilder = new ConfigurationBuilder()
        //    .SetBasePath(Environment.CurrentDirectory)
        //    .AddJsonFile(env, optional: false, reloadOnChange: false);

        //_configuration = configBuilder.Build();

    }

}