using EveStaticDataExportConverter;
using System.Data.SQLite;
using System.Diagnostics;

public class SDEConverter
{
    private string jsonSDEPath = "json_sde";
    
    public static void main(string[] args)
    {
        SDEConverter sDEConverter = new SDEConverter();
        sDEConverter.ConvertSDEToSQLite();
    }

    public bool ConvertSDEToSQLite()
    {
        bool success = true;

        //Delete the current sqlite DB and create a new one. 
        DatabaseManager.ClearSQLiteFile();

        Stopwatch sw = new Stopwatch();

        //Convert FSD
        sw.Start();
        Console.WriteLine("Converting FSD");
        FSDConverter fSDConverter = new FSDConverter();
        success = fSDConverter.ConvertFSD();
        sw.Stop();
        Utility.LogElapsedTime(sw, "Converting FSD");

        //Convert BSD
        sw.Restart();
        Console.WriteLine("");
        Console.WriteLine("Converting BSD");
        BSDConverter bSDConverter = new BSDConverter();
        success = bSDConverter.ConvertBSD();
        sw.Stop();
        Utility.LogElapsedTime(sw, "Converting BSD");

        //Convert Universe
        sw.Restart();
        Console.WriteLine("");
        Console.WriteLine("Converting Universe");
        UniverseConverter universeConverter = new UniverseConverter();
        success = universeConverter.ConvertUniverse();
        sw.Stop();
        Utility.LogElapsedTime(sw, "Converting Universe");

        return success;
    }

    

    public static void Main(string[] args)
    {
        SDEConverter sDEConverter = new SDEConverter();
        sDEConverter.ConvertSDEToSQLite();
        Console.WriteLine("Done");
    }
}