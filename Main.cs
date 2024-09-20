using EveStaticDataExportConverter;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;

public class SDEConverter
{
    private string jsonSDEPath = "json_sde";
    
    //public static void main(string[] args)
    //{
    //    SDEConverter sDEConverter = new SDEConverter();
    //    sDEConverter.ConvertSDEToSQLite();
    //}

    public bool ConvertSDEToSQLite()
    {
        bool success = true;
        int maxThreads = Environment.ProcessorCount * 2;
        ThreadPool.SetMaxThreads(maxThreads, maxThreads);

        //Delete the current sqlite DB and create a new one. 
        DatabaseManager.ClearSQLiteFile();


        //Convert FSD
        Stopwatch fsdStopWatch = new Stopwatch();
        fsdStopWatch.Start();
        Console.WriteLine("Converting FSD");
        FSDConverter fSDConverter = new FSDConverter();
        success = fSDConverter.ConvertFSD();
        fsdStopWatch.Stop();

        //Convert BSD
        Stopwatch bsdStopWatch = new Stopwatch();
        bsdStopWatch.Restart();
        Console.WriteLine("");
        Console.WriteLine("Converting BSD");
        BSDConverter bSDConverter = new BSDConverter();
        success = bSDConverter.ConvertBSD();
        bsdStopWatch.Stop();

        //Convert Universe
        Stopwatch universeStopWatch = new Stopwatch();
        universeStopWatch.Restart();
        Console.WriteLine("");
        Console.WriteLine("Converting Universe");
        UniverseConverter universeConverter = new UniverseConverter();
        success = universeConverter.ConvertUniverse();
        universeStopWatch.Stop();
        Console.WriteLine();
        Console.WriteLine();

        Utility.LogElapsedTime(fsdStopWatch, "Converting FSD");
        Utility.LogElapsedTime(bsdStopWatch, "Converting BSD");
        Utility.LogElapsedTime(universeStopWatch, "Converting Universe");

        return success;
    }

    public static void Main(string[] args)
    {
        SDEConverter sDEConverter = new SDEConverter();
        sDEConverter.ConvertSDEToSQLite();
        Console.WriteLine("Done");
    }
}