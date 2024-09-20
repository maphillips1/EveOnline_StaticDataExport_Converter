using EveStaticDataExportConverter.Classes.FSD;
using EveStaticDataExportConverter.Classes.Database;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using EveStaticDataExportConverter.Classes.BSD;
using EveStaticDataExportConverter.Classes.FSD.Supporting_Classes;

namespace EveStaticDataExportConverter
{
    internal class BSDConverter
    {
        string bsdPath = "C:\\Users\\mrphi\\source\\repos\\EveStaticDataExportConverter\\EveOnline_StaticDataExport_Converter\\json_sde\\bsd";

        public bool ConvertBSD()
        {
            bool success = true;

            success &= ConvertInvFlags();
            success &= ConvertInvItems();
            success &= ConvertInvNames();
            success &= ConvertInvPositions();
            success &= ConvertInvUniqueNames();
            success &= ConvertStaStations();
            return success;
        }

        #region "Inv Flags"
        private bool ConvertInvFlags()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = bsdPath + "\\invFlags.json";
                string json = File.ReadAllText(path);

                InvFlag tableInfoInvFlag = new InvFlag();
                TableInfo invFlagTable = DatabaseManager.GetTableInfo<InvFlag>(tableInfoInvFlag);
                DatabaseManager.CreateTable(invFlagTable);

                Console.WriteLine("Converting Inv Flags");
                count = ConvertInvFlagsFromJSON(json, invFlagTable);

            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Inv Flags Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Inv Flags");
            return success;
        }

        private int ConvertInvFlagsFromJSON(string json, TableInfo invFlagTable)
        {
            int count = 0;
            List<InvFlag> invFlags = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InvFlag>>(json);
            if (invFlags != null)
            {
                foreach (InvFlag flag in invFlags)
                {
                    DatabaseManager.InsertRecordForType<InvFlag>(invFlagTable, flag);
                    count++;
                }
            }
            return count;
        }
        #endregion

        #region "Inv Items"
        private bool ConvertInvItems()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = bsdPath + "\\invItems.json";
                string json = File.ReadAllText(path);

                InvItems tableInfoInvItem = new InvItems();
                TableInfo invItemTable = DatabaseManager.GetTableInfo<InvItems>(tableInfoInvItem);
                DatabaseManager.CreateTable(invItemTable);

