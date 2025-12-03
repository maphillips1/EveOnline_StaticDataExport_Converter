using EveStaticDataExportConverter.Classes.Attributes;
using EveStaticDataExportConverter.Classes.Database;
using EveStaticDataExportConverter.Classes.SDEClasses.Supporting_Classes;
using EveStaticDataExportConverter.Classes.SDEClasses;
using EveStaticDataExportConverter.Classes.Universe;
using EveStaticDataExportConverter.Classes.Universe.Supporting_Classes;
using EveStaticDataExportConverter.Classes.Universe.SupportingClasses;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter
{
    public class JSONConverter
    {
        string SDEPath = "C:\\Users\\mrphi\\source\\repos\\EveStaticDataExportConverter\\EveOnline_StaticDataExport_Converter\\json_sde";


        public bool ConvertSDE()
        {
            bool success = true;

            Stopwatch sw = Stopwatch.StartNew();

            success &= ConvertAgentsInSpace();
            success &= ConvertAgentTypes();
            success &= ConvertAncestry();
            success &= ConvertBloodlines();
            success &= ConvertBlueprints();
            success &= ConvertCategories();
            success &= ConvertCertificates();
            success &= ConvertCharacterAttributes();
            success &= ConvertContrabandTypes();
            success &= ConvertControlTowerResources();
            success &= ConvertCorporationActivities();
            success &= ConvertDBuffCollection();
            success &= ConvertDogmaAttributeCategories();
            success &= ConvertDogmaAttributes();
            success &= ConvertDogmaEffects();
            success &= ConvertDogmaUnits();
            success &= ConvertFactions();
            success &= ConvertGraphics();
            success &= ConvertGroups();
            success &= ConvertIcons();
            success &= ConvertLandmarks();
            success &= ConvertMapAsteroidBelts();
            success &= ConvertMapConstellations();
            success &= ConvertMapMoons();
            success &= ConvertMapPlanets();
            success &= ConvertMapRegions();
            success &= ConvertMapSolarSystems();
            success &= ConvertMapStargates();
            success &= ConvertMapStars();
            success &= ConvertMarketGroups();
            success &= ConvertMasteries();
            success &= ConvertMetaGroups();
            success &= ConvertNPCCharacters();
            success &= ConvertNPCCorpDivisions();
            success &= ConvertNPCCorps();
            success &= ConvertNPCStations();
            success &= ConvertPlanetResources();
            success &= ConvertPlanetSchematics();
            success &= ConvertRaces();
            success &= ConvertSkinLicense();
            success &= ConvertSkinMaterials();
            success &= ConvertSkins();
            success &= ConvertSovereigntyUpgrades();
            success &= ConvertStationOperations();
            success &= ConvertStationServices();
            success &= ConvertTranslationLanguages();
            success &= ConvertTypeBonuses();
            success &= ConvertTypeDogmas();
            success &= ConvertTypeMaterials();
            success &= ConvertTypes();

            sw.Stop();

            Utility.LogElapsedTime(sw, "Converting the Static Data Export took: ");

            return success;
        }

        private int ConvertFileForType<T>(TableInfo tableInfo, string fileName)
        {
            int count = 0;
            T newObject;
            List<T> newBatch = new List<T>();

            string path = SDEPath + fileName;
            StringReader reader = new StringReader(path);
            string jsonObject = "";
            using (StreamReader sr = new StreamReader(path))
            {
                while (sr.Peek() >= 0)
                {
                    jsonObject = sr.ReadLine();
                    newObject = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonObject);
                    if (newObject != null)
                    {
                        Utility.AddRecordToBatch<T>(tableInfo, ref newBatch, newObject);
                        count++;
                    }
                }
            }
            if (newBatch.Count > 0)
            {
                Utility.InsertBatchRecord<T>(tableInfo, newBatch);
            }
            return count;
        }

        #region "AgentsInSpace"
        private bool ConvertAgentsInSpace()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {

                AgentInSpace tableInfoAgentInSpace = new AgentInSpace();
                TableInfo agentsInSpaceTable = DatabaseManager.GetTableInfo<AgentInSpace>(tableInfoAgentInSpace);
                DatabaseManager.CreateTable(agentsInSpaceTable);


                Console.WriteLine("Converting Agents in space");
                count = ConvertFileForType<AgentInSpace>(agentsInSpaceTable, "\\agentsInSpace.jsonl");
                Console.WriteLine("Done Converting Agents in space");
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Agents Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Agents in space");
            return success;
        }
        #endregion

        #region "Agent Types"
        private bool ConvertAgentTypes()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {

                AgentType tableInfoAgentType = new AgentType();
                TableInfo agentTypeTable = DatabaseManager.GetTableInfo<AgentType>(tableInfoAgentType);
                DatabaseManager.CreateTable(agentTypeTable);


                Console.WriteLine("Converting Agent Types");
                count = ConvertFileForType<AgentType>(agentTypeTable, "\\agentTypes.jsonl");
                Console.WriteLine("Done Converting Agent Types");
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Agent Types Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Agent Types");
            return success;
        }
        #endregion

        #region "Ancestry"
        private bool ConvertAncestry()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                Ancestry tableInfoAncestry = new Ancestry();
                TableInfo ancestryTable = DatabaseManager.GetTableInfo<Ancestry>(tableInfoAncestry);

                DatabaseManager.CreateTable(ancestryTable);
                LanguageDescription tableInfoLD = new LanguageDescription();
                TableInfo LanguageDscrTableInfo = DatabaseManager.GetTableInfo(tableInfoLD);
                LanguageDescription TableInfoName = new LanguageDescription();
                TableInfo nameTableInfo = DatabaseManager.GetTableInfo(tableInfoLD);

                string dscrTable = "AncestryDescription";
                LanguageDscrTableInfo.Name = dscrTable;
                DatabaseManager.CreateTable(LanguageDscrTableInfo);

                string nameTable = "AncestryName";
                nameTableInfo.Name = nameTable;
                DatabaseManager.CreateTable(nameTableInfo);

                Console.WriteLine("Converting Ancestries");
                count = ConvertAncestries(ancestryTable, LanguageDscrTableInfo, nameTableInfo);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Ancestry Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Ancestries ");
            return success;
        }

        private int ConvertAncestries(TableInfo ancestryTableInfo, TableInfo languageDescriptionTableInfo, TableInfo nameTableInfo)
        {
            int count = 0;
            Ancestry newObject;
            List<Ancestry> newBatch = new List<Ancestry>();
            List<LanguageDescription> descriptionBatch = new List<LanguageDescription>();
            List<LanguageDescription> nameBatch = new List<LanguageDescription>();

            string path = SDEPath + "\\ancestries.jsonl";
            StringReader reader = new StringReader(path);
            string jsonObject = "";
            using (StreamReader sr = new StreamReader(path))
            {
                while (sr.Peek() >= 0)
                {
                    jsonObject = sr.ReadLine();
                    newObject = Newtonsoft.Json.JsonConvert.DeserializeObject<Ancestry>(jsonObject);
                    if (newObject != null)
                    {
                        Utility.AddRecordToBatch<Ancestry>(ancestryTableInfo, ref newBatch, newObject);
                        count++;

                        newObject.description.parentTypeId = newObject.GetKey1();
                        newObject.description.parentTypeId2 = newObject.GetKey2();
                        newObject.description.parentTypeCategory = newObject.GetCategory();
                        Utility.AddRecordToBatch<LanguageDescription>(languageDescriptionTableInfo, ref descriptionBatch, newObject.description);


                        newObject.name.parentTypeId = newObject.GetKey1();
                        newObject.name.parentTypeId2 = newObject.GetKey2();
                        newObject.name.parentTypeCategory = newObject.GetCategory();
                        Utility.AddRecordToBatch<LanguageDescription>(nameTableInfo, ref nameBatch, newObject.name);
                    }
                }
            }
            if (newBatch.Count > 0)
            {
                Utility.InsertBatchRecord<Ancestry>(ancestryTableInfo, newBatch);
            }
            if (descriptionBatch.Count > 0)
            {
                Utility.InsertBatchRecord<LanguageDescription>(languageDescriptionTableInfo, descriptionBatch);
            }
            if (nameBatch.Count > 0)
            {
                Utility.InsertBatchRecord<LanguageDescription>(nameTableInfo, nameBatch);
            }
            return count;
        }
        #endregion

        #region "Bloodlines"
        private bool ConvertBloodlines()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {

                Bloodlines tableInfoBloodlines = new Bloodlines();
                TableInfo bloodLinesTable = DatabaseManager.GetTableInfo<Bloodlines>(tableInfoBloodlines);
                DatabaseManager.CreateTable(bloodLinesTable);

                LanguageDescription tableInfoLD = new LanguageDescription();
                TableInfo descriptionTableInfo = DatabaseManager.GetTableInfo(tableInfoLD);

                LanguageDescription tableInfoName = new LanguageDescription();
                TableInfo nameTableInfo = DatabaseManager.GetTableInfo(tableInfoLD);

                string dscrTable = "BloodlinesDescription";
                descriptionTableInfo.Name = dscrTable;
                DatabaseManager.CreateTable(descriptionTableInfo);

                nameTableInfo.Name = "BloodlinesName";
                DatabaseManager.CreateTable(nameTableInfo);

                Console.WriteLine("Converting Bloodlines");
                count = ConvertBloodlinesFromJson(bloodLinesTable, descriptionTableInfo, nameTableInfo);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Bloodlines Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Bloodlines ");
            return success;
        }

        private int ConvertBloodlinesFromJson(TableInfo bloodlinesTableInfo,
                                            TableInfo descriptionTableInfo,
                                            TableInfo nameTableInfo)
        {
            int count = 0;
            Bloodlines newObject;
            List<Bloodlines> newBatch = new List<Bloodlines>();
            List<LanguageDescription> descriptionBatch = new List<LanguageDescription>();
            List<LanguageDescription> nameBatch = new List<LanguageDescription>();
            string path = SDEPath + "\\bloodlines.jsonl";
            string jsonObject = "";

            using (StreamReader sr = new StreamReader(path))
            {
                while (sr.Peek() >= 0)
                {
                    jsonObject = sr.ReadLine();
                    newObject = Newtonsoft.Json.JsonConvert.DeserializeObject<Bloodlines>(jsonObject);
                    if (newObject != null)
                    {
                        Utility.AddRecordToBatch<Bloodlines>(bloodlinesTableInfo, ref newBatch, newObject);
                        count++;

                        newObject.description.parentTypeId = newObject.bloodlinesID;
                        Utility.AddRecordToBatch<LanguageDescription>(descriptionTableInfo, ref descriptionBatch, newObject.description);


                        newObject.name.parentTypeId = newObject.bloodlinesID;
                        Utility.AddRecordToBatch<LanguageDescription>(nameTableInfo, ref nameBatch, newObject.name);
                    }
                }
            }
            if (newBatch.Count > 0)
            {
                Utility.InsertBatchRecord<Bloodlines>(bloodlinesTableInfo, newBatch);
            }
            if (descriptionBatch.Count > 0)
            {
                Utility.InsertBatchRecord<LanguageDescription>(descriptionTableInfo, descriptionBatch);
            }
            if (nameBatch.Count > 0)
            {
                Utility.InsertBatchRecord<LanguageDescription>(nameTableInfo, nameBatch);
            }
            return count;
        }
        #endregion

        #region "Blueprints"
        private bool ConvertBlueprints()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                //Create all the BP Tables. 
                Blueprints tableInfoblueprints = new Blueprints();
                TableInfo blueprintsTable = DatabaseManager.GetTableInfo<Blueprints>(tableInfoblueprints);
                DatabaseManager.CreateTable(blueprintsTable);

                BlueprintActivityType tableInfoBpActType = new BlueprintActivityType();
                TableInfo bpActivityTypeTableInfo = DatabaseManager.GetTableInfo(tableInfoBpActType);
                DatabaseManager.CreateTable(bpActivityTypeTableInfo);

                BlueprintActivityMaterial tableInfoActivityMaterial = new BlueprintActivityMaterial();
                TableInfo activityMaterialTableInfo = DatabaseManager.GetTableInfo(tableInfoActivityMaterial);
                DatabaseManager.CreateTable(activityMaterialTableInfo);

                BlueprintProduct tableInfoBPProduct = new BlueprintProduct();
                TableInfo bpProductTableInfo = DatabaseManager.GetTableInfo(tableInfoBPProduct);
                DatabaseManager.CreateTable(bpProductTableInfo);

                BlueprintSkill tableInfoBPActivitySkill = new BlueprintSkill();
                TableInfo bpActivitySkillTableInfo = DatabaseManager.GetTableInfo(tableInfoBPActivitySkill);
                DatabaseManager.CreateTable(bpActivitySkillTableInfo);

                Console.WriteLine("Converting Blueprints");

                count = ConvertBlueprintsFromJSON(blueprintsTable,
                                                    bpActivityTypeTableInfo,
                                                    activityMaterialTableInfo,
                                                    bpProductTableInfo,
                                                    bpActivitySkillTableInfo);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting blueprints Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Blueprints ");
            return success;
        }

        private int ConvertBlueprintsFromJSON(TableInfo blueprintsTable,
                                              TableInfo bpActivityTypeTable,
                                              TableInfo bpActivityMatTable,
                                              TableInfo bpProductTable,
                                              TableInfo bpActivitySkillTable)
        {
            int count = 0;
            Blueprints newBlueprint = null;
            string path = SDEPath + "\\blueprints.jsonl";
            string json = "";
            Blueprints newObject = null;
            List<Blueprints> batchBlureprints = new List<Blueprints>();
            List<BlueprintActivityType> batchActivityTypes = new List<BlueprintActivityType>();
            List<BlueprintActivityMaterial> batchMaterials = new List<BlueprintActivityMaterial>();
            List<BlueprintProduct> batchProducts = new List<BlueprintProduct>();
            List<BlueprintSkill> batchSkills = new List<BlueprintSkill>();

            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        json = sr.ReadLine();
                        newBlueprint = Newtonsoft.Json.JsonConvert.DeserializeObject<Blueprints>(json);
                        Utility.AddRecordToBatch<Blueprints>(blueprintsTable, ref batchBlureprints, newBlueprint);

                        if (newBlueprint.activities.copying != null)
                        {
                            SetActivityInfoForType(bpActivityTypeTable,
                                                   bpActivityMatTable,
                                                   bpActivitySkillTable,
                                                   bpProductTable,
                                                   newBlueprint.activities.copying,
                                                   "copying",
                                                   newBlueprint.blueprintTypeID,
                                                   ref batchActivityTypes,
                                                   ref batchMaterials,
                                                   ref batchProducts,
                                                   ref batchSkills);
                        }

                        if (newBlueprint.activities.manufacturing != null)
                        {
                            SetActivityInfoForType(bpActivityTypeTable,
                                                   bpActivityMatTable,
                                                   bpActivitySkillTable,
                                                   bpProductTable,
                                                   newBlueprint.activities.manufacturing,
                                                   "manufacturing",
                                                   newBlueprint.blueprintTypeID,
                                                   ref batchActivityTypes,
                                                   ref batchMaterials,
                                                   ref batchProducts,
                                                   ref batchSkills);
                        }

                        if (newBlueprint.activities.reaction != null)
                        {
                            SetActivityInfoForType(bpActivityTypeTable,
                                                   bpActivityMatTable,
                                                   bpActivitySkillTable,
                                                   bpProductTable,
                                                   newBlueprint.activities.reaction,
                                                   "reaction",
                                                   newBlueprint.blueprintTypeID,
                                                   ref batchActivityTypes,
                                                   ref batchMaterials,
                                                   ref batchProducts,
                                                   ref batchSkills);
                        }

                        if (newBlueprint.activities.invention != null)
                        {
                            SetActivityInfoForType(bpActivityTypeTable,
                                                   bpActivityMatTable,
                                                   bpActivitySkillTable,
                                                   bpProductTable,
                                                   newBlueprint.activities.invention,
                                                   "invention",
                                                   newBlueprint.blueprintTypeID,
                                                   ref batchActivityTypes,
                                                   ref batchMaterials,
                                                   ref batchProducts,
                                                   ref batchSkills);
                        }

                        if (newBlueprint.activities.research_material != null)
                        {
                            SetActivityInfoForType(bpActivityTypeTable,
                                                   bpActivityMatTable,
                                                   bpActivitySkillTable,
                                                   bpProductTable,
                                                   newBlueprint.activities.research_material,
                                                   "research_material",
                                                   newBlueprint.blueprintTypeID,
                                                   ref batchActivityTypes,
                                                   ref batchMaterials,
                                                   ref batchProducts,
                                                   ref batchSkills);
                        }

                        if (newBlueprint.activities.research_time != null)
                        {
                            SetActivityInfoForType(bpActivityTypeTable,
                                                   bpActivityMatTable,
                                                   bpActivitySkillTable,
                                                   bpProductTable,
                                                   newBlueprint.activities.research_time,
                                                   "research_time",
                                                   newBlueprint.blueprintTypeID,
                                                   ref batchActivityTypes,
                                                   ref batchMaterials,
                                                   ref batchProducts,
                                                   ref batchSkills);
                        }
                        count++;
                        Utility.InsertBatchRecord<Blueprints>(blueprintsTable, batchBlureprints);
                        Utility.InsertBatchRecord<BlueprintActivityType>(bpActivityTypeTable, batchActivityTypes);
                        Utility.InsertBatchRecord<BlueprintActivityMaterial>(bpActivityMatTable, batchMaterials);
                        Utility.InsertBatchRecord<BlueprintProduct>(bpProductTable, batchProducts);
                        Utility.InsertBatchRecord<BlueprintSkill>(bpActivitySkillTable, batchSkills);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertBlueprintsFromJSON");
            }

            return count;
        }

        private void SetActivityInfoForType(TableInfo activityTableInfo,
                                            TableInfo materialTableInfo,
                                            TableInfo skillTableInfo,
                                            TableInfo productTableInfo,
                                            BlueprintActivityType activityType,
                                            string activityName,
                                            int blueprintTypeId,
                                            ref List<BlueprintActivityType> batchActivityTypes,
                                            ref List<BlueprintActivityMaterial> batchMaterials,
                                            ref List<BlueprintProduct> batchProducts,
                                            ref List<BlueprintSkill> batchSkills)
        {
            if (activityType != null)
            {
                activityType.blueprintTypeID = blueprintTypeId;
                activityType.activityName = activityName;

                Utility.AddRecordToBatch<BlueprintActivityType>(activityTableInfo, ref batchActivityTypes, activityType);

                if (activityType.materials?.Count > 0)
                {
                    foreach (BlueprintActivityMaterial activityMaterial in activityType.materials)
                    {
                        activityMaterial.blueprintTypeID = blueprintTypeId;
                        activityMaterial.activityName = activityName;
                        Utility.AddRecordToBatch<BlueprintActivityMaterial>(materialTableInfo, ref batchMaterials, activityMaterial);
                    }
                }

                if (activityType.products?.Count > 0)
                {
                    foreach (BlueprintProduct product in activityType.products)
                    {
                        product.blueprintTypeID = blueprintTypeId;
                        product.activityName = activityName;
                        Utility.AddRecordToBatch<BlueprintProduct>(productTableInfo, ref batchProducts, product);
                    }
                }

                if (activityType.skills?.Count > 0)
                {
                    foreach (BlueprintSkill skill in activityType.skills)
                    {
                        skill.parentTypeId = blueprintTypeId;
                        skill.activityName = activityName;
                        Utility.AddRecordToBatch<BlueprintSkill>(skillTableInfo, ref batchSkills, skill);
                    }
                }
            }
        }
        #endregion

        #region "Categories"
        private bool ConvertCategories()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = SDEPath + "\\categories.jsonl";

                //Create all the BP Tables. 
                Category tableInfoCategory = new Category();
                TableInfo categoryTable = DatabaseManager.GetTableInfo<Category>(tableInfoCategory);
                DatabaseManager.CreateTable(categoryTable);

                //Category Name table
                LanguageDescription tableInfoLD = new LanguageDescription();
                TableInfo languageDescriptionTableInfo = DatabaseManager.GetTableInfo<LanguageDescription>(tableInfoLD);
                languageDescriptionTableInfo.Name = "CategoryName";
                DatabaseManager.CreateTable(languageDescriptionTableInfo);

                Console.WriteLine("Converting Categories");

                count = ConvertCategoriesFromJSON(path,
                                                    categoryTable,
                                                    languageDescriptionTableInfo);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting certificates Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Categories ");
            return success;
        }

        private int ConvertCategoriesFromJSON(string path, TableInfo categoryTableIndo, TableInfo langTableInfo)
        {
            int count = 0;
            Category newCategory = null;
            List<Category> batchCategories = new List<Category>();
            List<LanguageDescription> batchNames = new List<LanguageDescription>();
            string json = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        json = sr.ReadLine();
                        newCategory = Newtonsoft.Json.JsonConvert.DeserializeObject<Category>(json);
                        Utility.AddRecordToBatch<Category>(categoryTableIndo, ref batchCategories, newCategory);


                        newCategory.name.parentTypeId = newCategory.categoryID;
                        Utility.AddRecordToBatch<LanguageDescription>(langTableInfo, ref batchNames, newCategory.name);

                        count++;
                    }
                }
                Utility.InsertBatchRecord<Category>(categoryTableIndo, batchCategories);
                Utility.InsertBatchRecord<LanguageDescription>(langTableInfo, batchNames);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertCategoriesFromJSON");
            }

            return count;
        }
        #endregion

        #region "Certificates"
        private bool ConvertCertificates()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = SDEPath + "\\certificates.jsonl";

                Certificate tableInfoCertificate = new Certificate();
                TableInfo certificateTable = DatabaseManager.GetTableInfo<Certificate>(tableInfoCertificate);
                DatabaseManager.CreateTable(certificateTable);

                CertificateSkillType tableInfoCertificateSkillType = new CertificateSkillType();
                TableInfo certificateSkillTypTableInfo = DatabaseManager.GetTableInfo<CertificateSkillType>(tableInfoCertificateSkillType);
                DatabaseManager.CreateTable(certificateSkillTypTableInfo);

                CertificateRecommendedForType tableInfoCertificateRecommended = new CertificateRecommendedForType();
                TableInfo certificateRecommendedTableInfo = DatabaseManager.GetTableInfo<CertificateRecommendedForType>(tableInfoCertificateRecommended);
                DatabaseManager.CreateTable(certificateRecommendedTableInfo);

                LanguageDescription tableInfoCertDescription = new LanguageDescription();
                TableInfo certDescriptionTableInfo = DatabaseManager.GetTableInfo<LanguageDescription>(tableInfoCertDescription);
                certDescriptionTableInfo.Name = "CertificateDescription";
                DatabaseManager.CreateTable(certDescriptionTableInfo);

                LanguageDescription tableInfoCertName = new LanguageDescription();
                TableInfo certNameTableInfo = DatabaseManager.GetTableInfo<LanguageDescription>(tableInfoCertName);
                certNameTableInfo.Name = "CertificateName";
                DatabaseManager.CreateTable(certNameTableInfo);

                Console.WriteLine("Converting Certificates");

                count = ConvertCertificatesFromJSON(path,
                                                   certificateTable,
                                                   certificateSkillTypTableInfo,
                                                   certificateRecommendedTableInfo,
                                                   certDescriptionTableInfo,
                                                   certNameTableInfo);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Certificates Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Certificates ");
            return success;
        }

        private int ConvertCertificatesFromJSON(string path,
                                                TableInfo certificateTable,
                                                TableInfo certificateSkillTypTableInfo,
                                                TableInfo certificateRecommendedForTypeTableInfo,
                                                TableInfo certificateDescriptionTableInfo,
                                                TableInfo certificateNameTableInfo)
        {
            int count = 0;
            Certificate newCertificate = null;
            CertificateRecommendedForType certificateRecommendedFor = null;

            List<Certificate> certificates = new List<Certificate>();
            List<CertificateRecommendedForType> certificateRecommendedForTypesBatch = new List<CertificateRecommendedForType>();
            List<CertificateSkillType> skillTypeBatch = new List<CertificateSkillType>();
            List<LanguageDescription> descriptionBatch = new List<LanguageDescription>();
            List<LanguageDescription> nameBatch = new List<LanguageDescription>();

            string json = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        json = sr.ReadLine();
                        newCertificate = Newtonsoft.Json.JsonConvert.DeserializeObject<Certificate>(json);
                        Utility.AddRecordToBatch<Certificate>(certificateTable, ref certificates, newCertificate);


                        foreach (int typeId in newCertificate.recommendedFor)
                        {
                            certificateRecommendedFor = new CertificateRecommendedForType()
                            { certificateID = newCertificate.certificateID, recommendedForTypeID = typeId };
                            Utility.AddRecordToBatch<CertificateRecommendedForType>(certificateRecommendedForTypeTableInfo, ref certificateRecommendedForTypesBatch, certificateRecommendedFor);

                        }

                        if (newCertificate.skillTypes?.Count > 0)
                        {
                            foreach (CertificateSkillType skillType in newCertificate.skillTypes)
                            {
                                skillType.certificateID = newCertificate.certificateID;
                                Utility.AddRecordToBatch<CertificateSkillType>(certificateSkillTypTableInfo, ref skillTypeBatch, skillType);

                            }
                        }

                        newCertificate.description.parentTypeId = newCertificate.certificateID;
                        Utility.AddRecordToBatch<LanguageDescription>(certificateDescriptionTableInfo, ref descriptionBatch, newCertificate.description);
                        newCertificate.name.parentTypeId = newCertificate.certificateID;
                        Utility.AddRecordToBatch<LanguageDescription>(certificateNameTableInfo, ref nameBatch, newCertificate.name);


                        count++;
                    }
                }
                Utility.InsertBatchRecord<Certificate>(certificateTable, certificates);
                Utility.InsertBatchRecord<CertificateRecommendedForType>(certificateRecommendedForTypeTableInfo, certificateRecommendedForTypesBatch);
                Utility.InsertBatchRecord<CertificateSkillType>(certificateSkillTypTableInfo, skillTypeBatch);
                Utility.InsertBatchRecord<LanguageDescription>(certificateDescriptionTableInfo, descriptionBatch);
                Utility.InsertBatchRecord<LanguageDescription>(certificateNameTableInfo, nameBatch);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertCategoriesFromJSON");
            }

            return count;
        }
        #endregion

        #region "Character Attributes"
        private bool ConvertCharacterAttributes()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = SDEPath + "\\characterAttributes.jsonl";


                CharacterAttribute tableInfoCharacterAttribute = new CharacterAttribute();
                TableInfo charAttributeTable = DatabaseManager.GetTableInfo<CharacterAttribute>(tableInfoCharacterAttribute);
                DatabaseManager.CreateTable(charAttributeTable);

                LanguageDescription tableInfoName = new LanguageDescription();
                TableInfo nameTableInfo = DatabaseManager.GetTableInfo<LanguageDescription>(tableInfoName);
                nameTableInfo.Name = "CharacterAttributeName";
                DatabaseManager.CreateTable(nameTableInfo);

                Console.WriteLine("Converting Character Attributes");

                count = ConvertCharacterAttributesFromJSON(path,
                                                                charAttributeTable,
                                                                nameTableInfo);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Character Attributes Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Character Attributes ");
            return success;
        }

        private int ConvertCharacterAttributesFromJSON(string path,
                                                        TableInfo charAttributeTable,
                                                        TableInfo nameTableInfo)
        {
            int count = 0;
            CharacterAttribute newCharAttribute = null;
            List<CharacterAttribute> batchAttributes = new List<CharacterAttribute>();
            List<LanguageDescription> batchNames = new List<LanguageDescription>();
            string json = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        json = sr.ReadLine();
                        newCharAttribute = Newtonsoft.Json.JsonConvert.DeserializeObject<CharacterAttribute>(json);
                        Utility.AddRecordToBatch<CharacterAttribute>(charAttributeTable, ref batchAttributes, newCharAttribute);

                        newCharAttribute.name.parentTypeId = newCharAttribute.characterAttributeID;
                        Utility.AddRecordToBatch<LanguageDescription>(nameTableInfo, ref batchNames, newCharAttribute.name);

                        count++;
                    }
                }
                Utility.InsertBatchRecord<CharacterAttribute>(charAttributeTable, batchAttributes);
                Utility.InsertBatchRecord<LanguageDescription>(nameTableInfo, batchNames);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertCharacterAttributesFromJSON");
            }

            return count;
        }
        #endregion

        #region "ContrabandTypes
        private bool ConvertContrabandTypes()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = SDEPath + "\\contrabandTypes.jsonl";


                ContrabandType tableInfoContrabandType = new ContrabandType();
                TableInfo contrabandTypeTable = DatabaseManager.GetTableInfo<ContrabandType>(tableInfoContrabandType);
                DatabaseManager.CreateTable(contrabandTypeTable);

                Console.WriteLine("Converting Contraband Types");

                count = ConvertContrabandTypesFromJSON(path,
                                                        contrabandTypeTable);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Contraband Types Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Contraband Types ");
            return success;
        }

        private int ConvertContrabandTypesFromJSON(string path,
                                                        TableInfo contrabandTypeTable)
        {
            int count = 0;
            ContrabandType newContrabandType = null;
            List<ContrabandTypeFaction> batchContrabandTypeFactions = new List<ContrabandTypeFaction>();
            string json = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        json = sr.ReadLine();
                        newContrabandType = Newtonsoft.Json.JsonConvert.DeserializeObject<ContrabandType>(json);

                        if (newContrabandType.factions?.Count > 0)
                        {
                            foreach (ContrabandTypeFaction childType in newContrabandType.factions)
                            {
                                childType.contrabandTypeID = newContrabandType.contrabandTypeID;
                                Utility.AddRecordToBatch<ContrabandTypeFaction>(contrabandTypeTable, ref batchContrabandTypeFactions, childType);
                                count++;
                            }
                        }
                    }
                }
                Utility.InsertBatchRecord<ContrabandTypeFaction>(contrabandTypeTable, batchContrabandTypeFactions);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertContrabandTypesFromJSON");
            }

            return count;
        }
        #endregion

        #region "Control Tower Resources"
        private bool ConvertControlTowerResources()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = SDEPath + "\\controlTowerResources.jsonl";


                ControlTowerResource tableInfoControlTowerResource = new ControlTowerResource();
                TableInfo controlTowerResourceTable = DatabaseManager.GetTableInfo<ControlTowerResource>(tableInfoControlTowerResource);
                DatabaseManager.CreateTable(controlTowerResourceTable);
                ;
                Console.WriteLine("Converting Control Tower Resources");

                count = ConvertControlTowerResourcesFromJSON(path,
                                                                controlTowerResourceTable);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Control Tower Resources Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Control Tower Resources ");
            return success;
        }

        private int ConvertControlTowerResourcesFromJSON(string path,
                                                        TableInfo controlTowerResourceTable)
        {
            int count = 0;
            ControlTowerResource newControlTowerResource = null;
            List<ControlTowerResource> batchControlTowerResource = new List<ControlTowerResource>();
            string json = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        json = sr.ReadLine();
                        newControlTowerResource = Newtonsoft.Json.JsonConvert.DeserializeObject<ControlTowerResource>(json);

                        if (newControlTowerResource.resources?.Count > 0)
                        {
                            foreach (ControlTowerResource childType in newControlTowerResource.resources)
                            {
                                childType.controlTowerResourceID = newControlTowerResource.controlTowerResourceID;
                                Utility.AddRecordToBatch<ControlTowerResource>(controlTowerResourceTable, ref batchControlTowerResource, childType);
                                count++;
                            }
                        }
                    }
                }
                Utility.InsertBatchRecord<ControlTowerResource>(controlTowerResourceTable, batchControlTowerResource);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertControlTowerResourcesFromJSON");
            }

            return count;
        }
        #endregion

        #region "Corporation Activities"
        private bool ConvertCorporationActivities()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = SDEPath + "\\corporationActivities.jsonl";


                LanguageDescription tableInfoLanguageDescript = new LanguageDescription();
                TableInfo langDescripTable = DatabaseManager.GetTableInfo<LanguageDescription>(tableInfoLanguageDescript);
                langDescripTable.Name = "CorporationActivity";
                DatabaseManager.CreateTable(langDescripTable);

                Console.WriteLine("Converting Corporation Activities");

                count = ConvertCorporationActivitiesFromJSON(path,
                                                                langDescripTable);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Corporation Activities Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Corporation Activities ");
            return success;
        }

        private int ConvertCorporationActivitiesFromJSON(string path,
                                                        TableInfo languageDescriptTable)
        {
            int count = 0;
            CorporationActivity newCorporationActivity = null;
            List<LanguageDescription> batchCorporationActivity = new List<LanguageDescription>();
            string json = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        json = sr.ReadLine();
                        newCorporationActivity = Newtonsoft.Json.JsonConvert.DeserializeObject<CorporationActivity>(json);

                        if (newCorporationActivity.nameID != null)
                        {
                            newCorporationActivity.nameID.parentTypeId = newCorporationActivity.corporationActivityID;
                            Utility.AddRecordToBatch<LanguageDescription>(languageDescriptTable, ref batchCorporationActivity, newCorporationActivity.nameID);
                            count++;
                        }
                    }
                }

                Utility.InsertBatchRecord<LanguageDescription>(languageDescriptTable, batchCorporationActivity);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertCorporationActivitiesFromJSON");
            }

            return count;
        }
        #endregion

        #region "Dbuff Collections"
        private bool ConvertDBuffCollection()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                //Create all the BP Tables. 
                dbuffCollection tableInfoDBuffCollection = new dbuffCollection();
                TableInfo dBuffTable = DatabaseManager.GetTableInfo<dbuffCollection>(tableInfoDBuffCollection);
                DatabaseManager.CreateTable(dBuffTable);

                LanguageDescription tableInfoDisplayName = new LanguageDescription();
                TableInfo displayNameTable = DatabaseManager.GetTableInfo<LanguageDescription>(tableInfoDisplayName);
                displayNameTable.Name = "DBuffCollectionDisplayName";
                DatabaseManager.CreateTable(displayNameTable);

                DBuffCollectionDogmaAttribute tableInfoDogmaAtt = new DBuffCollectionDogmaAttribute();
                TableInfo dogmaAttTable = DatabaseManager.GetTableInfo<DBuffCollectionDogmaAttribute>(tableInfoDogmaAtt);
                DatabaseManager.CreateTable(dogmaAttTable);

                Console.WriteLine("Converting DBuff Collections");

                count = ConvertDBuffCollectionFromJSON(dBuffTable,
                                                    displayNameTable,
                                                    dogmaAttTable);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting DBuff Collections Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Dbuff Collections ");
            return success;
        }
        private int ConvertDBuffCollectionFromJSON(TableInfo dBuffTable,
                                              TableInfo displayNameTable,
                                              TableInfo dogmaAttTable)
        {
            int count = 0;
            dbuffCollection newDBuffCollection = null;
            string path = SDEPath + "\\dbuffCollections.jsonl";
            string json = "";
            Blueprints newObject = null;
            List<dbuffCollection> batchDBuffCollection = new List<dbuffCollection>();
            List<LanguageDescription> batchDisplayNames = new List<LanguageDescription>();
            List<DBuffCollectionDogmaAttribute> batchDogmaAttributes = new List<DBuffCollectionDogmaAttribute>();

            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        json = sr.ReadLine();
                        newDBuffCollection = Newtonsoft.Json.JsonConvert.DeserializeObject<dbuffCollection>(json);
                        Utility.AddRecordToBatch<dbuffCollection>(dBuffTable, ref batchDBuffCollection, newDBuffCollection);

                        if (newDBuffCollection.displayName != null)
                        {
                            newDBuffCollection.displayName.parentTypeId = newDBuffCollection.dbuffCollectionId;
                            Utility.AddRecordToBatch<LanguageDescription>(displayNameTable, ref batchDisplayNames, newDBuffCollection.displayName);
                        }

                        if (newDBuffCollection.itemModifiers != null)
                        {
                            foreach (DBuffCollectionDogmaAttribute attribute in newDBuffCollection.itemModifiers)
                            {
                                attribute.dbuffCollectionId = newDBuffCollection.dbuffCollectionId;
                                Utility.AddRecordToBatch<DBuffCollectionDogmaAttribute>(dogmaAttTable, ref batchDogmaAttributes, attribute);
                            }
                        }

                        count++;
                    }
                }
                Utility.InsertBatchRecord<dbuffCollection>(dBuffTable, batchDBuffCollection);
                Utility.InsertBatchRecord<LanguageDescription>(displayNameTable, batchDisplayNames);
                Utility.InsertBatchRecord<DBuffCollectionDogmaAttribute>(dogmaAttTable, batchDogmaAttributes);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertDBuffCollectionFromJSON");
            }

            return count;
        }
        #endregion

        #region "Dogma Attribute Categories"
        private bool ConvertDogmaAttributeCategories()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string fileName = "\\dogmaAttributeCategories.jsonl";

                DogmaAttributeCategory tableInfoDogmaAttrCat = new DogmaAttributeCategory();
                TableInfo dogmaAttrCatTable = DatabaseManager.GetTableInfo<DogmaAttributeCategory>(tableInfoDogmaAttrCat);
                DatabaseManager.CreateTable(dogmaAttrCatTable);

                Console.WriteLine("Converting Dogma Attribute Categories");

                count = ConvertFileForType<DogmaAttributeCategory>(dogmaAttrCatTable, fileName);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Dogma Attribute Categories Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Dogma Attribute Categories ");
            return success;
        }
        #endregion

        #region "Dogma Attributes"
        private bool ConvertDogmaAttributes()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = SDEPath + "\\dogmaAttributes.jsonl";


                DogmaAttribute tableInfoDogmaAttr = new DogmaAttribute();
                TableInfo dogmaAttrTable = DatabaseManager.GetTableInfo<DogmaAttribute>(tableInfoDogmaAttr);
                DatabaseManager.CreateTable(dogmaAttrTable);

                LanguageDescription tableInfoLangDscr = new LanguageDescription();
                TableInfo languageDescipriotnTableInfo = DatabaseManager.GetTableInfo(tableInfoLangDscr);
                languageDescipriotnTableInfo.Name = "DogmaAttributeDisplayName";
                DatabaseManager.CreateTable(languageDescipriotnTableInfo);

                Console.WriteLine("Converting Dogma Attributes");

                count = ConvertDogmaAttributesFromJSON(path,
                                                           dogmaAttrTable,
                                                           languageDescipriotnTableInfo);

            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Dogma Attributes Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Dogma Attributes ");
            return success;
        }

        private int ConvertDogmaAttributesFromJSON(string path,
                                                        TableInfo dogmaAttrTable,
                                                        TableInfo displayNameTable)
        {
            int count = 0;
            DogmaAttribute newDogmaAttribute = null;
            List<DogmaAttribute> batchDogmaAttributes = new List<DogmaAttribute>();
            List<LanguageDescription> batchDisplayNames = new List<LanguageDescription>();
            string json = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        json = sr.ReadLine();
                        newDogmaAttribute = Newtonsoft.Json.JsonConvert.DeserializeObject<DogmaAttribute>(json);
                        Utility.AddRecordToBatch<DogmaAttribute>(dogmaAttrTable, ref batchDogmaAttributes, newDogmaAttribute);
                        count++;

                        if (newDogmaAttribute.displayName != null)
                        {
                            newDogmaAttribute.displayName.parentTypeId = newDogmaAttribute.attributeID;
                            Utility.AddRecordToBatch<LanguageDescription>(displayNameTable, ref batchDisplayNames, newDogmaAttribute.displayName);
                        }
                    }
                }

                Utility.InsertBatchRecord<DogmaAttribute>(dogmaAttrTable, batchDogmaAttributes);
                Utility.InsertBatchRecord<LanguageDescription>(displayNameTable, batchDisplayNames);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertDogmaAttributesFromJSON");
            }

            return count;
        }
        #endregion

        #region "Dogma Effects"
        private bool ConvertDogmaEffects()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = SDEPath + "\\dogmaEffects.jsonl";

                DogmaEffect tableInfoDogmaEffect = new DogmaEffect();
                TableInfo dogmaEffectTable = DatabaseManager.GetTableInfo<DogmaEffect>(tableInfoDogmaEffect);
                DatabaseManager.CreateTable(dogmaEffectTable);

                LanguageDescription tableInfoLangDscr = new LanguageDescription();
                TableInfo descriptionTableInfo = DatabaseManager.GetTableInfo(tableInfoLangDscr);
                descriptionTableInfo.Name = "DogmaEffectDescription";
                DatabaseManager.CreateTable(descriptionTableInfo);

                TableInfo displayNameTableInfo = DatabaseManager.GetTableInfo(tableInfoLangDscr);
                displayNameTableInfo.Name = "DogmaEffectDisplayName";
                DatabaseManager.CreateTable(displayNameTableInfo);

                Console.WriteLine("Converting Dogma Effects");

                count = ConvertDogmaEffectsFromJSON(path,
                                                        dogmaEffectTable,
                                                        descriptionTableInfo,
                                                        displayNameTableInfo);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Dogma Effects Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Dogma Effects ");
            return success;
        }

        private int ConvertDogmaEffectsFromJSON(string path,
                                                        TableInfo dogmaEffectTable,
                                                        TableInfo descriptionTableInfo,
                                                        TableInfo displayNameTableInfo)
        {
            int count = 0;
            DogmaEffect newDogmaEffect = null;
            List<DogmaEffect> batchDogmaEffects = new List<DogmaEffect>();
            List<LanguageDescription> batchDescriptions = new List<LanguageDescription>();
            List<LanguageDescription> batchDisplayNames = new List<LanguageDescription>();
            string json = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        json = sr.ReadLine();
                        newDogmaEffect = Newtonsoft.Json.JsonConvert.DeserializeObject<DogmaEffect>(json);
                        Utility.AddRecordToBatch<DogmaEffect>(dogmaEffectTable, ref batchDogmaEffects, newDogmaEffect);
                        count++;

                        if (newDogmaEffect.displayName != null)
                        {
                            newDogmaEffect.displayName.parentTypeId = newDogmaEffect.dogmaEffectID;
                            Utility.AddRecordToBatch<LanguageDescription>(displayNameTableInfo, ref batchDisplayNames, newDogmaEffect.displayName);
                        }

                        if (newDogmaEffect.description != null)
                        {
                            newDogmaEffect.description.parentTypeId = newDogmaEffect.dogmaEffectID;
                            Utility.AddRecordToBatch<LanguageDescription>(descriptionTableInfo, ref batchDescriptions, newDogmaEffect.description);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertDogmaEffectsFromJSON");
            }

            return count;
        }
        #endregion

        #region "Dogma Units"
        private bool ConvertDogmaUnits()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = SDEPath + "\\dogmaUnits.jsonl";

                DogmaUnit tableInfoDogmaUnit = new DogmaUnit();
                TableInfo dogmaUnitTableInfo = DatabaseManager.GetTableInfo<DogmaUnit>(tableInfoDogmaUnit);
                DatabaseManager.CreateTable(dogmaUnitTableInfo);

                LanguageDescription tableInfoLangDscr = new LanguageDescription();
                TableInfo descriptionTableInfo = DatabaseManager.GetTableInfo(tableInfoLangDscr);
                descriptionTableInfo.Name = "DogmaUnitDescription";
                DatabaseManager.CreateTable(descriptionTableInfo);

                TableInfo displayNameTableInfo = DatabaseManager.GetTableInfo(tableInfoLangDscr);
                displayNameTableInfo.Name = "DogmaUnitDisplayName";
                DatabaseManager.CreateTable(displayNameTableInfo);

                Console.WriteLine("Converting Dogma Effects");

                count = ConvertDogmaUnitsFromJSON(path,
                                                        dogmaUnitTableInfo,
                                                        descriptionTableInfo,
                                                        displayNameTableInfo);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Dogma Units Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Dogma Units ");
            return success;
        }

        private int ConvertDogmaUnitsFromJSON(string path,
                                                        TableInfo dogmaEffectTable,
                                                        TableInfo descriptionTableInfo,
                                                        TableInfo displayNameTableInfo)
        {
            int count = 0;
            DogmaUnit newDogmaUnit = null;
            List<DogmaUnit> batchDogmaUnits = new List<DogmaUnit>();
            List<LanguageDescription> batchDescriptions = new List<LanguageDescription>();
            List<LanguageDescription> batchDisplayNames = new List<LanguageDescription>();
            string json = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        json = sr.ReadLine();
                        newDogmaUnit = Newtonsoft.Json.JsonConvert.DeserializeObject<DogmaUnit>(json);
                        Utility.AddRecordToBatch<DogmaUnit>(dogmaEffectTable, ref batchDogmaUnits, newDogmaUnit);
                        count++;

                        if (newDogmaUnit.displayName != null)
                        {
                            newDogmaUnit.displayName.parentTypeId = newDogmaUnit.dogmaUnitId;
                            Utility.AddRecordToBatch<LanguageDescription>(displayNameTableInfo, ref batchDisplayNames, newDogmaUnit.displayName);
                        }

                        if (newDogmaUnit.description != null)
                        {
                            newDogmaUnit.description.parentTypeId = newDogmaUnit.dogmaUnitId;
                            Utility.AddRecordToBatch<LanguageDescription>(descriptionTableInfo, ref batchDescriptions, newDogmaUnit.description);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertDogmaUnitsFromJSON");
            }

            return count;
        }
        #endregion

        #region "Factions"
        private bool ConvertFactions()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = SDEPath + "\\factions.jsonl";


                Faction tableInfoFaction = new Faction();
                TableInfo factionTable = DatabaseManager.GetTableInfo<Faction>(tableInfoFaction);
                DatabaseManager.CreateTable(factionTable);

                FactionRaces tableInfoFactionRaces = new FactionRaces();
                TableInfo factionRacesTable = DatabaseManager.GetTableInfo<FactionRaces>(tableInfoFactionRaces);
                DatabaseManager.CreateTable(factionRacesTable);

                LanguageDescription tableInfoLangDscr = new LanguageDescription();
                TableInfo languageDescipriotnTableInfo = DatabaseManager.GetTableInfo(tableInfoLangDscr);

                TableInfo descriptionTableInfo = DatabaseManager.GetTableInfo(tableInfoLangDscr);
                descriptionTableInfo.Name = "FactionDescription";
                DatabaseManager.CreateTable(descriptionTableInfo);

                TableInfo nameTable = DatabaseManager.GetTableInfo(tableInfoLangDscr);
                nameTable.Name = "FactionName";
                DatabaseManager.CreateTable(nameTable);

                TableInfo shortDescriptionTable = DatabaseManager.GetTableInfo(tableInfoLangDscr);
                shortDescriptionTable.Name = "FactionShortDescription";
                DatabaseManager.CreateTable(shortDescriptionTable);

                Console.WriteLine("Converting Factions");

                count = ConvertFactionsFromJSON(path,
                                                    factionTable,
                                                    factionRacesTable,
                                                    descriptionTableInfo,
                                                    nameTable,
                                                    shortDescriptionTable);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Factions Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Factions ");
            return success;
        }

        private int ConvertFactionsFromJSON(string path,
                                            TableInfo factionTable,
                                            TableInfo factionRacesTable,
                                            TableInfo factionDscrTableName,
                                            TableInfo factionNameTableName,
                                            TableInfo factionShortDscrTableName)
        {
            int count = 0;
            Faction newFaction = null;
            FactionRaces newFactionRaces = null;

            List<Faction> batchFactions = new List<Faction>();
            List<FactionRaces> batchFactionRaces = new List<FactionRaces>();
            List<LanguageDescription> batchDescription = new List<LanguageDescription>();
            List<LanguageDescription> batchNames = new List<LanguageDescription>();
            List<LanguageDescription> batchShortDescriptions = new List<LanguageDescription>();
            string json = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        json = sr.ReadLine();
                        newFaction = Newtonsoft.Json.JsonConvert.DeserializeObject<Faction>(json);
                        Utility.AddRecordToBatch<Faction>(factionTable, ref batchFactions, newFaction);

                        if (newFaction.memberRaces?.Count > 0)
                        {
                            foreach (int memberRace in newFaction.memberRaces)
                            {
                                newFactionRaces = new FactionRaces();
                                newFactionRaces.factionID = newFaction.factionID;
                                newFactionRaces.raceID = memberRace;
                                Utility.AddRecordToBatch<FactionRaces>(factionRacesTable, ref batchFactionRaces, newFactionRaces);
                            }
                        }

                        if (newFaction.description != null)
                        {
                            newFaction.description.parentTypeId = newFaction.factionID;
                            Utility.AddRecordToBatch<LanguageDescription>(factionDscrTableName, ref batchDescription, newFaction.description);
                        }

                        if (newFaction.name != null)
                        {
                            newFaction.name.parentTypeId = newFaction.factionID;
                            Utility.AddRecordToBatch<LanguageDescription>(factionNameTableName, ref batchNames, newFaction.name);
                        }

                        if (newFaction.shortDescription != null)
                        {
                            newFaction.shortDescription.parentTypeId = newFaction.factionID;
                            Utility.AddRecordToBatch<LanguageDescription>(factionShortDscrTableName, ref batchShortDescriptions, newFaction.shortDescription);
                        }

                        count++;
                    }
                }

                Utility.InsertBatchRecord<Faction>(factionTable, batchFactions);
                Utility.InsertBatchRecord<FactionRaces>(factionRacesTable, batchFactionRaces);
                Utility.InsertBatchRecord<LanguageDescription>(factionDscrTableName, batchDescription);
                Utility.InsertBatchRecord<LanguageDescription>(factionNameTableName, batchNames);
                Utility.InsertBatchRecord<LanguageDescription>(factionShortDscrTableName, batchShortDescriptions);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertFactionsFromJSON");
            }

            return count;
        }
        #endregion

        #region "Graphics"
        private bool ConvertGraphics()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = "\\graphics.jsonl";


                Graphic tableInfoFaction = new Graphic();
                TableInfo graphicTable = DatabaseManager.GetTableInfo<Graphic>(tableInfoFaction);
                DatabaseManager.CreateTable(graphicTable);

                Console.WriteLine("Converting Graphics");

                count = ConvertFileForType<Graphic>(graphicTable, path);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Graphics Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Graphics ");
            return success;
        }
        #endregion

        #region "Groups"
        private bool ConvertGroups()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = SDEPath + "\\groups.jsonl";


                EveGroup tableInfoGroup = new EveGroup();
                TableInfo groupTable = DatabaseManager.GetTableInfo<EveGroup>(tableInfoGroup);
                DatabaseManager.CreateTable(groupTable);

                LanguageDescription tableInfoLangDscr = new LanguageDescription();
                TableInfo languageDescipriotnTableInfo = DatabaseManager.GetTableInfo(tableInfoLangDscr);
                languageDescipriotnTableInfo.Name = "GroupName";
                DatabaseManager.CreateTable(languageDescipriotnTableInfo);

                Console.WriteLine("Converting Groups");

                count = ConvertGroupsFromJSON(path,
                                                groupTable,
                                                languageDescipriotnTableInfo);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Groups Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Groups ");
            return success;
        }

        private int ConvertGroupsFromJSON(string path,
                                            TableInfo groupTable,
                                            TableInfo langDscrTable)
        {
            int count = 0;
            EveGroup newGroup = null;
            List<EveGroup> batchGroups = new List<EveGroup>();
            List<LanguageDescription> batchNames = new List<LanguageDescription>();
            string json = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        json = sr.ReadLine();
                        newGroup = Newtonsoft.Json.JsonConvert.DeserializeObject<EveGroup>(json);
                        Utility.AddRecordToBatch<EveGroup>(groupTable, ref batchGroups, newGroup);

                        if (newGroup.name != null)
                        {
                            newGroup.name.parentTypeId = newGroup.groupID;
                            Utility.AddRecordToBatch<LanguageDescription>(langDscrTable, ref batchNames, newGroup.name);
                        }

                        count++;
                    }
                }
                Utility.InsertBatchRecord<EveGroup>(groupTable, batchGroups);
                Utility.InsertBatchRecord<LanguageDescription>(langDscrTable, batchNames);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertGroupsFromJSON");
            }

            return count;
        }
        #endregion

        #region "Icons"
        private bool ConvertIcons()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = "\\icons.jsonl";


                Icon tableInfoIcon = new Icon();
                TableInfo iconTable = DatabaseManager.GetTableInfo<Icon>(tableInfoIcon);
                DatabaseManager.CreateTable(iconTable);

                Console.WriteLine("Converting Icons");

                count = ConvertFileForType<Icon>(iconTable, path);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Icons Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Icons ");
            return success;
        }
        #endregion

        #region "Landmarks"
        private bool ConvertLandmarks()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = SDEPath + "\\landmarks.jsonl";


                Landmark tableInfoLandmark = new Landmark();
                TableInfo landmarkTable = DatabaseManager.GetTableInfo<Landmark>(tableInfoLandmark);
                DatabaseManager.CreateTable(landmarkTable);

                LanguageDescription tableInfoLangDscr = new LanguageDescription();
                TableInfo languageDescipriotnTableInfo = DatabaseManager.GetTableInfo(tableInfoLangDscr);
                languageDescipriotnTableInfo.Name = "LandmarkDescription";
                DatabaseManager.CreateTable(languageDescipriotnTableInfo);

                LanguageDescription tableInfoLangName = new LanguageDescription();
                TableInfo nameTableInfo = DatabaseManager.GetTableInfo(tableInfoLangName);
                nameTableInfo.Name = "LandmarkName";
                DatabaseManager.CreateTable(nameTableInfo);

                Console.WriteLine("Converting Landmarks");

                count = ConvertLandmarksFromJSON(path,
                                                landmarkTable,
                                                languageDescipriotnTableInfo,
                                                nameTableInfo);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Landmarks Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Landmarks ");
            return success;
        }

        private int ConvertLandmarksFromJSON(string path,
                                            TableInfo landmarkTable,
                                            TableInfo descriptionTable,
                                            TableInfo nameTable)
        {
            int count = 0;
            Landmark newLandMark = null;
            List<Landmark> batchLandmarks = new List<Landmark>();
            List<LanguageDescription> batchDescriptions = new List<LanguageDescription>();
            List<LanguageDescription> batchNames = new List<LanguageDescription>();
            string json = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        json = sr.ReadLine();
                        newLandMark = Newtonsoft.Json.JsonConvert.DeserializeObject<Landmark>(json);

                        if (newLandMark.position != null)
                        {
                            newLandMark.x = newLandMark.position.x;
                            newLandMark.y = newLandMark.position.y;
                            newLandMark.z = newLandMark.position.z;
                        }

                        Utility.AddRecordToBatch<Landmark>(landmarkTable, ref batchLandmarks, newLandMark);

                        if (newLandMark.description != null)
                        {
                            newLandMark.description.parentTypeId = newLandMark.landmarkID;
                            Utility.AddRecordToBatch<LanguageDescription>(descriptionTable, ref batchDescriptions, newLandMark.description);
                        }

                        if (newLandMark.name != null)
                        {
                            newLandMark.name.parentTypeId = newLandMark.landmarkID;
                            Utility.AddRecordToBatch<LanguageDescription>(nameTable, ref batchNames, newLandMark.name);
                        }

                        count++;
                    }
                }
                Utility.InsertBatchRecord<Landmark>(landmarkTable, batchLandmarks);
                Utility.InsertBatchRecord<LanguageDescription>(descriptionTable, batchDescriptions);
                Utility.InsertBatchRecord<LanguageDescription>(nameTable, batchNames);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertLandmarksFromJSON");
            }

            return count;
        }
        #endregion

        #region "Map Asteroid Belts"
        private bool ConvertMapAsteroidBelts()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = SDEPath + "\\mapAsteroidBelts.jsonl";


                mapAsteroidBelt tableInfoPlanetAsteroidBelt = new mapAsteroidBelt();
                TableInfo asteroidBeltTableInfo = DatabaseManager.GetTableInfo<mapAsteroidBelt>(tableInfoPlanetAsteroidBelt);
                DatabaseManager.CreateTable(asteroidBeltTableInfo);

                AsteroidBetlStatistics tableInfoBeltStats = new AsteroidBetlStatistics();
                TableInfo beltStatsTableInfo = DatabaseManager.GetTableInfo(tableInfoBeltStats);
                DatabaseManager.CreateTable(beltStatsTableInfo);

                Console.WriteLine("Converting Asteroid Belts");

                count = ConvertMapAsteroidBeltsFromJSON(path,
                                                asteroidBeltTableInfo,
                                                beltStatsTableInfo);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Landmarks Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " asteroid belts ");
            return success;
        }


        private int ConvertMapAsteroidBeltsFromJSON(string path,
                                            TableInfo asteroidBeltTable,
                                            TableInfo beltStatsTable)
        {
            int count = 0;
            mapAsteroidBelt newAsteroidBelt = null;
            List<mapAsteroidBelt> batchAsteroidBelts = new List<mapAsteroidBelt>();
            List<AsteroidBetlStatistics> batchStats = new List<AsteroidBetlStatistics>();
            string json = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        json = sr.ReadLine();
                        newAsteroidBelt = Newtonsoft.Json.JsonConvert.DeserializeObject<mapAsteroidBelt>(json);

                        if (newAsteroidBelt.position != null)
                        {
                            newAsteroidBelt.positionX = newAsteroidBelt.position.x;
                            newAsteroidBelt.positionY = newAsteroidBelt.position.y;
                            newAsteroidBelt.positionZ = newAsteroidBelt.position.z;
                        }


                        Utility.AddRecordToBatch<mapAsteroidBelt>(asteroidBeltTable, ref batchAsteroidBelts, newAsteroidBelt);

                        if (newAsteroidBelt.statistics != null)
                        {
                            newAsteroidBelt.statistics.asteroidBeltID = newAsteroidBelt.asteroidBeltID;
                            Utility.AddRecordToBatch<AsteroidBetlStatistics>(beltStatsTable, ref batchStats, newAsteroidBelt.statistics);
                        }

                        count++;
                    }
                }
                Utility.InsertBatchRecord<mapAsteroidBelt>(asteroidBeltTable, batchAsteroidBelts);
                Utility.InsertBatchRecord<AsteroidBetlStatistics>(beltStatsTable, batchStats);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertMapAsteroidBeltsFromJSON");
            }

            return count;
        }
        #endregion

        #region "Map Constellations"
        private bool ConvertMapConstellations()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = SDEPath + "\\mapConstellations.jsonl";


                mapConstellation tableInfomapConstellations = new mapConstellation();
                TableInfo mapConstellationsTableInfo = DatabaseManager.GetTableInfo<mapConstellation>(tableInfomapConstellations);
                DatabaseManager.CreateTable(mapConstellationsTableInfo);

                LanguageDescription tableInfoConstellationName = new LanguageDescription();
                TableInfo constellationNameTableInfo = DatabaseManager.GetTableInfo(tableInfoConstellationName);
                constellationNameTableInfo.Name = "ConstellationName";
                DatabaseManager.CreateTable(constellationNameTableInfo);

                ConstellationSolarSystem tableInfoConstellationSolarSystem = new ConstellationSolarSystem();
                TableInfo constellationSolarSystemTableInfo = DatabaseManager.GetTableInfo(tableInfoConstellationSolarSystem);
                DatabaseManager.CreateTable(constellationSolarSystemTableInfo);

                Console.WriteLine("Converting Constellations");

                count = ConvertMapConstellationsFromJSON(path,
                                                mapConstellationsTableInfo,
                                                constellationNameTableInfo,
                                                constellationSolarSystemTableInfo);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Constellations Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " constellations ");
            return success;
        }


        private int ConvertMapConstellationsFromJSON(string path,
                                            TableInfo constellationTable,
                                            TableInfo constellationNameTable,
                                            TableInfo constellationSolarSystemTableInfo)
        {
            int count = 0;
            mapConstellation newConstellation = null;
            ConstellationSolarSystem newConstellationSolarSystem = null;
            List<mapConstellation> batchConstellations = new List<mapConstellation>();
            List<LanguageDescription> batchNames = new List<LanguageDescription>();
            List<ConstellationSolarSystem> batchConstellationSystems = new List<ConstellationSolarSystem>();
            string json = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        json = sr.ReadLine();
                        newConstellation = Newtonsoft.Json.JsonConvert.DeserializeObject<mapConstellation>(json);

                        if (newConstellation.position != null)
                        {
                            newConstellation.x = newConstellation.position.x;
                            newConstellation.y = newConstellation.position.y;
                            newConstellation.z = newConstellation.position.z;
                        }

                        Utility.AddRecordToBatch<mapConstellation>(constellationTable, ref batchConstellations, newConstellation);

                        if (newConstellation.name != null)
                        {
                            newConstellation.name.parentTypeId = newConstellation.constellationID;
                            Utility.AddRecordToBatch<LanguageDescription>(constellationNameTable, ref batchNames, newConstellation.name);
                        }

                        if (newConstellation.solarSystemIDs != null && newConstellation.solarSystemIDs.Count > 0)
                        {
                            foreach (long solarSystemId in newConstellation.solarSystemIDs)
                            {
                                newConstellationSolarSystem = new ConstellationSolarSystem();
                                newConstellationSolarSystem.constellationID = newConstellation.constellationID;
                                newConstellationSolarSystem.solarSystemID = solarSystemId;
                                Utility.AddRecordToBatch<ConstellationSolarSystem>(constellationSolarSystemTableInfo, ref batchConstellationSystems, newConstellationSolarSystem);
                            }
                        }

                        count++;
                    }
                }
                Utility.InsertBatchRecord<mapConstellation>(constellationTable, batchConstellations);
                Utility.InsertBatchRecord<LanguageDescription>(constellationNameTable, batchNames);
                Utility.InsertBatchRecord<ConstellationSolarSystem>(constellationSolarSystemTableInfo, batchConstellationSystems);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertMapConstellationsFromJSON");
            }

            return count;
        }
        #endregion

        #region "Map Moons"
        private bool ConvertMapMoons()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = SDEPath + "\\mapMoons.jsonl";


                mapMoon tableInfoMapMoon = new mapMoon();
                TableInfo mapMoonTableInfo = DatabaseManager.GetTableInfo<mapMoon>(tableInfoMapMoon);
                DatabaseManager.CreateTable(mapMoonTableInfo);

                MoonAttributes tableInfoMoonAttributes = new MoonAttributes();
                TableInfo moonAttributesTableInfo = DatabaseManager.GetTableInfo(tableInfoMoonAttributes);
                DatabaseManager.CreateTable(moonAttributesTableInfo);

                MoonStatistics tableInfoMoonStatistics = new MoonStatistics();
                TableInfo moonStatsTableInfo = DatabaseManager.GetTableInfo(tableInfoMoonStatistics);
                DatabaseManager.CreateTable(moonStatsTableInfo);

                Console.WriteLine("Converting Moons");

                count = ConvertMapMoonsFromJSON(path,
                                                mapMoonTableInfo,
                                                moonAttributesTableInfo,
                                                moonStatsTableInfo);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Moons Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Moons ");
            return success;
        }


        private int ConvertMapMoonsFromJSON(string path,
                                            TableInfo mapMoonTableInfo,
                                            TableInfo moonAttributesTableInfo,
                                            TableInfo moonStatsTableInfo)
        {
            int count = 0;
            mapMoon newMapMoon = null;
            List<mapMoon> batchMoons = new List<mapMoon>();
            List<MoonAttributes> batchMoonAttributes = new List<MoonAttributes>();
            List<MoonStatistics> batchMoonStats = new List<MoonStatistics>();
            string json = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        json = sr.ReadLine();
                        newMapMoon = Newtonsoft.Json.JsonConvert.DeserializeObject<mapMoon>(json);

                        if (newMapMoon.position != null)
                        {
                            newMapMoon.x = newMapMoon.position.x;
                            newMapMoon.y = newMapMoon.position.y;
                            newMapMoon.z = newMapMoon.position.z;
                        }

                        Utility.AddRecordToBatch<mapMoon>(mapMoonTableInfo, ref batchMoons, newMapMoon);

                        if (newMapMoon.attributes != null)
                        {
                            newMapMoon.attributes.moonID = newMapMoon.moonID;
                            Utility.AddRecordToBatch<MoonAttributes>(moonAttributesTableInfo, ref batchMoonAttributes, newMapMoon.attributes);
                        }

                        if (newMapMoon.statistics != null)
                        {
                            newMapMoon.statistics.moonID = newMapMoon.moonID;
                            Utility.AddRecordToBatch<MoonStatistics>(moonStatsTableInfo, ref batchMoonStats, newMapMoon.statistics);
                        }

                        count++;
                    }
                }
                Utility.InsertBatchRecord<mapMoon>(mapMoonTableInfo, batchMoons);
                Utility.InsertBatchRecord<MoonAttributes>(moonAttributesTableInfo, batchMoonAttributes);
                Utility.InsertBatchRecord<MoonStatistics>(moonStatsTableInfo, batchMoonStats);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertMapMoonsFromJSON");
            }

            return count;
        }
        #endregion

        #region "Map Planets"
        private bool ConvertMapPlanets()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = SDEPath + "\\mapPlanets.jsonl";


                mapPlanet tableInfoMapPlanet = new mapPlanet();
                TableInfo mapPlanetTableInfo = DatabaseManager.GetTableInfo<mapPlanet>(tableInfoMapPlanet);
                DatabaseManager.CreateTable(mapPlanetTableInfo);

                PlanetAsteroidBelt tableInfoPlanetAsteroidBelt = new PlanetAsteroidBelt();
                TableInfo planetAsteroidBeltTableInfo = DatabaseManager.GetTableInfo(tableInfoPlanetAsteroidBelt);
                DatabaseManager.CreateTable(planetAsteroidBeltTableInfo);

                PlanetMoon tableInfoPlanetMoon = new PlanetMoon();
                TableInfo planetMoonTableInfo = DatabaseManager.GetTableInfo(tableInfoPlanetMoon);
                DatabaseManager.CreateTable(planetMoonTableInfo);

                PlanetAttributes tableInfoPlanetAttributes = new PlanetAttributes();
                TableInfo planetAttributesTableInfo = DatabaseManager.GetTableInfo(tableInfoPlanetAttributes);
                DatabaseManager.CreateTable(planetAttributesTableInfo);

                PlanetStatistics tableInfoPLanetStatistics = new PlanetStatistics();
                TableInfo planetStatisticsTableInfo = DatabaseManager.GetTableInfo(tableInfoPLanetStatistics);
                DatabaseManager.CreateTable(planetStatisticsTableInfo);

                Console.WriteLine("Converting Planets");

                count = ConvertMapPlanetsFromJSON(path,
                                                mapPlanetTableInfo,
                                                planetAsteroidBeltTableInfo,
                                                planetMoonTableInfo,
                                                planetAttributesTableInfo,
                                                planetStatisticsTableInfo);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Planets Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Planets ");
            return success;
        }


        private int ConvertMapPlanetsFromJSON(string path,
                                            TableInfo mapPlanetTableInfo,
                                            TableInfo planetAsteroidBeltTableInfo,
                                            TableInfo planetMoonTableInfo,
                                            TableInfo planetAttributesTableInfo,
                                            TableInfo planetStatisticsTableInfo)
        {
            int count = 0;
            mapPlanet newMapPlanet = null;
            PlanetAsteroidBelt newAsteroidBelt = null;
            PlanetMoon newMoon = null;
            List<mapPlanet> batchPlanet = new List<mapPlanet>();
            List<PlanetAttributes> batchPlanetAttributes = new List<PlanetAttributes>();
            List<PlanetStatistics> batchStats = new List<PlanetStatistics>();
            List<PlanetAsteroidBelt> batchAsteroidBelts = new List<PlanetAsteroidBelt>();
            List<PlanetMoon> batchMoons = new List<PlanetMoon>();
            string json = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        json = sr.ReadLine();
                        newMapPlanet = Newtonsoft.Json.JsonConvert.DeserializeObject<mapPlanet>(json);

                        if (newMapPlanet.position != null)
                        {
                            newMapPlanet.x = newMapPlanet.position.x;
                            newMapPlanet.y = newMapPlanet.position.y;
                            newMapPlanet.z = newMapPlanet.position.z;
                        }

                        Utility.AddRecordToBatch<mapPlanet>(mapPlanetTableInfo, ref batchPlanet, newMapPlanet);

                        if (newMapPlanet.attributes != null)
                        {
                            newMapPlanet.attributes.planetID = newMapPlanet.planetID;
                            Utility.AddRecordToBatch<PlanetAttributes>(planetAttributesTableInfo, ref batchPlanetAttributes, newMapPlanet.attributes);
                        }

                        if (newMapPlanet.statistics != null)
                        {
                            newMapPlanet.statistics.planetID = newMapPlanet.planetID;
                            Utility.AddRecordToBatch<PlanetStatistics>(planetStatisticsTableInfo, ref batchStats, newMapPlanet.statistics);
                        }

                        if (newMapPlanet.moonIDs != null)
                        {
                            foreach (long moonId in newMapPlanet.moonIDs)
                            {
                                newMoon = new PlanetMoon();
                                newMoon.planetID = newMapPlanet.planetID;
                                newMoon.moonID = moonId;
                                Utility.AddRecordToBatch<PlanetMoon>(planetMoonTableInfo, ref batchMoons, newMoon);
                            }
                        }

                        if (newMapPlanet.asteroidBeltIDs != null)
                        {
                            foreach (long asteroidBeltId in newMapPlanet.asteroidBeltIDs)
                            {
                                newAsteroidBelt = new PlanetAsteroidBelt();
                                newAsteroidBelt.planetID = newMapPlanet.planetID;
                                newAsteroidBelt.asteroidBeltId = asteroidBeltId;
                                Utility.AddRecordToBatch<PlanetAsteroidBelt>(planetAsteroidBeltTableInfo, ref batchAsteroidBelts, newAsteroidBelt);
                            }
                        }

                        count++;
                    }
                }
                Utility.InsertBatchRecord<mapPlanet>(mapPlanetTableInfo, batchPlanet);
                Utility.InsertBatchRecord<PlanetAsteroidBelt>(planetAsteroidBeltTableInfo, batchAsteroidBelts);
                Utility.InsertBatchRecord<PlanetMoon>(planetMoonTableInfo, batchMoons);
                Utility.InsertBatchRecord<PlanetAttributes>(planetAttributesTableInfo, batchPlanetAttributes);
                Utility.InsertBatchRecord<PlanetStatistics>(planetStatisticsTableInfo, batchStats);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertMapPlanetsFromJSON");
            }

            return count;
        }
        #endregion

        #region "Map Regions"
        private bool ConvertMapRegions()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = SDEPath + "\\mapRegions.jsonl";


                mapRegion tableInfoMapRegion = new mapRegion();
                TableInfo mapRegionTableInfo = DatabaseManager.GetTableInfo<mapRegion>(tableInfoMapRegion);
                DatabaseManager.CreateTable(mapRegionTableInfo);

                LanguageDescription tableInfoDescription = new LanguageDescription();
                TableInfo descriptionTableInfo = DatabaseManager.GetTableInfo(tableInfoDescription);
                descriptionTableInfo.Name = "RegionDescription";
                DatabaseManager.CreateTable(descriptionTableInfo);

                LanguageDescription tableInfoName = new LanguageDescription();
                TableInfo nameTableInfo = DatabaseManager.GetTableInfo(tableInfoName);
                nameTableInfo.Name = "RegionName";
                DatabaseManager.CreateTable(nameTableInfo);

                RegionConstellation tableInfoRegionConstellation = new RegionConstellation();
                TableInfo regionConstellationTableInfo = DatabaseManager.GetTableInfo(tableInfoRegionConstellation);
                DatabaseManager.CreateTable(regionConstellationTableInfo);

                Console.WriteLine("Converting Regions");

                count = ConvertMapRegionsFromJSON(path,
                                                mapRegionTableInfo,
                                                descriptionTableInfo,
                                                nameTableInfo,
                                                regionConstellationTableInfo);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Regions Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Regions ");
            return success;
        }


        private int ConvertMapRegionsFromJSON(string path,
                                            TableInfo mapRegionTableInfo,
                                            TableInfo descriptionTableInfo,
                                            TableInfo nameTableInfo,
                                            TableInfo regionConstellationTableInfo)
        {
            int count = 0;
            mapRegion newMapRegion = null;
            RegionConstellation newRegionConstellation = null;
            List<mapRegion> batchRegions = new List<mapRegion>();
            List<LanguageDescription> batchDescriptions = new List<LanguageDescription>();
            List<LanguageDescription> batchNames = new List<LanguageDescription>();
            List<RegionConstellation> batchConstellations = new List<RegionConstellation>();
            string json = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        json = sr.ReadLine();
                        newMapRegion = Newtonsoft.Json.JsonConvert.DeserializeObject<mapRegion>(json);

                        if (newMapRegion.position != null)
                        {
                            newMapRegion.x = newMapRegion.position.x;
                            newMapRegion.y = newMapRegion.position.y;
                            newMapRegion.z = newMapRegion.position.z;
                        }

                        Utility.AddRecordToBatch<mapRegion>(mapRegionTableInfo, ref batchRegions, newMapRegion);

                        if (newMapRegion.description != null)
                        {
                            newMapRegion.description.parentTypeId = newMapRegion.regionID;
                            Utility.AddRecordToBatch<LanguageDescription>(descriptionTableInfo, ref batchDescriptions, newMapRegion.description);
                        }

                        if (newMapRegion.name != null)
                        {
                            newMapRegion.name.parentTypeId = newMapRegion.regionID;
                            Utility.AddRecordToBatch<LanguageDescription>(nameTableInfo, ref batchNames, newMapRegion.name);
                        }

                        if (newMapRegion.constellationIDs != null)
                        {
                            foreach (long constellationID in newMapRegion.constellationIDs)
                            {
                                newRegionConstellation = new RegionConstellation();
                                newRegionConstellation.regionID = newMapRegion.regionID;
                                newRegionConstellation.constellationID = constellationID;
                                Utility.AddRecordToBatch<RegionConstellation>(regionConstellationTableInfo, ref batchConstellations, newRegionConstellation);
                            }
                        }

                        count++;
                    }
                }
                Utility.InsertBatchRecord<mapRegion>(mapRegionTableInfo, batchRegions);
                Utility.InsertBatchRecord<LanguageDescription>(descriptionTableInfo, batchDescriptions);
                Utility.InsertBatchRecord<LanguageDescription>(nameTableInfo, batchNames);
                Utility.InsertBatchRecord<RegionConstellation>(regionConstellationTableInfo, batchConstellations);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertMapRegionsFromJSON");
            }

            return count;
        }
        #endregion

        #region "Map Solar Systems"
        private bool ConvertMapSolarSystems()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = SDEPath + "\\mapSolarSystems.jsonl";


                mapSolarSystem tableInfoSolarSystem = new mapSolarSystem();
                TableInfo solarSystemTableInfo = DatabaseManager.GetTableInfo<mapSolarSystem>(tableInfoSolarSystem);
                DatabaseManager.CreateTable(solarSystemTableInfo);

                LanguageDescription tableInfoName = new LanguageDescription();
                TableInfo nameTableInfo = DatabaseManager.GetTableInfo(tableInfoName);
                nameTableInfo.Name = "SolarSystemName";
                DatabaseManager.CreateTable(nameTableInfo);

                SolarSystemStargate tableInfoStargate = new SolarSystemStargate();
                TableInfo stargateTableInfo = DatabaseManager.GetTableInfo(tableInfoStargate);
                DatabaseManager.CreateTable(stargateTableInfo);

                SolarSystemPlanet tableInfoPlanet = new SolarSystemPlanet();
                TableInfo planetTableInfo = DatabaseManager.GetTableInfo(tableInfoPlanet);
                DatabaseManager.CreateTable(planetTableInfo);

                Console.WriteLine("Converting Solar Systems");

                count = ConvertMapSolarSystemsFromJSON(path,
                                                solarSystemTableInfo,
                                                stargateTableInfo,
                                                nameTableInfo,
                                                planetTableInfo);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Solar Systems Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Solar Systems ");
            return success;
        }


        private int ConvertMapSolarSystemsFromJSON(string path,
                                            TableInfo solarSystemTableInfo,
                                            TableInfo stargateTableInfo,
                                            TableInfo nameTableInfo,
                                            TableInfo planetTableInfo)
        {
            int count = 0;
            mapSolarSystem newSolarSystem = null;
            SolarSystemPlanet newPlant = null;
            SolarSystemStargate newStargate = null;
            List<mapSolarSystem> batchSolarSystems = new List<mapSolarSystem>();
            List<LanguageDescription> batchNames = new List<LanguageDescription>();
            List<SolarSystemStargate> batchStargates = new List<SolarSystemStargate>();
            List<SolarSystemPlanet> batchPlanets = new List<SolarSystemPlanet>();
            string json = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        json = sr.ReadLine();
                        newSolarSystem = Newtonsoft.Json.JsonConvert.DeserializeObject<mapSolarSystem>(json);

                        if (newSolarSystem.position != null)
                        {
                            newSolarSystem.x = newSolarSystem.position.x;
                            newSolarSystem.y = newSolarSystem.position.y;
                            newSolarSystem.z = newSolarSystem.position.z;
                        }

                        if (newSolarSystem.position2D != null)
                        {
                            newSolarSystem.x2D = newSolarSystem.position2D.x;
                            newSolarSystem.y2D = newSolarSystem.position2D.y;
                        }

                        Utility.AddRecordToBatch<mapSolarSystem>(solarSystemTableInfo, ref batchSolarSystems, newSolarSystem);

                        if (newSolarSystem.name != null)
                        {
                            newSolarSystem.name.parentTypeId = newSolarSystem.solarSystemID;
                            Utility.AddRecordToBatch<LanguageDescription>(nameTableInfo, ref batchNames, newSolarSystem.name);
                        }

                        if (newSolarSystem.stargateIDs != null)
                        {
                            foreach (long stargateId in newSolarSystem.stargateIDs)
                            {
                                newStargate = new SolarSystemStargate();
                                newStargate.solarSystemID = newSolarSystem.solarSystemID;
                                newStargate.stargateID = stargateId;
                                Utility.AddRecordToBatch<SolarSystemStargate>(stargateTableInfo, ref batchStargates, newStargate);
                            }
                        }

                        if (newSolarSystem.planetIDs != null)
                        {
                            foreach (long planetId in newSolarSystem.planetIDs)
                            {
                                newPlant = new SolarSystemPlanet();
                                newPlant.solarSystemID = newSolarSystem.solarSystemID;
                                newPlant.planetID = planetId;
                                Utility.AddRecordToBatch<SolarSystemPlanet>(planetTableInfo, ref batchPlanets, newPlant);
                            }
                        }

                        count++;
                    }
                }
                Utility.InsertBatchRecord<mapSolarSystem>(solarSystemTableInfo, batchSolarSystems);
                Utility.InsertBatchRecord<LanguageDescription>(nameTableInfo, batchNames);
                Utility.InsertBatchRecord<SolarSystemStargate>(stargateTableInfo, batchStargates);
                Utility.InsertBatchRecord<SolarSystemPlanet>(planetTableInfo, batchPlanets);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertMapSolarSystemsFromJSON");
            }

            return count;
        }
        #endregion

        #region "Map Stargates"
        private bool ConvertMapStargates()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = SDEPath + "\\mapStargates.jsonl";


                mapStargate tableInfoMapStargate = new mapStargate();
                TableInfo mapStargateTableInfo = DatabaseManager.GetTableInfo<mapStargate>(tableInfoMapStargate);
                DatabaseManager.CreateTable(mapStargateTableInfo);

                StargateDestination tableInfoStargateDesto = new StargateDestination();
                TableInfo stargateDestinationTableInfo = DatabaseManager.GetTableInfo(tableInfoStargateDesto);
                DatabaseManager.CreateTable(stargateDestinationTableInfo);

                Console.WriteLine("Converting Stargates");

                count = ConvertMapStargatesFromJSON(path,
                                                mapStargateTableInfo,
                                                stargateDestinationTableInfo);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Stargates Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Stargates ");
            return success;
        }


        private int ConvertMapStargatesFromJSON(string path,
                                            TableInfo stargateTableInfo,
                                            TableInfo destoTableInfo)
        {
            int count = 0;
            mapStargate newStargate = null;
            List<mapStargate> batchStargates = new List<mapStargate>();
            List<StargateDestination> batchDestos = new List<StargateDestination>();
            string json = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        json = sr.ReadLine();
                        newStargate = Newtonsoft.Json.JsonConvert.DeserializeObject<mapStargate>(json);

                        if (newStargate.position != null)
                        {
                            newStargate.x = newStargate.position.x;
                            newStargate.y = newStargate.position.y;
                            newStargate.z = newStargate.position.z;
                        }

                        Utility.AddRecordToBatch<mapStargate>(stargateTableInfo, ref batchStargates, newStargate);

                        if (newStargate.destination != null)
                        {
                            newStargate.destination.stargateID = newStargate.stargateID;
                            Utility.AddRecordToBatch<StargateDestination>(destoTableInfo, ref batchDestos, newStargate.destination);
                        }

                        count++;
                    }
                }
                Utility.InsertBatchRecord<mapStargate>(stargateTableInfo, batchStargates);
                Utility.InsertBatchRecord<StargateDestination>(destoTableInfo, batchDestos);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertMapStargatesFromJSON");
            }

            return count;
        }
        #endregion

        #region "Map Stars"
        private bool ConvertMapStars()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = SDEPath + "\\mapStars.jsonl";


                mapStar tableInfoMapStar = new mapStar();
                TableInfo mapStarTableInfo = DatabaseManager.GetTableInfo<mapStar>(tableInfoMapStar);
                DatabaseManager.CreateTable(mapStarTableInfo);

                StarStatistics tableInfoStarStats = new StarStatistics();
                TableInfo starStatsTableiInfo = DatabaseManager.GetTableInfo(tableInfoStarStats);
                DatabaseManager.CreateTable(starStatsTableiInfo);

                Console.WriteLine("Converting Stars");

                count = ConvertMapStarsFromJSON(path,
                                                mapStarTableInfo,
                                                starStatsTableiInfo);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Stars Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Stars ");
            return success;
        }


        private int ConvertMapStarsFromJSON(string path,
                                            TableInfo mapStarTableInfo,
                                            TableInfo starStatsTableiInfo)
        {
            int count = 0;
            mapStar newStar = null;
            List<mapStar> batchStars = new List<mapStar>();
            List<StarStatistics> batchStats = new List<StarStatistics>();
            string json = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        json = sr.ReadLine();
                        newStar = Newtonsoft.Json.JsonConvert.DeserializeObject<mapStar>(json);


                        Utility.AddRecordToBatch<mapStar>(mapStarTableInfo, ref batchStars, newStar);

                        if (newStar.statistics != null)
                        {
                            newStar.statistics.starID = newStar.starID;
                            Utility.AddRecordToBatch<StarStatistics>(starStatsTableiInfo, ref batchStats, newStar.statistics);
                        }

                        count++;
                    }
                }
                Utility.InsertBatchRecord<mapStar>(mapStarTableInfo, batchStars);
                Utility.InsertBatchRecord<StarStatistics>(starStatsTableiInfo, batchStats);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertMapStarsFromJSON");
            }

            return count;
        }
        #endregion

        #region "MarketGroups"
        private bool ConvertMarketGroups()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = SDEPath + "\\marketGroups.jsonl";


                MarketGroup tableInfoGroup = new MarketGroup();
                TableInfo marketGroupTable = DatabaseManager.GetTableInfo<MarketGroup>(tableInfoGroup);
                DatabaseManager.CreateTable(marketGroupTable);

                LanguageDescription tableInfoLangDscr = new LanguageDescription();
                TableInfo descriptionTableInfo = DatabaseManager.GetTableInfo(tableInfoLangDscr);

                descriptionTableInfo.Name = "MarketGroupDescription";
                DatabaseManager.CreateTable(descriptionTableInfo);

                TableInfo nameTableInfo = DatabaseManager.GetTableInfo(tableInfoLangDscr);
                nameTableInfo.Name = "MarketGroupName";
                DatabaseManager.CreateTable(nameTableInfo);

                Console.WriteLine("Converting Market Groups");

                count = ConvertMarketGroupsromJSON(path,
                                                    marketGroupTable,
                                                    descriptionTableInfo,
                                                    nameTableInfo);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Market Groups Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Market Groups ");
            return success;
        }

        private int ConvertMarketGroupsromJSON(string path,
                                            TableInfo marketGroupTable,
                                            TableInfo descriptionTableInfo,
                                            TableInfo nameTableInfo)
        {
            int count = 0;
            MarketGroup newGroup = null;
            List<MarketGroup> batchMarketGroups = new List<MarketGroup>();
            List<LanguageDescription> batchDescription = new List<LanguageDescription>();
            List<LanguageDescription> batchNames = new List<LanguageDescription>();
            string json = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        json = sr.ReadLine();
                        newGroup = Newtonsoft.Json.JsonConvert.DeserializeObject<MarketGroup>(json);
                        Utility.AddRecordToBatch<MarketGroup>(marketGroupTable, ref batchMarketGroups, newGroup);

                        if (newGroup.description != null)
                        {
                            newGroup.description.parentTypeId = newGroup.marketGroupId;
                            Utility.AddRecordToBatch<LanguageDescription>(descriptionTableInfo, ref batchDescription, newGroup.description);
                        }

                        if (newGroup.name != null)
                        {
                            newGroup.name.parentTypeId = newGroup.marketGroupId;
                            Utility.AddRecordToBatch<LanguageDescription>(nameTableInfo, ref batchNames, newGroup.name);
                        }

                        count++;
                    }
                }
                Utility.InsertBatchRecord<MarketGroup>(marketGroupTable, batchMarketGroups);
                Utility.InsertBatchRecord<LanguageDescription>(descriptionTableInfo, batchDescription);
                Utility.InsertBatchRecord<LanguageDescription>(nameTableInfo, batchNames);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertGroupsFromJSON");
            }

            return count;
        }
        #endregion

        #region "Masteries"
        private bool ConvertMasteries()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = SDEPath + "\\masteries.jsonl";


                Mastery tableInfoMastery = new Mastery();
                TableInfo masteryTableInfo = DatabaseManager.GetTableInfo<Mastery>(tableInfoMastery);
                DatabaseManager.CreateTable(masteryTableInfo);

                Console.WriteLine("Converting Masteries");

                count = ConvertMasteriesFromJSON(path,
                                                    masteryTableInfo);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Masteries Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Masteries ");
            return success;
        }

        private int ConvertMasteriesFromJSON(string path,
                                            TableInfo masteryTableInfo)
        {
            int count = 0;
            Mastery newMastery = null;
            Mastery tableMaster = null;
            List<Mastery> batchMasteries = new List<Mastery>();
            string json = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        json = sr.ReadLine();
                        newMastery = Newtonsoft.Json.JsonConvert.DeserializeObject<Mastery>(json);
                        foreach (MasteryLevel masteryLevel in newMastery.levels)
                        {
                            foreach (int value in masteryLevel._value)
                            {
                                tableMaster = new Mastery();
                                tableMaster.masteryId = newMastery.masteryId;
                                tableMaster.level = masteryLevel.level;
                                tableMaster.value = value;
                                Utility.AddRecordToBatch<Mastery>(masteryTableInfo, ref batchMasteries, tableMaster);
                            }
                        }
                        count++;
                    }
                }
                Utility.InsertBatchRecord<Mastery>(masteryTableInfo, batchMasteries);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertMasteriesFromJSON");
            }

            return count;
        }
        #endregion

        #region "Meta Groups"
        private bool ConvertMetaGroups()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = SDEPath + "\\metaGroups.jsonl";


                MetaGroup tableInfoGroup = new MetaGroup();
                TableInfo metaGroupTable = DatabaseManager.GetTableInfo<MetaGroup>(tableInfoGroup);
                DatabaseManager.CreateTable(metaGroupTable);

                LanguageDescription tableInfoLangDscr = new LanguageDescription();
                TableInfo descriptionTableInfo = DatabaseManager.GetTableInfo(tableInfoLangDscr);
                descriptionTableInfo.Name = "MetaGroupDescription";
                DatabaseManager.CreateTable(descriptionTableInfo);


                TableInfo tableInfoName = DatabaseManager.GetTableInfo(tableInfoLangDscr);
                tableInfoName.Name = "MetaGroupName";
                DatabaseManager.CreateTable(tableInfoName);

                Console.WriteLine("Converting Meta Groups");

                count = ConvertMetaGroupsFromJSON(path,
                                                    metaGroupTable,
                                                    descriptionTableInfo,
                                                    tableInfoName);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Meta Groups Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Meta Groups ");
            return success;
        }

        private int ConvertMetaGroupsFromJSON(string path,
                                            TableInfo metaGroupTable,
                                            TableInfo descriptionTable,
                                            TableInfo nameTable)
        {
            int count = 0;
            MetaGroup newGroup = null;
            List<MetaGroup> batchGroups = new List<MetaGroup>();
            List<LanguageDescription> batchDescriptions = new List<LanguageDescription>();
            List<LanguageDescription> batchNames = new List<LanguageDescription>();
            string json = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        json = sr.ReadLine();
                        newGroup = Newtonsoft.Json.JsonConvert.DeserializeObject<MetaGroup>(json);
                        if (newGroup.color != null)
                        {
                            newGroup.r = newGroup.color.r;
                            newGroup.g = newGroup.color.g;
                            newGroup.b = newGroup.color.b;
                        }
                        Utility.AddRecordToBatch<MetaGroup>(metaGroupTable, ref batchGroups, newGroup);

                        if (newGroup.name != null)
                        {
                            newGroup.name.parentTypeId = newGroup.metaGroupID;
                            Utility.AddRecordToBatch<LanguageDescription>(nameTable, ref batchNames, newGroup.name);
                        }

                        if (newGroup.description != null)
                        {
                            newGroup.description.parentTypeId = newGroup.metaGroupID;
                            Utility.AddRecordToBatch<LanguageDescription>(nameTable, ref batchDescriptions, newGroup.description);
                        }

                        count++;
                    }
                }
                Utility.InsertBatchRecord<MetaGroup>(metaGroupTable, batchGroups);
                Utility.InsertBatchRecord<LanguageDescription>(nameTable, batchNames);
                Utility.InsertBatchRecord<LanguageDescription>(descriptionTable, batchDescriptions);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertMetaGroupsFromJSON");
            }

            return count;
        }
        #endregion

        #region "NPC Characters"
        private bool ConvertNPCCharacters()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = SDEPath + "\\npcCharacters.jsonl";

                NPCCharacter tableInfoNPCCharacter = new NPCCharacter();
                TableInfo npcCharacterTableInfo = DatabaseManager.GetTableInfo<NPCCharacter>(tableInfoNPCCharacter);
                DatabaseManager.CreateTable(npcCharacterTableInfo);

                LanguageDescription nameTableInfo = new LanguageDescription();
                TableInfo tableInfoName = DatabaseManager.GetTableInfo(nameTableInfo);
                tableInfoName.Name = "NPCCharacterName";
                DatabaseManager.CreateTable(tableInfoName);

                NPCCharacterAgentInfo tableInfoAgentInfo = new NPCCharacterAgentInfo();
                TableInfo agentInfoTableInfo = DatabaseManager.GetTableInfo(tableInfoAgentInfo);
                DatabaseManager.CreateTable(agentInfoTableInfo);

                NPCCharacterSkill tableInfoSkill = new NPCCharacterSkill();
                TableInfo skillTableInfo = DatabaseManager.GetTableInfo<NPCCharacterSkill>(tableInfoSkill);
                DatabaseManager.CreateTable(skillTableInfo);

                Console.WriteLine("Converting NPC Characters");

                count = ConvertNPCCharactersFromJSON(path,
                                                    npcCharacterTableInfo,
                                                    tableInfoName,
                                                    agentInfoTableInfo,
                                                    skillTableInfo);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting NPC Characters Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " NPC Characters ");
            return success;
        }

        private int ConvertNPCCharactersFromJSON(string path,
                                            TableInfo npcCharTableInfo,
                                            TableInfo nameTable,
                                            TableInfo agentInfoTableInfo,
                                            TableInfo skillTableInfo)
        {
            int count = 0;
            NPCCharacter newNPCCharacter = null;
            List<NPCCharacter> batchNPCCharacters = new List<NPCCharacter>();
            List<LanguageDescription> batchNames = new List<LanguageDescription>();
            List<NPCCharacterAgentInfo> batchAgents = new List<NPCCharacterAgentInfo>();
            List<NPCCharacterSkill> batchSkills = new List<NPCCharacterSkill>();
            string json = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        json = sr.ReadLine();
                        newNPCCharacter = Newtonsoft.Json.JsonConvert.DeserializeObject<NPCCharacter>(json);
                        Utility.AddRecordToBatch<NPCCharacter>(npcCharTableInfo, ref batchNPCCharacters, newNPCCharacter);

                        if (newNPCCharacter.name != null)
                        {
                            newNPCCharacter.name.parentTypeId = newNPCCharacter.npcCharacterID;
                            Utility.AddRecordToBatch<LanguageDescription>(nameTable, ref batchNames, newNPCCharacter.name);
                        }

                        if (newNPCCharacter.agent != null)
                        {
                            newNPCCharacter.agent.npcCharacterID = newNPCCharacter.npcCharacterID;
                            Utility.AddRecordToBatch<NPCCharacterAgentInfo>(agentInfoTableInfo, ref batchAgents, newNPCCharacter.agent);
                        }

                        if (newNPCCharacter.skills != null)
                        {
                            foreach (NPCCharacterSkill skill in newNPCCharacter.skills)
                            {
                                skill.npcCharacterID = newNPCCharacter.npcCharacterID;
                                Utility.AddRecordToBatch<NPCCharacterSkill>(skillTableInfo, ref batchSkills, skill);
                            }
                        }

                        count++;
                    }
                }
                Utility.InsertBatchRecord<NPCCharacter>(npcCharTableInfo, batchNPCCharacters);
                Utility.InsertBatchRecord<LanguageDescription>(nameTable, batchNames);
                Utility.InsertBatchRecord<NPCCharacterAgentInfo>(agentInfoTableInfo, batchAgents);
                Utility.InsertBatchRecord<NPCCharacterSkill>(skillTableInfo, batchSkills);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertNPCCharactersFromJSON");
            }

            return count;
        }
        #endregion

        #region "NPC Corporation Divisions"
        private bool ConvertNPCCorpDivisions()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = SDEPath + "\\npcCorporationDivisions.jsonl";


                NPCCorporationDivision tableInfoCorpDivision = new NPCCorporationDivision();
                TableInfo corpDivisionTable = DatabaseManager.GetTableInfo<NPCCorporationDivision>(tableInfoCorpDivision);
                DatabaseManager.CreateTable(corpDivisionTable);

                LanguageDescription tableInfoLangDscr = new LanguageDescription();
                TableInfo descriptionTableInfo = DatabaseManager.GetTableInfo(tableInfoLangDscr);
                descriptionTableInfo.Name = "NPCCorporationDivisionDescription";
                DatabaseManager.CreateTable(descriptionTableInfo);

                TableInfo nameTableInfo = DatabaseManager.GetTableInfo(tableInfoLangDscr);
                nameTableInfo.Name = "NPCCorporationDivisionName";
                DatabaseManager.CreateTable(nameTableInfo);

                TableInfo leaderNameTableInfo = DatabaseManager.GetTableInfo(tableInfoLangDscr);
                leaderNameTableInfo.Name = "NPCCorporationDivisionLeaderName";
                DatabaseManager.CreateTable(leaderNameTableInfo);

                Console.WriteLine("Converting NPC Corp Divisions");

                count = ConvertNPCCorpDivisionsFromJSON(path,
                                                    corpDivisionTable,
                                                    descriptionTableInfo,
                                                    nameTableInfo,
                                                    leaderNameTableInfo);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting NPC Corp Divisions Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " NPC Corp Divisions ");
            return success;
        }

        private int ConvertNPCCorpDivisionsFromJSON(string path,
                                            TableInfo npcCorpDivisionTable,
                                            TableInfo descriptionTableInfo,
                                            TableInfo nameTableInfo,
                                            TableInfo leaderTypeNameTableInfo)
        {
            int count = 0;
            NPCCorporationDivision newNPCCorpDivision = null;
            List<NPCCorporationDivision> batchDivisions = new List<NPCCorporationDivision>();
            List<LanguageDescription> batchNames = new List<LanguageDescription>();
            List<LanguageDescription> batchLeaderNames = new List<LanguageDescription>();
            List<LanguageDescription> batchDescriptions = new List<LanguageDescription>();
            string json = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        json = sr.ReadLine();
                        newNPCCorpDivision = Newtonsoft.Json.JsonConvert.DeserializeObject<NPCCorporationDivision>(json);
                        Utility.AddRecordToBatch<NPCCorporationDivision>(npcCorpDivisionTable, ref batchDivisions, newNPCCorpDivision);

                        if (newNPCCorpDivision.name != null)
                        {
                            newNPCCorpDivision.name.parentTypeId = newNPCCorpDivision.npcCorporationDivisionID;
                            Utility.AddRecordToBatch<LanguageDescription>(nameTableInfo, ref batchNames, newNPCCorpDivision.name);
                        }

                        if (newNPCCorpDivision.leaderTypeName != null)
                        {
                            newNPCCorpDivision.leaderTypeName.parentTypeId = newNPCCorpDivision.npcCorporationDivisionID;
                            Utility.AddRecordToBatch<LanguageDescription>(leaderTypeNameTableInfo, ref batchLeaderNames, newNPCCorpDivision.leaderTypeName);
                        }

                        if (newNPCCorpDivision.description != null)
                        {
                            newNPCCorpDivision.description.parentTypeId = newNPCCorpDivision.npcCorporationDivisionID;
                            Utility.AddRecordToBatch<LanguageDescription>(descriptionTableInfo, ref batchDescriptions, newNPCCorpDivision.description);
                        }

                        count++;
                    }
                }
                Utility.InsertBatchRecord<NPCCorporationDivision>(npcCorpDivisionTable, batchDivisions);
                Utility.InsertBatchRecord<LanguageDescription>(nameTableInfo, batchNames);
                Utility.InsertBatchRecord<LanguageDescription>(descriptionTableInfo, batchDescriptions);
                Utility.InsertBatchRecord<LanguageDescription>(leaderTypeNameTableInfo, batchLeaderNames);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertNPCCorpDivisionsFromJSON");
            }

            return count;
        }
        #endregion

        #region "NPC Corporations"
        private bool ConvertNPCCorps()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = SDEPath + "\\npcCorporations.jsonl";


                NPCCorporation tableInfoNPCCorp = new NPCCorporation();
                TableInfo npcCorpTable = DatabaseManager.GetTableInfo<NPCCorporation>(tableInfoNPCCorp);
                DatabaseManager.CreateTable(npcCorpTable);

                NPCCorpAllowedRace tableInfoNPCCorpAllowedRaces = new NPCCorpAllowedRace();
                TableInfo npcCorpAllowedRacesTable = DatabaseManager.GetTableInfo<NPCCorpAllowedRace>(tableInfoNPCCorpAllowedRaces);
                DatabaseManager.CreateTable(npcCorpAllowedRacesTable);

                CorporationTrade tableInfoNPCCorpTrades = new CorporationTrade();
                TableInfo npcCorpTradesTable = DatabaseManager.GetTableInfo<CorporationTrade>(tableInfoNPCCorpTrades);
                DatabaseManager.CreateTable(npcCorpTradesTable);

                LanguageDescription tableInfoLangDscr = new LanguageDescription();
                TableInfo descriptionTableInfo = DatabaseManager.GetTableInfo<LanguageDescription>(tableInfoLangDscr);
                descriptionTableInfo.Name = "NPCCorporationDescription";
                DatabaseManager.CreateTable(descriptionTableInfo);

                NPCCorporationCorpDivision tableInfoNPCCorpDivision = new NPCCorporationCorpDivision();
                TableInfo npcCorpDivisionTable = DatabaseManager.GetTableInfo<NPCCorporationCorpDivision>(tableInfoNPCCorpDivision);
                DatabaseManager.CreateTable(npcCorpDivisionTable);

                NPCCorpLPOfferTable tableInfoNPCCorplpTable = new NPCCorpLPOfferTable();
                TableInfo npcCorpLPTable = DatabaseManager.GetTableInfo<NPCCorpLPOfferTable>(tableInfoNPCCorplpTable);
                DatabaseManager.CreateTable(npcCorpLPTable);

                TableInfo nameTableInfo = DatabaseManager.GetTableInfo<LanguageDescription>(tableInfoLangDscr);
                nameTableInfo.Name = "NPCCorporationName";
                DatabaseManager.CreateTable(nameTableInfo);

                Console.WriteLine("Converting NPC Corporations");

                count = ConvertNPCCorpsFromJSON(path,
                                                    npcCorpTable,
                                                    npcCorpAllowedRacesTable,
                                                    npcCorpTradesTable,
                                                    descriptionTableInfo,
                                                    npcCorpDivisionTable,
                                                    npcCorpLPTable,
                                                    nameTableInfo);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting NPC Corporations Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " NPC Corporations ");
            return success;
        }

        private int ConvertNPCCorpsFromJSON(string path,
                                            TableInfo npcCorpTable,
                                            TableInfo npcCorpAllowedRacesTable,
                                            TableInfo npcCorpTradesTable,
                                            TableInfo descriptionTableInfo,
                                            TableInfo npcCorpDivisionTable,
                                            TableInfo npcCorpLPTable,
                                            TableInfo nameTableInfo)
        {
            int count = 0;
            NPCCorporation newNPCCorp = null;
            NPCCorpAllowedRace newNPCCorpAllowedRace = null;
            NPCCorpLPOfferTable newNPCCorpLPOfferTable = null;
            List<NPCCorporation> batchCorps = new List<NPCCorporation>();
            List<NPCCorpAllowedRace> batchRaces = new List<NPCCorpAllowedRace>();
            List<NPCCorpLPOfferTable> batchOffers = new List<NPCCorpLPOfferTable>();
            List<CorporationTrade> batchTrades = new List<CorporationTrade>();
            List<LanguageDescription> batchDescriptions = new List<LanguageDescription>();
            List<LanguageDescription> batchNames = new List<LanguageDescription>();
            List<NPCCorporationCorpDivision> batchDivisions = new List<NPCCorporationCorpDivision>();
            string json = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        json = sr.ReadLine();
                        newNPCCorp = Newtonsoft.Json.JsonConvert.DeserializeObject<NPCCorporation>(json);
                        Utility.AddRecordToBatch<NPCCorporation>(npcCorpTable, ref batchCorps, newNPCCorp);

                        if (newNPCCorp.name != null)
                        {
                            newNPCCorp.name.parentTypeId = newNPCCorp.npcCorporationID;
                            Utility.AddRecordToBatch<LanguageDescription>(nameTableInfo, ref batchNames, newNPCCorp.name);
                        }

                        if (newNPCCorp.description != null)
                        {
                            newNPCCorp.description.parentTypeId = newNPCCorp.npcCorporationID;
                            Utility.AddRecordToBatch<LanguageDescription>(descriptionTableInfo, ref batchDescriptions, newNPCCorp.description);
                        }

                        if (newNPCCorp.allowedMemberRaces?.Count > 0)
                        {
                            foreach (int raceID in newNPCCorp.allowedMemberRaces)
                            {
                                newNPCCorpAllowedRace = new NPCCorpAllowedRace() { npcCorporationID = newNPCCorp.npcCorporationID, raceID = raceID };
                                Utility.AddRecordToBatch<NPCCorpAllowedRace>(npcCorpAllowedRacesTable, ref batchRaces, newNPCCorpAllowedRace);
                            }
                        }

                        if (newNPCCorp.corporationTrades?.Count > 0)
                        {
                            foreach (CorporationTrade corporationTrade in newNPCCorp.corporationTrades)
                            {
                                corporationTrade.npcCorporationID = newNPCCorp.npcCorporationID;
                                Utility.AddRecordToBatch<CorporationTrade>(npcCorpTradesTable, ref batchTrades, corporationTrade);
                            }
                        }

                        if (newNPCCorp.divisions?.Count > 0)
                        {
                            foreach (NPCCorporationCorpDivision division in newNPCCorp.divisions)
                            {
                                division.npcCorporationID = newNPCCorp.npcCorporationID;
                                Utility.AddRecordToBatch<NPCCorporationCorpDivision>(npcCorpDivisionTable, ref batchDivisions, division);
                            }
                        }

                        if (newNPCCorp.lpOfferTables?.Count > 0)
                        {
                            foreach (int offerID in newNPCCorp.lpOfferTables)
                            {
                                newNPCCorpLPOfferTable = new NPCCorpLPOfferTable();
                                newNPCCorpLPOfferTable.npcCorporationID = newNPCCorp.npcCorporationID;
                                newNPCCorpLPOfferTable.lpOfferTableID = offerID;
                                Utility.AddRecordToBatch<NPCCorpLPOfferTable>(npcCorpLPTable, ref batchOffers, newNPCCorpLPOfferTable);
                            }
                        }
                        count++;
                    }
                }

                Utility.InsertBatchRecord(npcCorpTable, batchCorps);
                Utility.InsertBatchRecord(npcCorpAllowedRacesTable, batchRaces);
                Utility.InsertBatchRecord(npcCorpLPTable, batchOffers);
                Utility.InsertBatchRecord(npcCorpTradesTable, batchTrades);
                Utility.InsertBatchRecord(npcCorpDivisionTable, batchDivisions);
                Utility.InsertBatchRecord(descriptionTableInfo, batchDescriptions);
                Utility.InsertBatchRecord(nameTableInfo, batchNames);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertNPCCorpsFromJSON");
            }

            return count;
        }
        #endregion

        #region "NPC Stations"
        private bool ConvertNPCStations()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = SDEPath + "\\npcStations.jsonl";


                NPCStation tableInfoNPCStations = new NPCStation();
                TableInfo npcStationTableInfo = DatabaseManager.GetTableInfo<NPCStation>(tableInfoNPCStations);
                DatabaseManager.CreateTable(npcStationTableInfo);

                Console.WriteLine("Converting NPC Stations");

                count = ConvertNPCStationsJSON(path,
                                                npcStationTableInfo);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting NPC Stations Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " NPC Stations ");
            return success;
        }

        private int ConvertNPCStationsJSON(string path,
                                            TableInfo npcStationTable)
        {
            int count = 0;
            NPCStation newNPCStations = null;
            List<NPCStation> batchStations = new List<NPCStation>();
            string json = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        json = sr.ReadLine();
                        newNPCStations = Newtonsoft.Json.JsonConvert.DeserializeObject<NPCStation>(json);

                        if (newNPCStations.position != null)
                        {
                            newNPCStations.x = newNPCStations.position.x;
                            newNPCStations.y = newNPCStations.position.y;
                            newNPCStations.z = newNPCStations.position.z;
                        }

                        Utility.AddRecordToBatch<NPCStation>(npcStationTable, ref batchStations, newNPCStations);

                        count++;
                    }
                }
                Utility.InsertBatchRecord<NPCStation>(npcStationTable, batchStations);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertNPCStationsJSON");
            }

            return count;
        }
        #endregion

        #region "Planet Resources"
        private bool ConvertPlanetResources()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = SDEPath + "\\planetResources.jsonl";


                PlanetResource tableInfoPlanetResource = new PlanetResource();
                TableInfo planetResource = DatabaseManager.GetTableInfo<PlanetResource>(tableInfoPlanetResource);
                DatabaseManager.CreateTable(planetResource);

                Console.WriteLine("Converting Planet Resources");

                count = ConvertPlanetResourcesFromJSON(path,
                                                    planetResource);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Planet Resources Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Planet Resources ");
            return success;
        }

        private int ConvertPlanetResourcesFromJSON(string path,
                                            TableInfo planetResourcesTable)
        {
            int count = 0;
            PlanetResource newPlanetResource = null;
            List<PlanetResource> batchPlanetResources = new List<PlanetResource>();
            string json = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        json = sr.ReadLine();
                        newPlanetResource = Newtonsoft.Json.JsonConvert.DeserializeObject<PlanetResource>(json);

                        if (newPlanetResource.reagent != null)
                        {
                            newPlanetResource.amount_per_cycle = newPlanetResource.reagent.amount_per_cycle;
                            newPlanetResource.cycle_period = newPlanetResource.reagent.cycle_period;
                            newPlanetResource.secured_capacity = newPlanetResource.reagent.secured_capacity;
                            newPlanetResource.unsecured_capacity = newPlanetResource.reagent.unsecured_capacity;
                            newPlanetResource.reagent_type_id = newPlanetResource.reagent.type_id;

                        }

                        Utility.AddRecordToBatch<PlanetResource>(planetResourcesTable, ref batchPlanetResources, newPlanetResource);

                        count++;
                    }
                    Utility.InsertBatchRecord<PlanetResource>(planetResourcesTable, batchPlanetResources);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertPlanetResourcesFromJSON");
            }

            return count;
        }
        #endregion

        #region "Planet Schematics"
        private bool ConvertPlanetSchematics()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = SDEPath + "\\planetSchematics.jsonl";


                PlanetSchematic tableInfoPlanetSchematic = new PlanetSchematic();
                TableInfo planetSchematicTable = DatabaseManager.GetTableInfo<PlanetSchematic>(tableInfoPlanetSchematic);
                DatabaseManager.CreateTable(planetSchematicTable);

                LanguageDescription tableInfoLangDscr = new LanguageDescription();
                TableInfo nameTableInfo = DatabaseManager.GetTableInfo(tableInfoLangDscr);
                nameTableInfo.Name = "PlanetSchematicName";
                DatabaseManager.CreateTable(nameTableInfo);

                PlanetSchematicPin tableInfoPlanetSchematicPin = new PlanetSchematicPin();
                TableInfo planetSchematicPinTable = DatabaseManager.GetTableInfo<PlanetSchematicPin>(tableInfoPlanetSchematicPin);
                DatabaseManager.CreateTable(planetSchematicPinTable);

                PlanetSchematicType tableInfoPlanetSchematicType = new PlanetSchematicType();
                TableInfo planetSchematicTypeTable = DatabaseManager.GetTableInfo<PlanetSchematicType>(tableInfoPlanetSchematicType);
                DatabaseManager.CreateTable(planetSchematicTypeTable);

                Console.WriteLine("Converting Planet Schematics");

                count = ConvertPlanetSchematicsFromJSON(path,
                                                    planetSchematicTable,
                                                    nameTableInfo,
                                                    planetSchematicPinTable,
                                                    planetSchematicTypeTable);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Planet Schematics Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Planet Schematics ");
            return success;
        }

        private int ConvertPlanetSchematicsFromJSON(string path,
                                            TableInfo planetSchematicTable,
                                            TableInfo nameTable,
                                            TableInfo planetSchematicPinTable,
                                            TableInfo planetSchematicTypeTable)
        {
            int count = 0;
            PlanetSchematic newPlanetSchematic = null;
            PlanetSchematicPin newplanetSchematicPin = null;
            List<PlanetSchematic> batchSchematics = new List<PlanetSchematic>();
            List<LanguageDescription> batchNames = new List<LanguageDescription>();
            List<PlanetSchematicPin> batchPins = new List<PlanetSchematicPin>();
            List<PlanetSchematicType> batchTypes = new List<PlanetSchematicType>();
            string json = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        json = sr.ReadLine();
                        newPlanetSchematic = Newtonsoft.Json.JsonConvert.DeserializeObject<PlanetSchematic>(json);
                        Utility.AddRecordToBatch(planetSchematicTable, ref batchSchematics, newPlanetSchematic);

                        if (newPlanetSchematic.name != null)
                        {
                            newPlanetSchematic.name.parentTypeId = newPlanetSchematic.planetSchematicID;
                            Utility.AddRecordToBatch<LanguageDescription>(nameTable, ref batchNames, newPlanetSchematic.name);
                        }

                        if (newPlanetSchematic.pins?.Count > 0)
                        {
                            foreach (int pinId in newPlanetSchematic.pins)
                            {
                                newplanetSchematicPin = new PlanetSchematicPin();
                                newplanetSchematicPin.planetSchematicID = newPlanetSchematic.planetSchematicID;
                                newplanetSchematicPin.pinID = pinId;
                                Utility.AddRecordToBatch<PlanetSchematicPin>(planetSchematicPinTable, ref batchPins, newplanetSchematicPin);
                            }
                        }

                        if (newPlanetSchematic.types?.Count > 0)
                        {
                            foreach (PlanetSchematicType planetSchematicType in newPlanetSchematic.types)
                            {
                                planetSchematicType.planetSchematicID = newPlanetSchematic.planetSchematicID;
                                Utility.AddRecordToBatch<PlanetSchematicType>(planetSchematicTypeTable, ref batchTypes, planetSchematicType);
                            }
                        }
                        count++;
                    }
                }
                Utility.InsertBatchRecord(planetSchematicTable, batchSchematics);
                Utility.InsertBatchRecord(nameTable, batchNames);
                Utility.InsertBatchRecord(planetSchematicPinTable, batchPins);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertPlanetResourcesFromJSON");
            }

            return count;
        }
        #endregion

        #region "Races"
        private bool ConvertRaces()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = SDEPath + "\\races.jsonl";


                Race tableInfoRace = new Race();
                TableInfo raceTable = DatabaseManager.GetTableInfo<Race>(tableInfoRace);
                DatabaseManager.CreateTable(raceTable);

                LanguageDescription tableInfoLangDscr = new LanguageDescription();

                TableInfo descriptionTableInfo = DatabaseManager.GetTableInfo(tableInfoLangDscr);
                descriptionTableInfo.Name = "RaceDescription";
                DatabaseManager.CreateTable(descriptionTableInfo);

                TableInfo nameTableInfo = DatabaseManager.GetTableInfo(tableInfoLangDscr);
                nameTableInfo.Name = "RaceName";
                DatabaseManager.CreateTable(nameTableInfo);

                RaceSkill tableInfoRaceSkill = new RaceSkill();
                TableInfo raceSkillTable = DatabaseManager.GetTableInfo<RaceSkill>(tableInfoRaceSkill);
                DatabaseManager.CreateTable(raceSkillTable);

                Console.WriteLine("Converting Races");

                count = ConvertRacesFromJSON(path,
                                                raceTable,
                                                descriptionTableInfo,
                                                raceSkillTable,
                                                nameTableInfo);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Races Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Races ");
            return success;
        }

        private int ConvertRacesFromJSON(string path,
                                            TableInfo raceTable,
                                            TableInfo descriptionTableInfo,
                                            TableInfo raceSkillTable,
                                            TableInfo nameTableInfo)
        {
            int count = 0;
            Race newRace = null;
            List<Race> batchRaces = new List<Race>();
            List<LanguageDescription> batchNames = new List<LanguageDescription>();
            List<LanguageDescription> batchDescriptions = new List<LanguageDescription>();
            List<RaceSkill> batchSkills = new List<RaceSkill>();
            string json = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        json = sr.ReadLine();
                        newRace = Newtonsoft.Json.JsonConvert.DeserializeObject<Race>(json);
                        Utility.AddRecordToBatch<Race>(raceTable, ref batchRaces, newRace);

                        if (newRace.name != null)
                        {
                            newRace.name.parentTypeId = newRace.raceID;
                            Utility.AddRecordToBatch<LanguageDescription>(nameTableInfo, ref batchNames, newRace.name);
                        }

                        if (newRace.description != null)
                        {
                            newRace.description.parentTypeId = newRace.raceID;
                            Utility.AddRecordToBatch<LanguageDescription>(descriptionTableInfo, ref batchDescriptions, newRace.description);
                        }

                        if (newRace.skills?.Count > 0)
                        {
                            foreach (RaceSkill skill in newRace.skills)
                            {
                                skill.raceID = newRace.raceID;
                                Utility.AddRecordToBatch<RaceSkill>(raceSkillTable, ref batchSkills, skill);
                            }
                        }

                        count++;
                    }
                }
                Utility.InsertBatchRecord<Race>(raceTable, batchRaces);
                Utility.InsertBatchRecord<LanguageDescription>(nameTableInfo, batchNames);
                Utility.InsertBatchRecord<LanguageDescription>(descriptionTableInfo, batchDescriptions);
                Utility.InsertBatchRecord<RaceSkill>(raceSkillTable, batchSkills);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertRacesFromJSON");
            }

            return count;
        }
        #endregion

        #region "Skin License"
        private bool ConvertSkinLicense()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = "\\skinLicenses.jsonl";


                SkinLicense tableInfoSkinLicense = new SkinLicense();
                TableInfo skinLicenseTable = DatabaseManager.GetTableInfo<SkinLicense>(tableInfoSkinLicense);
                DatabaseManager.CreateTable(skinLicenseTable);

                Console.WriteLine("Converting Skin License");

                count = ConvertFileForType<SkinLicense>(skinLicenseTable, path);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Skin License Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Skin License ");
            return success;
        }
        #endregion

        #region "Skin Materials"
        private bool ConvertSkinMaterials()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = SDEPath + "\\skinMaterials.jsonl";


                SkinMaterial tableInfoSkinMaterial = new SkinMaterial();
                TableInfo skinMaterialTable = DatabaseManager.GetTableInfo<SkinMaterial>(tableInfoSkinMaterial);
                DatabaseManager.CreateTable(skinMaterialTable);

                LanguageDescription tableInfoLangDscr = new LanguageDescription();
                TableInfo displayNameTableInfo = DatabaseManager.GetTableInfo(tableInfoLangDscr);
                displayNameTableInfo.Name = "SkinMaterialDisplayName";
                DatabaseManager.CreateTable(displayNameTableInfo);

                Console.WriteLine("Converting Skin Materials");

                count = ConvertSkinMaterialsFromJSON(path, skinMaterialTable, displayNameTableInfo);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Skin Materials Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Skin Materials ");
            return success;
        }

        private int ConvertSkinMaterialsFromJSON(string path,
                                            TableInfo skinMaterialTable,
                                            TableInfo displayNameTableInfo)
        {
            int count = 0;
            SkinMaterial newSkinMaterial = null;
            List<SkinMaterial> batchMaterials = new List<SkinMaterial>();
            List<LanguageDescription> batchDisplayNames = new List<LanguageDescription>();
            string json = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        json = sr.ReadLine();
                        newSkinMaterial = Newtonsoft.Json.JsonConvert.DeserializeObject<SkinMaterial>(json);
                        Utility.AddRecordToBatch<SkinMaterial>(skinMaterialTable, ref batchMaterials, newSkinMaterial);

                        if (newSkinMaterial.displayName != null)
                        {
                            newSkinMaterial.displayName.parentTypeId = newSkinMaterial.skinMaterialID;
                            Utility.AddRecordToBatch<LanguageDescription>(displayNameTableInfo, ref batchDisplayNames, newSkinMaterial.displayName);
                        }
                        count++;
                    }
                }

                Utility.InsertBatchRecord<SkinMaterial>(skinMaterialTable, batchMaterials);
                Utility.InsertBatchRecord<LanguageDescription>(displayNameTableInfo, batchDisplayNames);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertSkinMaterialsFromJSON");
            }

            return count;
        }
        #endregion

        #region "Skins"
        private bool ConvertSkins()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = SDEPath + "\\skins.jsonl";


                Skin tableInfoSkin = new Skin();
                TableInfo skinTable = DatabaseManager.GetTableInfo<Skin>(tableInfoSkin);
                DatabaseManager.CreateTable(skinTable);

                SkinType tableInfoSkinType = new SkinType();
                TableInfo skinTypelTable = DatabaseManager.GetTableInfo<SkinType>(tableInfoSkinType);
                DatabaseManager.CreateTable(skinTypelTable);

                Console.WriteLine("Converting Skins");
                count = ConvertSkinsFromJSON(path,
                                                skinTable,
                                                skinTypelTable);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Skins Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Skins ");
            return success;
        }

        private int ConvertSkinsFromJSON(string path,
                                            TableInfo skinLicenseTable,
                                            TableInfo skinTypeTable)
        {
            int count = 0;
            Skin newSkin = null;
            SkinType newSkinType = null;
            List<Skin> batchSkins = new List<Skin>();
            List<SkinType> batchSkinTypes = new List<SkinType>();
            string json = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        json = sr.ReadLine();
                        newSkin = Newtonsoft.Json.JsonConvert.DeserializeObject<Skin>(json);
                        Utility.AddRecordToBatch<Skin>(skinLicenseTable, ref batchSkins, newSkin);

                        if (newSkin.types?.Count > 0)
                        {
                            foreach (int type in newSkin.types)
                            {
                                newSkinType = new SkinType() { skinID = newSkin.skinID, typeID = type };
                                Utility.AddRecordToBatch<SkinType>(skinTypeTable, ref batchSkinTypes, newSkinType);
                            }
                        }
                        count++;
                    }

                    Utility.InsertBatchRecord<Skin>(skinLicenseTable, batchSkins);
                    Utility.InsertBatchRecord<SkinType>(skinTypeTable, batchSkinTypes);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertSkinsFromJSON");
            }

            return count;
        }
        #endregion

        #region "Sovereignty Upgrades"
        private bool ConvertSovereigntyUpgrades()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = SDEPath + "\\sovereigntyUpgrades.jsonl";


                SovereigntyUpgrade tableInfoSovereigntyUpgrade = new SovereigntyUpgrade();
                TableInfo sovereigntyUpgradeTable = DatabaseManager.GetTableInfo<SovereigntyUpgrade>(tableInfoSovereigntyUpgrade);
                DatabaseManager.CreateTable(sovereigntyUpgradeTable);

                Console.WriteLine("Converting Sovereignty Upgrades");
                count = ConvertSovereigntyUpgradesFromJSON(path,
                                                    sovereigntyUpgradeTable);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Sovereignty Upgrades Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Sovereignty Upgrades ");
            return success;
        }

        private int ConvertSovereigntyUpgradesFromJSON(string path,
                                            TableInfo sovUpgradeTable)
        {
            int count = 0;
            SovereigntyUpgrade newSovUpgrade = null;
            List<SovereigntyUpgrade> batchUpgrades = new List<SovereigntyUpgrade>();
            string json = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        json = sr.ReadLine();
                        newSovUpgrade = Newtonsoft.Json.JsonConvert.DeserializeObject<SovereigntyUpgrade>(json);

                        if (newSovUpgrade.fuel != null)
                        {
                            newSovUpgrade.fuel_hourly_upkeep = newSovUpgrade.fuel.hourly_upkeep;
                            newSovUpgrade.fuel_startup_cost = newSovUpgrade.fuel.startup_cost;
                            newSovUpgrade.fuel_type_id = newSovUpgrade.fuel.type_id;
                        }

                        Utility.AddRecordToBatch<SovereigntyUpgrade>(sovUpgradeTable, ref batchUpgrades, newSovUpgrade);

                        count++;
                    }
                }
                Utility.InsertBatchRecord<SovereigntyUpgrade>(sovUpgradeTable, batchUpgrades);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertSovereigntyUpgradesFromJSON");
            }

            return count;
        }
        #endregion

        #region "Station Operations"
        private bool ConvertStationOperations()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = SDEPath + "\\stationOperations.jsonl";


                StationOperation tableInfoStationOperation = new StationOperation();
                TableInfo stationOperationTable = DatabaseManager.GetTableInfo<StationOperation>(tableInfoStationOperation);
                DatabaseManager.CreateTable(stationOperationTable);

                StationOperationService tableInfoStationOperationService = new StationOperationService();
                TableInfo stationOperationServiceTable = DatabaseManager.GetTableInfo<StationOperationService>(tableInfoStationOperationService);
                DatabaseManager.CreateTable(stationOperationServiceTable);

                StationType tableInfoStationType = new StationType();
                TableInfo stationTypeTable = DatabaseManager.GetTableInfo<StationType>(tableInfoStationType);
                DatabaseManager.CreateTable(stationTypeTable);

                LanguageDescription tableInfoLangDscr = new LanguageDescription();

                TableInfo nameTableInfo = DatabaseManager.GetTableInfo<LanguageDescription>(tableInfoLangDscr);
                nameTableInfo.Name = "StationOperationName";
                DatabaseManager.CreateTable(nameTableInfo);

                TableInfo descriptionTableInfo = DatabaseManager.GetTableInfo<LanguageDescription>(tableInfoLangDscr);
                descriptionTableInfo.Name = "StationOperationDescription";
                DatabaseManager.CreateTable(descriptionTableInfo);

                Console.WriteLine("Converting Station Operations");
                count = ConvertStationOperationsFromJSON(path,
                                                    stationOperationTable,
                                                    stationOperationServiceTable,
                                                    stationTypeTable,
                                                    descriptionTableInfo,
                                                    nameTableInfo);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Station Operations Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Station Operations ");
            return success;
        }

        private int ConvertStationOperationsFromJSON(string path,
                                            TableInfo stationOperationTable,
                                            TableInfo stationOperationServiceTable,
                                            TableInfo stationTypeTable,
                                            TableInfo descriptionTable,
                                            TableInfo nameTable)
        {
            int count = 0;
            StationOperation newStationOperation = null;
            StationOperationService sto = new StationOperationService();
            List<StationOperation> batchOps = new List<StationOperation>();
            List<LanguageDescription> batchDescriptions = new List<LanguageDescription>();
            List<LanguageDescription> batchNames = new List<LanguageDescription>();
            List<StationOperationService> batchServices = new List<StationOperationService>();
            List<StationType> batchTypes = new List<StationType>();
            string json = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        json = sr.ReadLine();
                        newStationOperation = Newtonsoft.Json.JsonConvert.DeserializeObject<StationOperation>(json);
                        Utility.AddRecordToBatch<StationOperation>(stationOperationTable, ref batchOps, newStationOperation);

                        if (newStationOperation.operationName != null)
                        {
                            newStationOperation.operationName.parentTypeId = newStationOperation.stationOperationID;
                            Utility.AddRecordToBatch<LanguageDescription>(nameTable, ref batchNames, newStationOperation.operationName);
                        }

                        if (newStationOperation.description != null)
                        {
                            newStationOperation.description.parentTypeId = newStationOperation.stationOperationID;
                            Utility.AddRecordToBatch<LanguageDescription>(descriptionTable, ref batchDescriptions, newStationOperation.description);
                        }

                        if (newStationOperation.services?.Count > 0)
                        {
                            foreach (int service in newStationOperation.services)
                            {
                                sto = new StationOperationService();
                                sto.stationOperationID = newStationOperation.stationOperationID;
                                sto.stationServiceID = service;
                                Utility.AddRecordToBatch<StationOperationService>(stationOperationServiceTable, ref batchServices, sto);

                            }
                        }

                        if (newStationOperation.stationTypes?.Count > 0)
                        {
                            foreach (StationType stationType in newStationOperation.stationTypes)
                            {
                                stationType.stationOperationID = newStationOperation.stationOperationID;
                                Utility.AddRecordToBatch<StationType>(stationTypeTable, ref batchTypes, stationType);
                            }
                        }

                        count++;
                    }
                }
                Utility.InsertBatchRecord<StationOperation>(stationOperationTable, batchOps);
                Utility.InsertBatchRecord<LanguageDescription>(nameTable, batchNames);
                Utility.InsertBatchRecord<LanguageDescription>(descriptionTable, batchDescriptions);
                Utility.InsertBatchRecord<StationOperationService>(stationOperationServiceTable, batchServices);
                Utility.InsertBatchRecord<StationType>(stationTypeTable, batchTypes);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertStationOperationsFromJSON");
            }

            return count;
        }
        #endregion

        #region "Station Services"
        private bool ConvertStationServices()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = SDEPath + "\\stationServices.jsonl";

                LanguageDescription tableInfoLangDscr = new LanguageDescription();
                TableInfo descriptionTableInfo = DatabaseManager.GetTableInfo<LanguageDescription>(tableInfoLangDscr);
                descriptionTableInfo.Name = "StationService";
                DatabaseManager.CreateTable(descriptionTableInfo);

                Console.WriteLine("Converting Station Services");
                count = ConvertStationServicesFromJSON(path,
                                                    descriptionTableInfo);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Station Services Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Station Services ");
            return success;
        }

        private int ConvertStationServicesFromJSON(string path,
                                            TableInfo serviceNameTable)
        {
            int count = 0;
            StationService newStationService = null;
            List<LanguageDescription> batchNames = new List<LanguageDescription>();
            string json = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        json = sr.ReadLine();
                        newStationService = Newtonsoft.Json.JsonConvert.DeserializeObject<StationService>(json);

                        if (newStationService.serviceName != null)
                        {
                            newStationService.serviceName.parentTypeId = newStationService.stationServiceID;
                            Utility.AddRecordToBatch<LanguageDescription>(serviceNameTable, ref batchNames, newStationService.serviceName);
                        }

                        count++;
                    }
                }
                Utility.InsertBatchRecord<LanguageDescription>(serviceNameTable, batchNames);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertStationServicesFromJSON");
            }

            return count;
        }
        #endregion

        #region "Translation Languages"
        private bool ConvertTranslationLanguages()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = "\\translationLanguages.jsonl";

                TranslationLanguage tableInfoTranslationLanguage = new TranslationLanguage();
                TableInfo translationLanguageTable = DatabaseManager.GetTableInfo<TranslationLanguage>(tableInfoTranslationLanguage);
                DatabaseManager.CreateTable(translationLanguageTable);

                Console.WriteLine("Converting Trnslation Languages");
                count = ConvertFileForType<TranslationLanguage>(translationLanguageTable,
                                                    path);

            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Trnslation Languages Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Trnslation Languages into DB records");
            return success;
        }
        #endregion

        #region "Type Bonus"
        private bool ConvertTypeBonuses()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = SDEPath + "\\typeBonus.jsonl";

                TypeBonus tableInfoTypeBonus = new TypeBonus();
                TableInfo typeBonusTableInfo = DatabaseManager.GetTableInfo<TypeBonus>(tableInfoTypeBonus);
                DatabaseManager.CreateTable(typeBonusTableInfo);

                LanguageDescription tableInfoLangDscr = new LanguageDescription();
                TableInfo bonusTextTableInfo = DatabaseManager.GetTableInfo(tableInfoLangDscr);
                bonusTextTableInfo.Name = "TypeBonusBonusText";
                DatabaseManager.CreateTable(bonusTextTableInfo);

                Console.WriteLine("Converting Type Bonuses");
                count = ConvertTypeBonusesFromJSON(path,
                                                    typeBonusTableInfo,
                                                    bonusTextTableInfo);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Type Bonuses Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Type Bonuses ");
            return success;
        }

        private int ConvertTypeBonusesFromJSON(string path,
                                            TableInfo typeBonusTableInfo,
                                            TableInfo bonusTextTableInfo)
        {
            int count = 0;
            TypeBonus newTypeBonus = null;
            TypeBonus bonusToInsert = null;
            List<TypeBonus> batchBonus = new List<TypeBonus>();
            List<LanguageDescription> batchText = new List<LanguageDescription>();
            string json = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        json = sr.ReadLine();
                        newTypeBonus = Newtonsoft.Json.JsonConvert.DeserializeObject<TypeBonus>(json);

                        if (newTypeBonus.roleBonuses != null)
                        {
                            foreach (TypeBonusRoleBonus roleBonus in newTypeBonus.roleBonuses)
                            {
                                bonusToInsert = new TypeBonus();
                                bonusToInsert.typeBonusID = newTypeBonus.typeBonusID;
                                bonusToInsert.bonus = roleBonus.bonus;
                                bonusToInsert.bonusText = roleBonus.bonusText;
                                bonusToInsert.importance = roleBonus.importance;
                                bonusToInsert.unitID = roleBonus.unitID;
                                bonusToInsert.isRoleBonus = true;
                                Utility.AddRecordToBatch<TypeBonus>(typeBonusTableInfo, ref batchBonus, bonusToInsert);
                            }
                        }
                        if (newTypeBonus.types != null)
                        {
                            foreach (TypeBonus type in newTypeBonus.types)
                            {
                                foreach (TypeBonusRoleBonus value in type.values)
                                {
                                    bonusToInsert = new TypeBonus();
                                    bonusToInsert.typeBonusID = newTypeBonus.typeBonusID;
                                    bonusToInsert.typeID = type.typeBonusID;
                                    bonusToInsert.bonus = value.bonus;
                                    bonusToInsert.bonusText = value.bonusText;
                                    bonusToInsert.importance = value.importance;
                                    bonusToInsert.unitID = value.unitID;
                                    Utility.AddRecordToBatch<TypeBonus>(typeBonusTableInfo, ref batchBonus, bonusToInsert);
                                }
                            }
                        }

                        if (newTypeBonus.bonusText != null)
                        {
                            newTypeBonus.bonusText.parentTypeId = newTypeBonus.typeBonusID;
                            Utility.AddRecordToBatch<LanguageDescription>(bonusTextTableInfo, ref batchText, newTypeBonus.bonusText);
                        }

                        count++;
                    }
                }
                Utility.InsertBatchRecord<TypeBonus>(typeBonusTableInfo, batchBonus);
                Utility.InsertBatchRecord<LanguageDescription>(bonusTextTableInfo, batchText);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertTypeBonusesFromJSON");
            }

            return count;
        }
        #endregion

        #region "Type Dogmas"
        private bool ConvertTypeDogmas()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = SDEPath + "\\typeDogma.jsonl";

                TypeDogmaAttribute tableInfoTypeDogmaAttribute = new TypeDogmaAttribute();
                TableInfo typeDogmaAttributeTable = DatabaseManager.GetTableInfo<TypeDogmaAttribute>(tableInfoTypeDogmaAttribute);
                DatabaseManager.CreateTable(typeDogmaAttributeTable);

                TypeDogmaEffect tableInfoTypeDogmaEffect = new TypeDogmaEffect();
                TableInfo typeDogmaEffectTable = DatabaseManager.GetTableInfo<TypeDogmaEffect>(tableInfoTypeDogmaEffect);
                DatabaseManager.CreateTable(typeDogmaEffectTable);

                Console.WriteLine("Converting Type Dogmas");
                count = ConvertTypeDogmasFromJSON(path,
                                                    typeDogmaAttributeTable,
                                                    typeDogmaEffectTable);

            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Type Dogmas Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Type Dogmas into DB records");
            return success;
        }

        private int ConvertTypeDogmasFromJSON(string path,
                                            TableInfo typeDogmaAttributeTable,
                                            TableInfo typeDogmaEffectTable)
        {
            int count = 0;
            TypeDogma newTypeDogma = null;
            List<TypeDogmaAttribute> batchAttributes = new List<TypeDogmaAttribute>();
            List<TypeDogmaEffect> batchEffects = new List<TypeDogmaEffect>();
            string json = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        json = sr.ReadLine();
                        newTypeDogma = Newtonsoft.Json.JsonConvert.DeserializeObject<TypeDogma>(json);

                        if (newTypeDogma.dogmaAttributes?.Count > 0)
                        {
                            foreach (TypeDogmaAttribute typeDogmaAttribute in newTypeDogma.dogmaAttributes)
                            {
                                typeDogmaAttribute.typeID = newTypeDogma.typeID;
                                Utility.AddRecordToBatch<TypeDogmaAttribute>(typeDogmaAttributeTable, ref batchAttributes, typeDogmaAttribute);
                            }
                        }

                        if (newTypeDogma.dogmaEffects?.Count > 0)
                        {
                            foreach (TypeDogmaEffect typeDogmaEffect in newTypeDogma.dogmaEffects)
                            {
                                typeDogmaEffect.typeID = newTypeDogma.typeID;
                                Utility.AddRecordToBatch<TypeDogmaEffect>(typeDogmaEffectTable, ref batchEffects, typeDogmaEffect);
                            }
                        }
                        count++;
                    }
                    Utility.InsertBatchRecord<TypeDogmaAttribute>(typeDogmaAttributeTable, batchAttributes);
                    Utility.InsertBatchRecord<TypeDogmaEffect>(typeDogmaEffectTable, batchEffects);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertTypeDogmasFromJSON");
            }

            return count;
        }
        #endregion

        #region "Type Materials"
        private bool ConvertTypeMaterials()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = SDEPath + "\\typeMaterials.jsonl";

                TypeMaterial tableInfoTypeMaterial = new TypeMaterial();
                TableInfo typeMaterialTable = DatabaseManager.GetTableInfo<TypeMaterial>(tableInfoTypeMaterial);
                DatabaseManager.CreateTable(typeMaterialTable);

                Console.WriteLine("Converting Type Materials");
                count = ConvertTypeMaterialsFromJSON(path,
                                                    typeMaterialTable);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Type Materials Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Type Materials into DB records");
            return success;
        }

        private int ConvertTypeMaterialsFromJSON(string path,
                                            TableInfo typeMaterialTable)
        {
            int count = 0;
            TypeMaterial newTypeMaterial = null;
            List<TypeMaterial> batchTypeMaterials = new List<TypeMaterial>();
            string json = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        json = sr.ReadLine();
                        newTypeMaterial = Newtonsoft.Json.JsonConvert.DeserializeObject<TypeMaterial>(json);

                        if (newTypeMaterial.materials?.Count > 0)
                        {
                            foreach (TypeMaterial material in newTypeMaterial.materials)
                            {
                                material.typeID = Convert.ToInt32(newTypeMaterial.typeID);
                                Utility.AddRecordToBatch<TypeMaterial>(typeMaterialTable, ref batchTypeMaterials, material);
                            }
                        }
                        count++;
                    }
                    Utility.InsertBatchRecord<TypeMaterial>(typeMaterialTable, batchTypeMaterials);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertTypeMaterialsFromJSON");
            }

            return count;
        }
        #endregion

        #region "Types"
        private bool ConvertTypes()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = SDEPath + "\\types.jsonl";

                EveType tableInfoEveType = new EveType();
                TableInfo eveTypeTable = DatabaseManager.GetTableInfo<EveType>(tableInfoEveType);
                DatabaseManager.CreateTable(eveTypeTable);

                LanguageDescription tableInfoLangDscr = new LanguageDescription();

                TableInfo descriptionTableInfo = DatabaseManager.GetTableInfo<LanguageDescription>(tableInfoLangDscr);
                descriptionTableInfo.Name = "EveTypeDescription";
                DatabaseManager.CreateTable(descriptionTableInfo);

                TableInfo nameTableInfo = DatabaseManager.GetTableInfo<LanguageDescription>(tableInfoLangDscr);
                nameTableInfo.Name = "EveTypeName";
                DatabaseManager.CreateTable(nameTableInfo);

                Console.WriteLine("Converting Types");
                count = ConvertTypesFromJSON(path,
                                                eveTypeTable,
                                                descriptionTableInfo,
                                                nameTableInfo);

            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Types Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Types");
            return success;
        }

        private int ConvertTypesFromJSON(string path,
                                            TableInfo eveTypeTable,
                                            TableInfo descriptionTableInfo,
                                            TableInfo nameTableInfo)
        {
            int count = 0;
            EveType newType = null;

            List<EveType> batchTypes = new List<EveType>();
            List<LanguageDescription> batchTypeDscr = new List<LanguageDescription>();
            List<LanguageDescription> batchTypeName = new List<LanguageDescription>();
            string json = "";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() >= 0)
                    {
                        json = sr.ReadLine();
                        newType = Newtonsoft.Json.JsonConvert.DeserializeObject<EveType>(json);
                        Utility.AddRecordToBatch<EveType>(eveTypeTable, ref batchTypes, newType);

                        if (newType.description != null)
                        {
                            newType.description.parentTypeId = newType.typeID;
                            Utility.AddRecordToBatch<LanguageDescription>(descriptionTableInfo, ref batchTypeDscr, newType.description);
                        }

                        if (newType.name != null)
                        {
                            newType.name.parentTypeId = newType.typeID;
                            Utility.AddRecordToBatch<LanguageDescription>(nameTableInfo, ref batchTypeName, newType.name);
                        }
                        count++;
                    }

                    Utility.InsertBatchRecord<EveType>(eveTypeTable, batchTypes);
                    Utility.InsertBatchRecord<LanguageDescription>(descriptionTableInfo, batchTypeDscr);
                    Utility.InsertBatchRecord<LanguageDescription>(descriptionTableInfo, batchTypeName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertTypesFromJSON");
            }

            return count;
        }
        #endregion
    }
}
