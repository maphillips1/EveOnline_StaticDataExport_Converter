using EveStaticDataExportConverter.Classes.FSD;
using EveStaticDataExportConverter.Classes.Database;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EveStaticDataExportConverter.Classes.BSD;
using EveStaticDataExportConverter.Classes.FSD.Supporting_Classes;

namespace EveStaticDataExportConverter
{
    internal class BSDConverter
    {
        string bsdPath = "C:\\Users\\mrphi\\source\\repos\\EveStaticDataExportConverter\\json_sde\\bsd";

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
                count = ConvertInvItemsFromJSON(json, invItemTable).Result;

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
        private async Task<int> ConvertInvItemsFromJSON(string json,
                                            TableInfo invItemsTable)
        {
            int count = 0;
            JArray jArray = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);

            try
            {
                if (jArray != null)
                {
                    List<Task> tasks = new List<Task>();

                    foreach (JObject jObject in jArray)
                    {
                        tasks.Add(ThreadedConvertInvItemsFromJSON(jObject,
                                                                invItemsTable));
                        count++;
                    }

                    await Task.WhenAll(tasks);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertInvItemsFromJSON");
            }

            return count;
        }

        private Task ThreadedConvertInvItemsFromJSON(JObject jObject,
                                            TableInfo eveTypeTable)
        {
            InvItems newInvItem = null;
            newInvItem = Newtonsoft.Json.JsonConvert.DeserializeObject<InvItems>(jObject.ToString());
            DatabaseManager.InsertRecordForType<InvItems>(eveTypeTable, newInvItem);

            return Task.CompletedTask;
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
            try
            {
                if (jArray != null)
                {
                    List<Task> tasks = new List<Task>();

                    Console.WriteLine("Converting Inv Names");
                    foreach (JObject jObject in jArray)
                    {
                        tasks.Add(ThreadedConvertInvNamesFromJSON(jObject,
                                                                invNameTable));
                        count++;
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

        private Task ThreadedConvertInvNamesFromJSON(JObject jObject,
                                            TableInfo invNameTable)
        {
            InvName newInvName = null;
            newInvName = Newtonsoft.Json.JsonConvert.DeserializeObject<InvName>(jObject.ToString());
            DatabaseManager.InsertRecordForType<InvName>(invNameTable, newInvName);

            return Task.CompletedTask;
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
                count = ConvertInvPositionsFromJSON(json, invPositionTable).Result;

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
        private async Task<int> ConvertInvPositionsFromJSON(string json,
                                            TableInfo invPositionTable)
        {
            int count = 0;
            JArray jArray = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);

            try
            {
                if (jArray != null)
                {
                    List<Task> tasks = new List<Task>();

                    foreach (JObject jObject in jArray)
                    {
                        tasks.Add(ThreadedConvertInvPositionsFromJSON(jObject,
                                                                invPositionTable));
                        count++;
                    }

                    await Task.WhenAll(tasks);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertInvPositionsFromJSON");
            }

            return count;
        }

        private Task ThreadedConvertInvPositionsFromJSON(JObject jObject,
                                            TableInfo invPositionTable)
        {
            InvPosition newInvName = null;
            newInvName = Newtonsoft.Json.JsonConvert.DeserializeObject<InvPosition>(jObject.ToString());
            DatabaseManager.InsertRecordForType<InvPosition>(invPositionTable, newInvName);

            return Task.CompletedTask;
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
                count = ConvertInvUniqueNamesFromJSON(json, invUniqueNameTable).Result;

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
        private async Task<int> ConvertInvUniqueNamesFromJSON(string json,
                                            TableInfo invUniqueNameTable)
        {
            int count = 0;
            JArray jArray = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);

            try
            {
                if (jArray != null)
                {
                    List<Task> tasks = new List<Task>();

                    foreach (JObject jObject in jArray)
                    {
                        tasks.Add(ThreadedConvertInvUniqueNamesFromJSON(jObject,
                                                                invUniqueNameTable));
                        count++;
                    }

                    await Task.WhenAll(tasks);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertInvPositionsFromJSON");
            }

            return count;
        }

        private Task ThreadedConvertInvUniqueNamesFromJSON(JObject jObject,
                                            TableInfo invPositionTable)
        {
            InvUniqueName ewInvUniqueName = null;
            ewInvUniqueName = Newtonsoft.Json.JsonConvert.DeserializeObject<InvUniqueName>(jObject.ToString());
            DatabaseManager.InsertRecordForType<InvUniqueName>(invPositionTable, ewInvUniqueName);

            return Task.CompletedTask;
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
                count = ConvertStaStationsFromJSON(json, staStationTable).Result;

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
        private async Task<int> ConvertStaStationsFromJSON(string json,
                                            TableInfo staStationTable)
        {
            int count = 0;
            JArray jArray = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);

            try
            {
                if (jArray != null)
                {
                    List<Task> tasks = new List<Task>();

                    foreach (JObject jObject in jArray)
                    {
                        tasks.Add(ThreadedConvertStaStationsFromJSON(jObject,
                                                                staStationTable));
                        count++;
                    }

                    await Task.WhenAll(tasks);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertStaStationsFromJSON");
            }

            return count;
        }

        private Task ThreadedConvertStaStationsFromJSON(JObject jObject,
                                            TableInfo staStationTable)
        {
            StaStation newStaStation = null;
            newStaStation = Newtonsoft.Json.JsonConvert.DeserializeObject<StaStation>(jObject.ToString());
            DatabaseManager.InsertRecordForType<StaStation>(staStationTable, newStaStation);

            return Task.CompletedTask;
        }
        #endregion
    }
}