                Console.WriteLine("Converting Inv Items");
                count = ConvertInvItemsFromJSON(json, invItemTable);

            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Inv Items Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Inv Items");
            return success;
        }
        private int ConvertInvItemsFromJSON(string json,
                                            TableInfo invItemsTable)
        {
            int count = 0;
            JArray jArray = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);
            InvItems newInvItem = null;
            List<InvItems> batchInvItems = new List<InvItems>();
            try
            {
                if (jArray != null)
                {
                    List<Task> tasks = new List<Task>();

                    foreach (JObject jObject in jArray)
                    {
                        newInvItem = Newtonsoft.Json.JsonConvert.DeserializeObject<InvItems>(jObject.ToString());
                        Utility.AddRecordToBatch<InvItems>(invItemsTable, ref batchInvItems, newInvItem);
                        count++;
                    }
                    if (batchInvItems.Count > 0)
                    {
                        Utility.InsertBatchRecord<InvItems>(invItemsTable, batchInvItems);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertInvItemsFromJSON");
            }

            return count;
        }
        #endregion

        #region "Inv Names"
        private bool ConvertInvNames()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = bsdPath + "\\invNames.json";
                string json = File.ReadAllText(path);

                InvName tableInfoInvName = new InvName();
                TableInfo invNameTable = DatabaseManager.GetTableInfo<InvName>(tableInfoInvName);
                DatabaseManager.CreateTable(invNameTable);

                count = ConvertInvNamesFromJSON(json, invNameTable).Result;

            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Inv Names Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Inv Names");
            return success;
        }
        private async Task<int> ConvertInvNamesFromJSON(string json,
                                            TableInfo invNameTable)
        {
            int count = 0;
            JArray jArray = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);
            InvName newInvName = null;
            List<InvName> batchInvNames = new List<InvName>();
            try
            {
                if (jArray != null)
                {
                    List<Task> tasks = new List<Task>();

                    Console.WriteLine("Converting Inv Names");
                    foreach (JObject jObject in jArray)
                    {
                        newInvName = Newtonsoft.Json.JsonConvert.DeserializeObject<InvName>(jObject.ToString());
                        Utility.AddRecordToBatch<InvName>(invNameTable, ref batchInvNames, newInvName);
                        count++;
                    }
                    if (batchInvNames.Count > 0)
                    {
                        Utility.InsertBatchRecord<InvName>(invNameTable, batchInvNames);
                    }

                    await Task.WhenAll(tasks);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertInvNamesFromJSON");
            }
            return count;
        }
        #endregion

        #region "Inv Positions"
        private bool ConvertInvPositions()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = bsdPath + "\\invPositions.json";
                string json = File.ReadAllText(path);

                InvPosition tableInfoInvPosition = new InvPosition();
                TableInfo invPositionTable = DatabaseManager.GetTableInfo<InvPosition>(tableInfoInvPosition);
                DatabaseManager.CreateTable(invPositionTable);

                Console.WriteLine("Converting Inv Positions");
                count = ConvertInvPositionsFromJSON(json, invPositionTable);

            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Inv Positions Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Inv Positions");
            return success;
        }
        private int ConvertInvPositionsFromJSON(string json,
                                            TableInfo invPositionTable)
        {
            int count = 0;
            JArray jArray = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);
            InvPosition newInvName = null;
            List<InvPosition> batchInvPositions = new List<InvPosition>();

            try
            {
                if (jArray != null)
                {
                    foreach (JObject jObject in jArray)
                    {
                        newInvName = Newtonsoft.Json.JsonConvert.DeserializeObject<InvPosition>(jObject.ToString());
                        Utility.AddRecordToBatch<InvPosition>(invPositionTable, ref batchInvPositions, newInvName);
                        count++;
                    }
                    if (batchInvPositions.Count > 0)
                    {
                        Utility.InsertBatchRecord<InvPosition>(invPositionTable, batchInvPositions);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertInvPositionsFromJSON");
            }

            return count;
        }
        #endregion

        #region "Inv Unique Names"
        private bool ConvertInvUniqueNames()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = bsdPath + "\\invUniqueNames.json";
                string json = File.ReadAllText(path);

                InvUniqueName tableInfoInvUniqueName = new InvUniqueName();
                TableInfo invUniqueNameTable = DatabaseManager.GetTableInfo<InvUniqueName>(tableInfoInvUniqueName);
                DatabaseManager.CreateTable(invUniqueNameTable);

                Console.WriteLine("Converting Inv UniqueNames");
                count = ConvertInvUniqueNamesFromJSON(json, invUniqueNameTable);

            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Inv Unique Names Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Inv UniqueNames");
            return success;
        }
        private int ConvertInvUniqueNamesFromJSON(string json,
                                            TableInfo invUniqueNameTable)
        {
            int count = 0;
            JArray jArray = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);
            InvUniqueName ewInvUniqueName = null;
            List<InvUniqueName> batchUniqueNames = new List<InvUniqueName>();

            try
            {
                if (jArray != null)
                {
                    foreach (JObject jObject in jArray)
                    {
                        ewInvUniqueName = Newtonsoft.Json.JsonConvert.DeserializeObject<InvUniqueName>(jObject.ToString());
                        Utility.AddRecordToBatch<InvUniqueName>(invUniqueNameTable, ref batchUniqueNames, ewInvUniqueName);
                        count++;
                    }
                    if (batchUniqueNames.Count > 0)
                    {
                        Utility.InsertBatchRecord<InvUniqueName>(invUniqueNameTable, batchUniqueNames);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertInvPositionsFromJSON");
            }

            return count;
        }
        #endregion

        #region "Sta Stations"
        private bool ConvertStaStations()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = bsdPath + "\\staStations.json";
                string json = File.ReadAllText(path);

                StaStation tableInfoStaStation = new StaStation();
                TableInfo staStationTable = DatabaseManager.GetTableInfo<StaStation>(tableInfoStaStation);
                DatabaseManager.CreateTable(staStationTable);

                Console.WriteLine("Converting Sta Stations");
                count = ConvertStaStationsFromJSON(json, staStationTable);

            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Sta Stations Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Sta Stations");
            return success;
        }
        private int ConvertStaStationsFromJSON(string json,
                                            TableInfo staStationTable)
        {
            int count = 0;
            JArray jArray = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);
            StaStation newStaStation = null;
            List<StaStation> batchStations = new List<StaStation>();

            try
            {
                if (jArray != null)
                {
                    foreach (JObject jObject in jArray)
                    {
                        newStaStation = Newtonsoft.Json.JsonConvert.DeserializeObject<StaStation>(jObject.ToString());
                        Utility.AddRecordToBatch(staStationTable, ref batchStations, newStaStation);
                        count++;
                    }
                    if (batchStations.Count > 0)
                    {
                        Utility.InsertBatchRecord<StaStation>(staStationTable, batchStations);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertStaStationsFromJSON");
            }

            return count;
        }
        #endregion
    }
}
