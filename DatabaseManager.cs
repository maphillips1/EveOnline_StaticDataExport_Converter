using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using EveStaticDataExportConverter.Classes.Database;
using System.Data.Entity.Core.Metadata.Edm;
using System.Reflection;
using EveStaticDataExportConverter.Classes.FSD.Supporting_Classes;
using EveStaticDataExportConverter.Classes.Attributes;

namespace EveStaticDataExportConverter
{
    static internal class DatabaseManager
    {
        static string outputFilePath = "SQLiteOutput/sqlite-latest.sqlite";

        public static void ClearSQLiteFile()
        {
            if (File.Exists(outputFilePath))
            {
                File.Delete(outputFilePath);
            }
            System.IO.FileInfo fileInfo = new FileInfo(outputFilePath);
            fileInfo.Directory.Create();
            SQLiteConnection.CreateFile(outputFilePath);
        }

        public static TableInfo GetTableInfo<T>(T objectForTable)
        {
            TableInfo tableInfo = new TableInfo();
            tableInfo.Name = typeof(T).Name;
            tableInfo.columns = new List<string>();
            tableInfo.indexColumns = new List<string>();
            string typeString = "";
            Classes.Attributes.SQLIgnore sqlIgnore;
            Classes.Attributes.SQLiteIndex sQLiteIndex;
            foreach (System.Reflection.PropertyInfo property in typeof(T).GetProperties())
            {
                sqlIgnore = property.GetCustomAttribute<Classes.Attributes.SQLIgnore>();
                if (sqlIgnore != null) { continue; }

                if (property.PropertyType == typeof(List<LanguageDescription>))
                {
                    continue;
                }
                else
                {
                    typeString = property.GetCustomAttribute<Classes.Attributes.SQLiteType>().name;

                    if (!string.IsNullOrEmpty(typeString))
                    {
                        tableInfo.columns.Add(property.Name + " " + typeString);
                    }

                    sQLiteIndex = property.GetCustomAttribute<Classes.Attributes.SQLiteIndex>();
                    if (sQLiteIndex != null)
                    {
                        tableInfo.indexColumns.Add(property.Name);
                    }
                }
            }

            return tableInfo;
        }

        public static void CreateTable(TableInfo tableInfo)
        {
            StringBuilder createCommand = new StringBuilder();
            createCommand.Append("CREATE TABLE ");
            createCommand.Append(tableInfo.Name);
            createCommand.Append(" (");
            for (int i = 0; i < tableInfo.columns.Count; i++)
            {
                createCommand.Append(tableInfo.columns[i]);
                if (i != tableInfo.columns.Count - 1)
                {
                    createCommand.Append(", ");
                }
            }
            createCommand.Append(")");

            ExecuteCommand(createCommand.ToString());

            if (tableInfo.indexColumns.Count > 0)
            {
                CreateIndexForTable(tableInfo);
            }
        }

        public static void CreateIndexForTable(TableInfo tableInfo)
        {
            StringBuilder indexCommand = new StringBuilder();
            indexCommand.Append("Create INDEX ");
            indexCommand.Append(tableInfo.Name + "_IX on " + tableInfo.Name);
            indexCommand.Append(" (");

            int columnCount = 0;
            while (columnCount < tableInfo.indexColumns.Count)
            {
                indexCommand.Append(tableInfo.indexColumns[columnCount]);
                if (columnCount < tableInfo.indexColumns.Count - 1)
                {
                    indexCommand.Append(", ");
                }
                columnCount++;
            }

            indexCommand.Append(")");
            ExecuteCommand(indexCommand.ToString());
        }

        public static void InsertRecordForType<T>(TableInfo tableInfo, T record)
        {
            List<string> values = null;
            System.TypeCode typeCode;
            values = new List<string>();
            Classes.Attributes.SQLIgnore sqlIgnore;
            foreach (System.Reflection.PropertyInfo property in typeof(T).GetProperties())
            {
                sqlIgnore = property.GetCustomAttribute<Classes.Attributes.SQLIgnore>();
                if (sqlIgnore != null) { continue; }

                typeCode = Type.GetTypeCode(property.PropertyType);
                if (typeCode == TypeCode.Boolean)
                {
                    values.Add((Convert.ToBoolean(property.GetValue(record)) ? 1 : 0).ToString());
                }
                else
                {
                    values.Add(property.GetValue(record).ToString());
                }
            }
            if (values.Count > 0 && values.Count == tableInfo.columns.Count)
            {
                InsertRecord(tableInfo, values);
            }
        }

