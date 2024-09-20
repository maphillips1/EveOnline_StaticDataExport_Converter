using EveStaticDataExportConverter.Classes.Database;
using EveStaticDataExportConverter.Classes.Universe;
using EveStaticDataExportConverter.Classes.Universe.Supporting_Classes;
using EveStaticDataExportConverter.Classes.Universe.SupportingClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace EveStaticDataExportConverter
{
    internal class UniverseConverter
    {
        string unversePath = "C:\\Users\\mrphi\\source\\repos\\EveStaticDataExportConverter\\json_sde\\universe";
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
        public bool ConvertUniverse()
        {
            string[] universeAreas = Directory.GetDirectories(unversePath);

            CreateTables();

            foreach (string universeArea in universeAreas)
            {
                ProcessArea(universeArea);
            }
            return true;
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

        private async void ProcessArea(string areaPath)
        {
            string[] regions = Directory.GetDirectories(areaPath);
            string regionTypeName = areaPath.Substring(areaPath.LastIndexOf('\\') + 1);

            List<Task> tasks = new List<Task>();
            foreach (string region in regions)
            {
                //Process regions concurrently. 
                tasks.Add(ProcessRegion(region, regionTypeName)); 
            }
            await Task.WhenAll(tasks);
        }

        private Task ProcessRegion(string regionPath, string regionTypeName)
        {
            string regionFileName = regionPath + "\\region.json";
            string regionName = regionPath.Substring(regionPath.LastIndexOf('\\') + 1);

            Console.WriteLine("");
            Console.WriteLine("Processing Region " + regionName);
            string json = File.ReadAllText(regionFileName);
            long regionID = ConvertRegionFromJSON(json, regionName, regionTypeName);

            string[] constellations = Directory.GetDirectories(regionPath);
            foreach (string constellation in constellations)
            {
                ProcessConstellation(constellation, regionID);
            }

            return Task.CompletedTask;
        }

        private long ConvertRegionFromJSON(string json, string regionName, string regionTypeName)
        {
            long regionID = 0;
            Region region = Newtonsoft.Json.JsonConvert.DeserializeObject<Region>(json);
            if (region != null)
            {
                regionID = region.regionID;
                region.regionName = regionName;
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

            Stopwatch sw = new Stopwatch();
            sw.Start();
            string json = File.ReadAllText(systemFileName);
            ConvertSystemFromJSON(json, systemName, regionID, constellationID);
            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " +  systemName);
        }

        private void ConvertSystemFromJSON(string json, string systemName, long regionID, long constellationID)
        {
            SolarSystem solarSystem = Newtonsoft.Json.JsonConvert.DeserializeObject<SolarSystem>(json);
            if (solarSystem != null)
            {
                solarSystem.regionID = regionID;
                solarSystem.constellationId = constellationID;
                solarSystem.solarSystemName = systemName;

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
                DatabaseManager.InsertRecordForType<SolarSystem>(solarSystemTableInfo, solarSystem);

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
                    DatabaseManager.InsertRecordForType<SolarSystemPlanet>(solarSystemPlanetTableInfo, planet);

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
                DatabaseManager.InsertRecordForType<PlanetAttributes>(planetAttributesTableInfo, planet.planetAttributes);
            }
        }

        private void InsertPlanetStatsRecords(SolarSystemPlanet planet)
        {
            if (planet.statistics != null)
            {
                planet.statistics.planetID = planet.planetID;
                DatabaseManager.InsertRecordForType<PlanetStatistics>(planetStatsTableInfo, planet.statistics);
            }
        }

        private void InsertPlanetMoonRecords(SolarSystemPlanet planet)
        {
            if (planet.moons?.Count > 0)
            {
                foreach (PlanetMoon moon in planet.moons)
                {
                    moon.planetID = planet.planetID;
                    DatabaseManager.InsertRecordForType<PlanetMoon>(planetMoonTableInfo, moon);

                    if (moon.planetAttributes != null)
                    {
                        moon.planetAttributes.moonID = moon.moonID;
                        DatabaseManager.InsertRecordForType<MoonAttributes>(moonAttributesTableInfo, moon.planetAttributes);
                    }

                    if (moon.statistics != null)
                    {
                        moon.statistics.moonID = moon.moonID;
                        DatabaseManager.InsertRecordForType<MoonStatistics>(moonStatisticsTableInfo, moon.statistics);
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
                    DatabaseManager.InsertRecordForType<PlanetAsteroidBelt>(planetAsteroidBeltTableInfo, asteroidBelt);

                    if (asteroidBelt.statistics != null)
                    {
                        asteroidBelt.statistics.asteroidBeltID = asteroidBelt.asteroidBeltID;
                        DatabaseManager.InsertRecordForType<AsteroidBetlStatistics>(asteroidBeltStatsTableInfo, asteroidBelt.statistics);
                    }
                }
            }
        }

        private void InsertStarRecords(SolarSystem solarSystem)
        {
            if (solarSystem.star != null)
            {
                solarSystem.star.solarSystemID = solarSystem.solarSystemID;
                DatabaseManager.InsertRecordForType<SolarSystemStar>(solarSystemStarTableInfo, solarSystem.star);


                if (solarSystem.star.statistics != null)
                {
                    solarSystem.star.statistics.starID = solarSystem.star.starID;
                    DatabaseManager.InsertRecordForType<StarStatistics>(starStatsTableInfo, solarSystem.star.statistics);
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
                    DatabaseManager.InsertRecordForType<SolarSystemStargate>(solarSystemStargateTableInfo, stargate);
                }
            }
        }
    }
}
