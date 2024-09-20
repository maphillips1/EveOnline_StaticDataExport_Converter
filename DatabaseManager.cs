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
            string typeString = "";
            Classes.Attributes.SQLIgnore sqlIgnore;
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
                }
            }

            return tableInfo;
        }

        public static void CreateLanguageDescriptionTable(string tableName)
        {
            StringBuilder createCommand = new StringBuilder();
            createCommand.Append("CREATE TABLE ");
            createCommand.Append(tableName);
            createCommand.Append(" (");
            createCommand.Append("de VARCHAR(MAX), ");
            createCommand.Append("en VARCHAR(MAX), ");
            createCommand.Append("es VARCHAR(MAX), ");
            createCommand.Append("fr VARCHAR(MAX), ");
            createCommand.Append("ja VARCHAR(MAX), ");
            createCommand.Append("ko VARCHAR(MAX), ");
            createCommand.Append("ru VARCHAR(MAX), ");
            createCommand.Append("zh VARCHAR(MAX) ");
            createCommand.Append(")");

            ExecuteCommand(createCommand.ToString());
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