        public static void InsertRecord(TableInfo tableInfo, List<string> values)
        {
            StringBuilder createCommand = new StringBuilder();
            createCommand.Append("INSERT INTO ");
            createCommand.Append(tableInfo.Name);
            createCommand.Append(" (");
            for (int i = 0; i < tableInfo.columns.Count; i++)
            {
                createCommand.Append(tableInfo.columns[i].Split(" ")[0]);
                if (i != tableInfo.columns.Count - 1)
                {
                    createCommand.Append(", ");
                }
            }
            createCommand.Append(")");
            createCommand.AppendLine();

            createCommand.Append("VALUES (");
            for (int i = 0; i < values.Count; i++)
            {
                createCommand.Append("'");
                createCommand.Append(values[i].Replace("'", "''").Replace(@"\", @"\\"));
                createCommand.Append("'");
                if (i != values.Count - 1)
                {
                    createCommand.Append(", ");
                }
            }
            createCommand.Append(")");


            ExecuteCommand(createCommand.ToString());
        }

        public static void InsertRecordsForListofTypes<T>(TableInfo tableInfo, List<T> records)
        {

            StringBuilder createCommand = new StringBuilder();
            createCommand.Append(GetInsertLine(tableInfo));
            createCommand.AppendLine(GetValuesLines<T>(tableInfo, records));
            ExecuteCommand(createCommand.ToString());
        }

        private static string GetInsertLine(TableInfo tableInfo)
        {
            StringBuilder createCommand = new StringBuilder();
            createCommand.Append("INSERT INTO ");
            createCommand.Append(tableInfo.Name);
            createCommand.Append(" (");
            for (int i = 0; i < tableInfo.columns.Count; i++)
            {
                createCommand.Append(tableInfo.columns[i].Split(" ")[0]);
                if (i != tableInfo.columns.Count - 1)
                {
                    createCommand.Append(", ");
                }
            }
            createCommand.Append(")");
            createCommand.AppendLine();
            return createCommand.ToString();
        }

        private static string GetValuesLines<T>(TableInfo tableInfo, List<T> records)
        {
            StringBuilder createCommand = new StringBuilder();
            List<string> values = null;
            System.TypeCode typeCode;
            values = new List<string>();
            Classes.Attributes.SQLIgnore sqlIgnore;
            createCommand.Append("VALUES ");
            int recordCount = 0;

            foreach (T record in records)
            {
                values = new List<string>();
                foreach (System.Reflection.PropertyInfo property in typeof(T).GetProperties())
                {
                    sqlIgnore = property.GetCustomAttribute<Classes.Attributes.SQLIgnore>();
                    if (sqlIgnore != null) { continue; }

                    typeCode = Type.GetTypeCode(property.PropertyType);
                    if (typeCode == TypeCode.Boolean)
                    {
                        values.Add((Convert.ToBoolean(property.GetValue(record)) ? 1 : 0).ToString());
                    }
                    else
                    {
                        values.Add(property.GetValue(record).ToString());
                    }
                }
                createCommand.Append("(");
                for (int i = 0; i < values.Count; i++)
                {
                    createCommand.Append("'");
                    createCommand.Append(values[i].Replace("'", "''").Replace(@"\", @"\\"));
                    createCommand.Append("'");
                    if (i != values.Count - 1)
                    {
                        createCommand.Append(", ");
                    }
                }
                createCommand.Append(")");
                if (recordCount != records.Count - 1)
                {
                    createCommand.Append(",");
                    createCommand.AppendLine("");
                }
                recordCount++;
            }
            return createCommand.ToString();
        }

        private static void ExecuteCommand(string commandString)
        {
            string connectionString = "Data Source=" + outputFilePath + ";Version=3;";
            SQLiteConnection m_dbConnection = new SQLiteConnection(connectionString);
            m_dbConnection.Open();

            SQLiteCommand command = new SQLiteCommand(commandString, m_dbConnection);

            command.ExecuteNonQuery();

            m_dbConnection.Close();
        }
    }
}
