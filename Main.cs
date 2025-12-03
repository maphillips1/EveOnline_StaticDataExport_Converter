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
        DatabaseManager.AddReleaseInformation();

        JSONConverter sDEConverter = new JSONConverter();
        success = sDEConverter.ConvertSDE();



        return success;
    }

    public static void Main(string[] args)
    {
        SDEConverter sDEConverter = new SDEConverter();
        sDEConverter.ConvertSDEToSQLite();
        Console.WriteLine("Done");
    }
}