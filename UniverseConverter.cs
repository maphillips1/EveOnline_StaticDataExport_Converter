using EveStaticDataExportConverter.Classes.Database;
using EveStaticDataExportConverter.Classes.Universe;
using EveStaticDataExportConverter.Classes.Universe.Supporting_Classes;
using EveStaticDataExportConverter.Classes.Universe.SupportingClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace EveStaticDataExportConverter
{
    internal class UniverseConverter
    {
        string unversePath = "C:\\Users\\mrphi\\source\\repos\\EveStaticDataExportConverter\\EveOnline_StaticDataExport_Converter\\json_sde\\universe";
        TableInfo regionTableInfo;
        TableInfo constellationTableInfo;
        TableInfo solarSystemTableInfo;
        TableInfo solarSystemStargateTableInfo;
        TableInfo solarSystemStarTableInfo;
        TableInfo starStatsTableInfo;
        TableInfo solarSystemPlanetTableInfo;
        TableInfo planetAttributesTableInfo;
        TableInfo planetStatsTableInfo;
        TableInfo planetMoonTableInfo;
        TableInfo moonAttributesTableInfo;
        TableInfo moonStatisticsTableInfo;
        TableInfo planetAsteroidBeltTableInfo;
        TableInfo asteroidBeltStatsTableInfo;

        private List<SolarSystem> solarSystems = new List<SolarSystem>();
        private List<SolarSystemStar> solarSystemStars = new List<SolarSystemStar>();
        private List<SolarSystemPlanet> solarSystemPlanets = new List<SolarSystemPlanet>();
        private List<PlanetAttributes> planetAttributes = new List<PlanetAttributes>();
        private List<PlanetStatistics> planetStatistics = new List<PlanetStatistics>();
        private List<PlanetMoon> planetMoons = new List<PlanetMoon>();
        private List<MoonAttributes> moonAttributes = new List<MoonAttributes>();
        private List<MoonStatistics> MoonStatistics = new List<MoonStatistics>();
        private List<PlanetAsteroidBelt> planetAsteroidBelts = new List<PlanetAsteroidBelt>();
        private List<AsteroidBetlStatistics> AsteroidBetlStatistics = new List<AsteroidBetlStatistics>();
        private List<StarStatistics> starStats = new List<StarStatistics>();
        private List<SolarSystemStargate> solarSystemStargates = new List<SolarSystemStargate>();

        private List<SolarSystemSovereignty> sovereignties = null;

        public bool ConvertUniverse()
        {
            string[] universeAreas = Directory.GetDirectories(unversePath);

            CreateTables();
            GetSolarSystemSovereignties();

            foreach (string universeArea in universeAreas)
            {
                ProcessArea(universeArea);
            }
            HandleLeftoverRecords();
            return true;
        }

        private void GetSolarSystemSovereignties()
        {
            sovereignties = ESICalls.GetSolarSystemSovereignties();
        }

        private void CreateTables()
        {

            regionTableInfo = DatabaseManager.GetTableInfo(new Region());
            DatabaseManager.CreateTable(regionTableInfo);

            constellationTableInfo = DatabaseManager.GetTableInfo(new Constellation());
            DatabaseManager.CreateTable(constellationTableInfo);

            solarSystemTableInfo = DatabaseManager.GetTableInfo(new SolarSystem());
            DatabaseManager.CreateTable(solarSystemTableInfo);

            solarSystemStargateTableInfo = DatabaseManager.GetTableInfo(new SolarSystemStargate());
            DatabaseManager.CreateTable(solarSystemStargateTableInfo);

            solarSystemStarTableInfo = DatabaseManager.GetTableInfo(new SolarSystemStar());
            DatabaseManager.CreateTable(solarSystemStarTableInfo);

            starStatsTableInfo = DatabaseManager.GetTableInfo(new StarStatistics());
            DatabaseManager.CreateTable(starStatsTableInfo);

            solarSystemPlanetTableInfo = DatabaseManager.GetTableInfo(new SolarSystemPlanet());
            DatabaseManager.CreateTable(solarSystemPlanetTableInfo);

            planetAttributesTableInfo = DatabaseManager.GetTableInfo(new PlanetAttributes());
            DatabaseManager.CreateTable(planetAttributesTableInfo);

            planetStatsTableInfo = DatabaseManager.GetTableInfo(new PlanetStatistics());
            DatabaseManager.CreateTable(planetStatsTableInfo);

            planetMoonTableInfo = DatabaseManager.GetTableInfo(new PlanetMoon());
            DatabaseManager.CreateTable(planetMoonTableInfo);

            moonAttributesTableInfo = DatabaseManager.GetTableInfo(new MoonAttributes());
            DatabaseManager.CreateTable(moonAttributesTableInfo);

            moonStatisticsTableInfo = DatabaseManager.GetTableInfo(new MoonStatistics());
            DatabaseManager.CreateTable(moonStatisticsTableInfo);

            planetAsteroidBeltTableInfo = DatabaseManager.GetTableInfo(new PlanetAsteroidBelt());
            DatabaseManager.CreateTable(planetAsteroidBeltTableInfo);

            asteroidBeltStatsTableInfo = DatabaseManager.GetTableInfo(new AsteroidBetlStatistics());
            DatabaseManager.CreateTable(asteroidBeltStatsTableInfo);
        }

        private void HandleLeftoverRecords()
        {
            Utility.InsertBatchRecord<SolarSystem>(solarSystemTableInfo, solarSystems);
            Utility.InsertBatchRecord<SolarSystemStar>(solarSystemStarTableInfo, solarSystemStars);
            Utility.InsertBatchRecord<SolarSystemPlanet>(solarSystemPlanetTableInfo, solarSystemPlanets);
            Utility.InsertBatchRecord<PlanetAttributes>(planetAttributesTableInfo, planetAttributes);
            Utility.InsertBatchRecord<PlanetStatistics>(planetStatsTableInfo, planetStatistics);
            Utility.InsertBatchRecord<PlanetMoon>(planetMoonTableInfo, planetMoons);
            Utility.InsertBatchRecord<MoonAttributes>(moonAttributesTableInfo, moonAttributes);
            Utility.InsertBatchRecord<MoonStatistics>(moonStatisticsTableInfo, MoonStatistics);
            Utility.InsertBatchRecord<PlanetAsteroidBelt>(planetAsteroidBeltTableInfo, planetAsteroidBelts);
            Utility.InsertBatchRecord<AsteroidBetlStatistics>(asteroidBeltStatsTableInfo, AsteroidBetlStatistics);
            Utility.InsertBatchRecord<StarStatistics>(starStatsTableInfo, starStats);
            Utility.InsertBatchRecord<SolarSystemStargate>(solarSystemStargateTableInfo, solarSystemStargates);
        }

        private  void ProcessArea(string areaPath)
        {
            string[] regions = Directory.GetDirectories(areaPath);
            string regionTypeName = areaPath.Substring(areaPath.LastIndexOf('\\') + 1);

            foreach (string region in regions)
            {
                //Process regions concurrently. 
                ProcessRegion(region, regionTypeName); 
            }
        }

        private void ProcessRegion(string regionPath, string regionTypeName)
        {
            string regionFileName = regionPath + "\\region.json";
            string regionName = regionPath.Substring(regionPath.LastIndexOf('\\') + 1);

            Stopwatch sw = Stopwatch.StartNew();
            Console.WriteLine("");
            Console.WriteLine("Processing Region " + regionName);
            string json = File.ReadAllText(regionFileName);
            long regionID = ConvertRegionFromJSON(json, regionName, regionTypeName);

            string[] constellations = Directory.GetDirectories(regionPath);
            foreach (string constellation in constellations)
            {
                ProcessConstellation(constellation, regionID);
            }
            sw.Stop();
            Console.WriteLine();
            Utility.LogElapsedTime(sw, "Converting " + regionName);
        }

        private long ConvertRegionFromJSON(string json, string regionName, string regionTypeName)
        {
            long regionID = 0;
            Region region = Newtonsoft.Json.JsonConvert.DeserializeObject<Region>(json);
            if (region != null)
            {
                regionID = region.regionID;
                region.regionName = GetRegionNameWithSpaces(regionName);
                region.regionTypeName = regionTypeName;

                if (region.center?.Count > 0)
                {
                    region.centerX = region.center[0];
                    region.centerY = region.center[1];
                    region.centerZ = region.center[2];
                }

                if (region.max?.Count > 0)
                {
                    region.maxX = region.max[0];
                    region.maxY = region.max[1];
                    region.maxZ = region.max[2];
                }

                if (region.min?.Count > 0)
                {
                    region.minX = region.min[0];
                    region.minY = region.min[1];
                    region.minZ = region.min[2];
                }

                DatabaseManager.InsertRecordForType<Region>(regionTableInfo, region);
            }
            return regionID;
        }

        private string GetRegionNameWithSpaces(string regionName)
        {
            if (regionName == "ValeoftheSilent")
            {
                return "Vale of the Silent";
            }

            string newString = "";
            int count = 0;
            foreach (char c in regionName)
            {
                if (count > 0 && char.IsUpper(c) && regionName[count - 1] != '-' && (count - 1) != 0)
                {
                    newString += " ";
                }
                newString += c;
                count++;
            }
            return newString;
        }

        private void ProcessConstellation(string constellationPath, long regionID)
        {
            string constellationFileName = constellationPath + "\\constellation.json";
            string constellationName = constellationPath.Substring(constellationPath.LastIndexOf('\\') + 1);

            Console.WriteLine("");
            Console.WriteLine("Processing Constellation " + constellationName);
            string json = File.ReadAllText(constellationFileName);
            long constellationID = ConvertConstellationFromJSON(json, constellationName, regionID);

            string[] systems = Directory.GetDirectories(constellationPath);
            foreach (string system in systems)
            {
                ProcessSystem(system, regionID, constellationID);
            }
        }

        private long ConvertConstellationFromJSON(string json, string constellationName, long regionID)
        {
            long constellationID = 0;
            Constellation constellation = Newtonsoft.Json.JsonConvert.DeserializeObject<Constellation>(json);
            if (constellation != null)
            {
                constellationID = constellation.constellationID;
                constellation.constellationName = constellationName;
                constellation.regionID = regionID;

                if (constellation.center?.Count > 0)
                {
                    constellation.centerX = constellation.center[0];
                    constellation.centerY = constellation.center[1];
                    constellation.centerZ = constellation.center[2];
                }

                if (constellation.max?.Count > 0)
                {
                    constellation.maxX = constellation.max[0];
                    constellation.maxY = constellation.max[1];
                    constellation.maxZ = constellation.max[2];
                }

                if (constellation.min?.Count > 0)
                {
                    constellation.minX = constellation.min[0];
                    constellation.minY = constellation.min[1];
                    constellation.minZ = constellation.min[2];
                }

                DatabaseManager.InsertRecordForType<Constellation>(constellationTableInfo, constellation);
            }
            return constellationID;
        }

        private void ProcessSystem(string systemPath, long regionID, long constellationID)
        {
            string systemFileName = systemPath + "\\solarsystem.json";
            string systemName = systemPath.Substring(systemPath.LastIndexOf('\\') + 1);

            Console.WriteLine("Converting " + systemName);
            string json = File.ReadAllText(systemFileName);
            ConvertSystemFromJSON(json, systemName, regionID, constellationID);
        }

        private void ConvertSystemFromJSON(string json, string systemName, long regionID, long constellationID)
        {
            SolarSystem solarSystem = Newtonsoft.Json.JsonConvert.DeserializeObject<SolarSystem>(json);
            if (solarSystem != null)
            {
                solarSystem.regionID = regionID;
                solarSystem.constellationId = constellationID;
                solarSystem.solarSystemName = systemName;

                SolarSystemSovereignty sovInfo = sovereignties.Find(x => x.system_id == solarSystem.solarSystemID);
                if (sovInfo != null)
                {
                    solarSystem.factionID = sovInfo.faction_id;
                }

                if (solarSystem.center?.Count > 0)
                {
                    solarSystem.centerX = solarSystem.center[0];
                    solarSystem.centerY = solarSystem.center[1];
                    solarSystem.centerZ = solarSystem.center[2];
                }

                if (solarSystem.max?.Count > 0)
                {
                    solarSystem.maxX = solarSystem.max[0];
                    solarSystem.maxY = solarSystem.max[1];
                    solarSystem.maxZ = solarSystem.max[2];
                }

                if (solarSystem.min?.Count > 0)
                {
                    solarSystem.minX = solarSystem.min[0];
                    solarSystem.minY = solarSystem.min[1];
                    solarSystem.minZ = solarSystem.min[2];
                }
                Utility.AddRecordToBatch<SolarSystem>(solarSystemTableInfo, ref solarSystems, solarSystem);

                InsertPlanetRecords(solarSystem);
                InsertStarRecords(solarSystem);
                InsertStargateRecords(solarSystem);
            }
        }

        private void InsertPlanetRecords(SolarSystem solarSystem)
        {
            if (solarSystem.planets?.Count > 0)
            {
                foreach (SolarSystemPlanet planet in solarSystem.planets)
                {
                    planet.solarSystemID = solarSystem.solarSystemID;
                    Utility.AddRecordToBatch<SolarSystemPlanet>(solarSystemPlanetTableInfo, ref solarSystemPlanets, planet);
                    
                    InsertPlanetAttributeRecords(planet);
                    InsertPlanetMoonRecords(planet);
                    InsertPlanetAsteroidBeltRecords(planet);
                    InsertPlanetStatsRecords(planet);
                }
            }
        }

        private void InsertPlanetAttributeRecords(SolarSystemPlanet planet)
        {
            if (planet.planetAttributes != null)
            {
                planet.planetAttributes.planetID = planet.planetID;
                Utility.AddRecordToBatch<PlanetAttributes>(planetAttributesTableInfo, ref planetAttributes, planet.planetAttributes);
                
            }
        }

        private void InsertPlanetStatsRecords(SolarSystemPlanet planet)
        {
            if (planet.statistics != null)
            {
                planet.statistics.planetID = planet.planetID;
                Utility.AddRecordToBatch<PlanetStatistics>(planetStatsTableInfo, ref planetStatistics, planet.statistics);

            }
        }

        private void InsertPlanetMoonRecords(SolarSystemPlanet planet)
        {
            if (planet.moons?.Count > 0)
            {
                foreach (PlanetMoon moon in planet.moons)
                {
                    moon.planetID = planet.planetID;
                    Utility.AddRecordToBatch<PlanetMoon>(planetMoonTableInfo, ref planetMoons, moon);

                    if (moon.planetAttributes != null)
                    {
                        moon.planetAttributes.moonID = moon.moonID;
                        Utility.AddRecordToBatch<MoonAttributes>(moonAttributesTableInfo, ref moonAttributes, moon.planetAttributes);
                    }

                    if (moon.statistics != null)
                    {
                        moon.statistics.moonID = moon.moonID;
                        Utility.AddRecordToBatch<MoonStatistics>(moonStatisticsTableInfo, ref MoonStatistics, moon.statistics);
                    }
                }
            }
        }

        private void InsertPlanetAsteroidBeltRecords(SolarSystemPlanet planet)
        {
            if (planet.asteroidBelts?.Count > 0)
            {
                foreach (PlanetAsteroidBelt asteroidBelt in planet.asteroidBelts)
                {
                    asteroidBelt.planetID = planet.planetID;
                    Utility.AddRecordToBatch<PlanetAsteroidBelt>(planetAsteroidBeltTableInfo, ref planetAsteroidBelts, asteroidBelt);
                    

                    if (asteroidBelt.statistics != null)
                    {
                        asteroidBelt.statistics.asteroidBeltID = asteroidBelt.asteroidBeltID;
                        Utility.AddRecordToBatch<AsteroidBetlStatistics>(asteroidBeltStatsTableInfo, ref AsteroidBetlStatistics, asteroidBelt.statistics);
                    }
                }
            }
        }

        private void InsertStarRecords(SolarSystem solarSystem)
        {
            if (solarSystem.star != null)
            {
                solarSystem.star.solarSystemID = solarSystem.solarSystemID;
                Utility.AddRecordToBatch<SolarSystemStar>(solarSystemStarTableInfo, ref solarSystemStars, solarSystem.star);


                if (solarSystem.star.statistics != null)
                {
                    solarSystem.star.statistics.starID = solarSystem.star.starID;
                    Utility.AddRecordToBatch<StarStatistics>(starStatsTableInfo, ref starStats, solarSystem.star.statistics);
                }
            }
        }

        private void InsertStargateRecords(SolarSystem solarSystem)
        {
            if (solarSystem.stargates?.Count > 0)
            {
                foreach (SolarSystemStargate stargate in solarSystem.stargates)
                {
                    stargate.solarSystemID = solarSystem.solarSystemID;
                    Utility.AddRecordToBatch<SolarSystemStargate>(solarSystemStargateTableInfo, ref solarSystemStargates, stargate);
                }
            }
        }
    }
}
