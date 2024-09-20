using EveStaticDataExportConverter.Classes.Database;
using EveStaticDataExportConverter.Classes.FSD;
using EveStaticDataExportConverter.Classes.FSD.Supporting_Classes;
using EveStaticDataExportConverter.Classes.FSD.Supporting_Classes.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter
{
    internal class FSDConverter
    {
        string fsdPath = "C:\\Users\\mrphi\\source\\repos\\EveStaticDataExportConverter\\EveOnline_StaticDataExport_Converter\\json_sde\\fsd";

        public bool ConvertFSD()
        {
            bool success = true;

            success = ConvertAgents();
            success &= ConvertAgentsInSpace();
            success &= ConvertAncestry();
            success &= ConvertBloodlines();
            success &= ConvertBlueprints();
            success &= ConvertCategories();
            success &= ConvertCertificates();
            success &= ConvertCharacterAttributes();
            success &= ConvertContrabandTypes();
            success &= ConvertControlTowerResources();
            success &= ConvertCorporationActivities();
            success &= ConvertDogmaAttributeCategories();
            success &= ConvertDogmaAttributes();
            success &= ConvertDogmaEffects();
            success &= ConvertFactions();
            success &= ConvertGraphics();
            success &= ConvertGroups();
            success &= ConvertIcons();
            success &= ConvertMarketGroups();
            success &= ConvertMetaGroups();
            success &= ConvertNPCCorpDivisions();
            success &= ConvertNPCCorps();
            success &= ConvertPlanetResources();
            success &= ConvertPlanetSchematics();
            success &= ConvertRaces();
            success &= ConvertResearchAgents();
            success &= ConvertSkinLicense();
            success &= ConvertSkinMaterials();
            success &= ConvertSkins();
            success &= ConvertSovereigntyUpgrades();
            success &= ConvertStationOperations();
            success &= ConvertStationServices();
            success &= ConvertTournamentRuleSets();
            success &= ConvertTranslationLanguages();
            success &= ConvertTypeDogmas();
            success &= ConvertTypeMaterials();
            success &= ConvertTypes();
            return success;
        }

        #region "Agents"
        private bool ConvertAgents()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = fsdPath + "\\agents.json";
                string json = File.ReadAllText(path);

                Agent tableInfoAgent = new Agent();
                TableInfo agentTableInfo = DatabaseManager.GetTableInfo<Agent>(tableInfoAgent);
                DatabaseManager.CreateTable(agentTableInfo);

                Console.WriteLine("Converting Agents");
                count = ConvertAgentsFromJSON(json, agentTableInfo);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Agents Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Agents");
            return success;
        }

        private int ConvertAgentsFromJSON(string json, TableInfo agentTableInfo)
        {
            int count = 0;
            Agent newAgent;
            JObject agentArray = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);
            List<Agent> batchAgents = new List<Agent>();

            if (agentArray != null)
            {
                foreach (JToken token in agentArray.Children())
                {
                    newAgent = Newtonsoft.Json.JsonConvert.DeserializeObject<Agent>(token.First.ToString());
                    newAgent.agentId = Convert.ToInt64(token.Path);
                    Utility.AddRecordToBatch<Agent>(agentTableInfo, ref batchAgents, newAgent);
                    count++;
                }
                Utility.InsertBatchRecord<Agent>(agentTableInfo, batchAgents);
            }
            return count;
        }
        #endregion

        #region "AgentsInSpace"
        private bool ConvertAgentsInSpace()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = fsdPath + "\\agentsInSpace.json";
                string json = File.ReadAllText(path);

                AgentInSpace tableInfoAgentInSpace = new AgentInSpace();
                TableInfo agentsInSpaceTable = DatabaseManager.GetTableInfo<AgentInSpace>(tableInfoAgentInSpace);
                DatabaseManager.CreateTable(agentsInSpaceTable);

                Console.WriteLine("Converting Agents in space");
                count = ConvertAgentsInSpaceFromJson(json, agentsInSpaceTable);
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

        private int ConvertAgentsInSpaceFromJson(string json, TableInfo agentTableInfo)
        {
            int count = 0;
            AgentInSpace newAgent;
            JObject agentArray = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);

            if (agentArray != null)
            {
                foreach (JToken token in agentArray.Children())
                {
                    newAgent = Newtonsoft.Json.JsonConvert.DeserializeObject<AgentInSpace>(token.First.ToString());
                    newAgent.agentInSpaceId = Convert.ToInt64(token.Path);
                    DatabaseManager.InsertRecordForType<AgentInSpace>(agentTableInfo, newAgent);
                    count++;
                }
            }
            return count;
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
                string path = fsdPath + "\\ancestries.json";
                string json = File.ReadAllText(path);

                Ancestry tableInfoAncestry = new Ancestry();
                TableInfo ancestryTable = DatabaseManager.GetTableInfo<Ancestry>(tableInfoAncestry);

                DatabaseManager.CreateTable(ancestryTable);
                LanguageDescription tableInfoLD = new LanguageDescription();
                TableInfo langugateDscrTableInfo = DatabaseManager.GetTableInfo(tableInfoLD);

                string dscrTable = "AncestryDescription";
                langugateDscrTableInfo.Name = dscrTable;
                DatabaseManager.CreateTable(langugateDscrTableInfo);

                string shortDscrTable = "AncestryShortDescription";
                langugateDscrTableInfo.Name = shortDscrTable;
                DatabaseManager.CreateTable(langugateDscrTableInfo);

                Console.WriteLine("Converting Ancestries");
                count = ConvertAncestryFromJson(json, ancestryTable, langugateDscrTableInfo, dscrTable, shortDscrTable);
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

        private int ConvertAncestryFromJson(string json,
                                            TableInfo ancestryTableInfo,
                                            TableInfo languageDscrTableInfo,
                                            string dscrTableName,
                                            string shortDscrTableName)
        {
            int count = 0;
            Ancestry newAncestry;
            JObject agentArray = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);

            if (agentArray != null)
            {
                foreach (JToken token in agentArray.Children())
                {
                    newAncestry = Newtonsoft.Json.JsonConvert.DeserializeObject<Ancestry>(token.First.ToString());
                    newAncestry.ancestryID = Convert.ToInt32(token.Path);
                    DatabaseManager.InsertRecordForType<Ancestry>(ancestryTableInfo, newAncestry);

                    languageDscrTableInfo.Name = dscrTableName;
                    newAncestry.descriptionID.parentTypeId = newAncestry.ancestryID;
                    DatabaseManager.InsertRecordForType<LanguageDescription>(languageDscrTableInfo, newAncestry.descriptionID);

                    languageDscrTableInfo.Name = shortDscrTableName;
                    newAncestry.nameID.parentTypeId = newAncestry.ancestryID;
                    DatabaseManager.InsertRecordForType<LanguageDescription>(languageDscrTableInfo, newAncestry.nameID);

                    count++;
                }
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
                string path = fsdPath + "\\bloodlines.json";
                string json = File.ReadAllText(path);

                Bloodlines tableInfoBloodlines = new Bloodlines();
                TableInfo bloodLinesTable = DatabaseManager.GetTableInfo<Bloodlines>(tableInfoBloodlines);
                DatabaseManager.CreateTable(bloodLinesTable);

                LanguageDescription tableInfoLD = new LanguageDescription();
                TableInfo langugateDscrTableInfo = DatabaseManager.GetTableInfo(tableInfoLD);

                string dscrTable = "BloodlinesDescription";
                langugateDscrTableInfo.Name = dscrTable;
                DatabaseManager.CreateTable(langugateDscrTableInfo);

                string shortDscrTable = "BloodlinesShortDescription";
                langugateDscrTableInfo.Name = shortDscrTable;
                DatabaseManager.CreateTable(langugateDscrTableInfo);

                Console.WriteLine("Converting Bloodlines");
                count = ConvertBloodlinesFromJson(json, bloodLinesTable, langugateDscrTableInfo, dscrTable, shortDscrTable);
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

        private int ConvertBloodlinesFromJson(string json,
                                            TableInfo bloodlinesTableInfo,
                                            TableInfo languageDscrTableInfo,
                                            string dscrTableName,
                                            string shortDscrTableName)
        {
            int count = 0;
            Bloodlines newBloodline;
            JObject agentArray = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);

            if (agentArray != null)
            {
                foreach (JToken token in agentArray.Children())
                {
                    newBloodline = Newtonsoft.Json.JsonConvert.DeserializeObject<Bloodlines>(token.First.ToString());
                    newBloodline.bloodlinesID = Convert.ToInt32(token.Path);
                    DatabaseManager.InsertRecordForType<Bloodlines>(bloodlinesTableInfo, newBloodline);

                    languageDscrTableInfo.Name = dscrTableName;
                    newBloodline.descriptionID.parentTypeId = newBloodline.bloodlinesID;
                    DatabaseManager.InsertRecordForType<LanguageDescription>(languageDscrTableInfo, newBloodline.descriptionID);

                    languageDscrTableInfo.Name = shortDscrTableName;
                    newBloodline.nameID.parentTypeId = newBloodline.bloodlinesID;
                    DatabaseManager.InsertRecordForType<LanguageDescription>(languageDscrTableInfo, newBloodline.nameID);

                    count++;
                }
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
                string path = fsdPath + "\\blueprints.json";
                string json = File.ReadAllText(path);

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
                 
                count = ConvertBlueprintsFromJSON(json,
                                                    blueprintsTable,
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

        private int ConvertBlueprintsFromJSON(string json,
                                              TableInfo blueprintsTable,
                                              TableInfo bpActivityTypeTable,
                                              TableInfo bpActivityMatTable,
                                              TableInfo bpProductTable,
                                              TableInfo bpActivitySkillTable)
        {
            int count = 0;
            Blueprints newBlueprint = null;
            JObject bpArray = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);
            List<Blueprints> batchBlureprints = new List<Blueprints>();
            List<BlueprintActivityType> batchActivityTypes = new List<BlueprintActivityType>();
            List<BlueprintActivityMaterial> batchMaterials = new List<BlueprintActivityMaterial>();
            List<BlueprintProduct> batchProducts = new List<BlueprintProduct>();
            List<BlueprintSkill> batchSkills = new List<BlueprintSkill>();
           
            try
            {
                if (bpArray != null)
                {
                    foreach (JToken token in bpArray.Children())
                    {
                        newBlueprint = Newtonsoft.Json.JsonConvert.DeserializeObject<Blueprints>(token.First.ToString());
                        newBlueprint.blueprintTypeID = Convert.ToInt32(token.Path);
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
                    }
                    Utility.InsertBatchRecord<Blueprints>(blueprintsTable, batchBlureprints);
                    Utility.InsertBatchRecord<BlueprintActivityType>(bpActivityTypeTable, batchActivityTypes);
                    Utility.InsertBatchRecord<BlueprintActivityMaterial>(bpActivityMatTable, batchMaterials);
                    Utility.InsertBatchRecord<BlueprintProduct>(bpProductTable, batchProducts);
                    Utility.InsertBatchRecord<BlueprintSkill>(bpActivitySkillTable, batchSkills);
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
                string path = fsdPath + "\\categories.json";
                string json = File.ReadAllText(path);

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
                 
                count = ConvertCategoriesFromJSON(json,
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

        private int ConvertCategoriesFromJSON(string json, TableInfo categoryTableIndo, TableInfo langTableInfo)
        {
            int count = 0;
            Category newCategory = null;
            JObject categoryArray = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);

            try
            {
                if (categoryArray != null)
                {
                    foreach (JToken token in categoryArray.Children())
                    {
                        newCategory = Newtonsoft.Json.JsonConvert.DeserializeObject<Category>(token.First.ToString());
                        newCategory.categoryID = Convert.ToInt32(token.Path);
                        DatabaseManager.InsertRecordForType<Category>(categoryTableIndo, newCategory);


                        newCategory.name.parentTypeId = newCategory.categoryID;
                        DatabaseManager.InsertRecordForType<LanguageDescription>(langTableInfo, newCategory.name);

                        count++;
                    }
                }
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
                string path = fsdPath + "\\certificates.json";
                string json = File.ReadAllText(path);


                Certificate tableInfoCertificate = new Certificate();
                TableInfo certificateTable = DatabaseManager.GetTableInfo<Certificate>(tableInfoCertificate);
                DatabaseManager.CreateTable(certificateTable);

                CertificateSkillType tableInfoCertificateSkillType = new CertificateSkillType();
                TableInfo certificateSkillTypTableInfo = DatabaseManager.GetTableInfo<CertificateSkillType>(tableInfoCertificateSkillType);
                DatabaseManager.CreateTable(certificateSkillTypTableInfo);

                CertificateRecommendedForType tableInfoCertificateRecommended = new CertificateRecommendedForType();
                TableInfo certificateRecommendedTableInfo = DatabaseManager.GetTableInfo<CertificateRecommendedForType>(tableInfoCertificateRecommended);
                DatabaseManager.CreateTable(certificateRecommendedTableInfo);

                Console.WriteLine("Converting Certificates");
                 
                count = ConvertCertificatesFromJSON(json,
                                                   certificateTable,
                                                   certificateSkillTypTableInfo,
                                                   certificateRecommendedTableInfo);
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

        private int ConvertCertificatesFromJSON(string json,
                                                TableInfo certificateTable,
                                                TableInfo certificateSkillTypTableInfo,
                                                TableInfo certificateRecommendedForTypeTableInfo)
        {
            int count = 0;
            Certificate newCertificate = null;
            CertificateRecommendedForType certificateRecommendedFor = null;
            JObject certificateArray = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);

            try
            {
                if (certificateArray != null)
                {
                    foreach (JToken token in certificateArray.Children())
                    {
                        newCertificate = Newtonsoft.Json.JsonConvert.DeserializeObject<Certificate>(token.First.ToString());
                        newCertificate.certificateID = Convert.ToInt32(token.Path);
                        DatabaseManager.InsertRecordForType<Certificate>(certificateTable, newCertificate);


                        foreach (int typeId in newCertificate.recommendedFor)
                        {
                            certificateRecommendedFor = new CertificateRecommendedForType()
                            { certificateID = newCertificate.certificateID, recommendedForTypeID = typeId };
                            DatabaseManager.InsertRecordForType<CertificateRecommendedForType>(certificateRecommendedForTypeTableInfo,
                                                                                              certificateRecommendedFor);
                        }

                        if (newCertificate.skillTypes?.Count > 0)
                        {
                            foreach (CertificateSkillType skillType in newCertificate.skillTypes)
                            {
                                skillType.certificateID = newCertificate.certificateID;
                                DatabaseManager.InsertRecordForType<CertificateSkillType>(certificateSkillTypTableInfo, skillType);
                            }
                        }

                        count++;
                    }
                }
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
                string path = fsdPath + "\\characterAttributes.json";
                string json = File.ReadAllText(path);


                CharacterAttribute tableInfoCharacterAttribute = new CharacterAttribute();
                TableInfo charAttributeTable = DatabaseManager.GetTableInfo<CharacterAttribute>(tableInfoCharacterAttribute);
                DatabaseManager.CreateTable(charAttributeTable);

                LanguageDescription tableInfoLangDescript = new LanguageDescription();
                TableInfo langDescriptTableInfo = DatabaseManager.GetTableInfo<LanguageDescription>(tableInfoLangDescript);
                DatabaseManager.CreateTable(langDescriptTableInfo);

                Console.WriteLine("Converting Character Attributes");
                 
                count = ConvertCharacterAttributesFromJSON(json,
                                                                charAttributeTable,
                                                                langDescriptTableInfo);
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

        private int ConvertCharacterAttributesFromJSON(string json,
                                                        TableInfo charAttributeTable,
                                                        TableInfo langTableInfo)
        {
            int count = 0;
            CharacterAttribute newCharAttribute = null;
            JObject characterAttributeArray = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);

            try
            {
                if (characterAttributeArray != null)
                {
                    foreach (JToken token in characterAttributeArray.Children())
                    {
                        newCharAttribute = Newtonsoft.Json.JsonConvert.DeserializeObject<CharacterAttribute>(token.First.ToString());
                        newCharAttribute.characterAttributeID = Convert.ToInt32(token.Path);
                        DatabaseManager.InsertRecordForType<CharacterAttribute>(charAttributeTable, newCharAttribute);

                        newCharAttribute.nameID.parentTypeId = newCharAttribute.characterAttributeID;
                        DatabaseManager.InsertRecordForType<LanguageDescription>(langTableInfo, newCharAttribute.nameID);

                        count++;
                    }
                }
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
                string path = fsdPath + "\\contrabandTypes.json";
                string json = File.ReadAllText(path);


                ContrabandType tableInfoContrabandType = new ContrabandType();
                TableInfo contrabandTypeTable = DatabaseManager.GetTableInfo<ContrabandType>(tableInfoContrabandType);
                DatabaseManager.CreateTable(contrabandTypeTable);

                Console.WriteLine("Converting Contraband Types");
                 
                count = ConvertContrabandTypesFromJSON(json,
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

        private int ConvertContrabandTypesFromJSON(string json,
                                                        TableInfo contrabandTypeTable)
        {
            int count = 0;
            ContrabandType newContrabandType = null;
            JObject contrabandTypeArray = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);

            try
            {
                if (contrabandTypeArray != null)
                {
                    foreach (JToken token in contrabandTypeArray.Children())
                    {
                        newContrabandType = Newtonsoft.Json.JsonConvert.DeserializeObject<ContrabandType>(token.First.ToString());
                        newContrabandType.contrabandTypeID = Convert.ToInt32(token.Path);

                        if (newContrabandType.factions?.Count > 0)
                        {
                            foreach (ContrabandType childType in newContrabandType.factions)
                            {
                                childType.contrabandTypeID = newContrabandType.contrabandTypeID;
                                DatabaseManager.InsertRecordForType<ContrabandType>(contrabandTypeTable, childType);
                                count++;
                            }
                        }
                    }
                }
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
                string path = fsdPath + "\\controlTowerResources.json";
                string json = File.ReadAllText(path);


                ControlTowerResource tableInfoControlTowerResource = new ControlTowerResource();
                TableInfo controlTowerResourceTable = DatabaseManager.GetTableInfo<ControlTowerResource>(tableInfoControlTowerResource);
                DatabaseManager.CreateTable(controlTowerResourceTable);
                ;
                Console.WriteLine("Converting Control Tower Resources");
                 
                count = ConvertControlTowerResourcesFromJSON(json,
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

        private int ConvertControlTowerResourcesFromJSON(string json,
                                                        TableInfo controlTowerResourceTable)
        {
            int count = 0;
            ControlTowerResource newControlTowerResource = null;
            JObject controlTowerResourceArray = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);

            try
            {
                if (controlTowerResourceArray != null)
                {
                    foreach (JToken token in controlTowerResourceArray.Children())
                    {
                        newControlTowerResource = Newtonsoft.Json.JsonConvert.DeserializeObject<ControlTowerResource>(token.First.ToString());
                        newControlTowerResource.controlTowerResourceID = Convert.ToInt32(token.Path);

                        if (newControlTowerResource.resources?.Count > 0)
                        {
                            foreach (ControlTowerResource childType in newControlTowerResource.resources)
                            {
                                childType.controlTowerResourceID = newControlTowerResource.controlTowerResourceID;
                                DatabaseManager.InsertRecordForType<ControlTowerResource>(controlTowerResourceTable, childType);
                                count++;
                            }
                        }
                    }
                }
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
                string path = fsdPath + "\\corporationActivities.json";
                string json = File.ReadAllText(path);


                LanguageDescription tableInfoLanguageDescript = new LanguageDescription();
                TableInfo langDescripTable = DatabaseManager.GetTableInfo<LanguageDescription>(tableInfoLanguageDescript);
                langDescripTable.Name = "CorporationActivity";
                DatabaseManager.CreateTable(langDescripTable);

                Console.WriteLine("Converting Corporation Activities");
                 
                count = ConvertCorporationActivitiesFromJSON(json,
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

        private int ConvertCorporationActivitiesFromJSON(string json,
                                                        TableInfo languageDescriptTable)
        {
            int count = 0;
            CorporationActivity newCorporationActivity = null;
            JObject corpActivityArray = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);

            try
            {
                if (corpActivityArray != null)
                {
                    foreach (JToken token in corpActivityArray.Children())
                    {
                        newCorporationActivity = Newtonsoft.Json.JsonConvert.DeserializeObject<CorporationActivity>(token.First.ToString());
                        newCorporationActivity.corporationActivityID = Convert.ToInt32(token.Path);

                        if (newCorporationActivity.nameID != null)
                        {
                            newCorporationActivity.nameID.parentTypeId = newCorporationActivity.corporationActivityID;
                            DatabaseManager.InsertRecordForType<LanguageDescription>(languageDescriptTable, newCorporationActivity.nameID);
                            count++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertCorporationActivitiesFromJSON");
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
                string path = fsdPath + "\\dogmaAttributeCategories.json";
                string json = File.ReadAllText(path);


                DogmaAttributeCategory tableInfoDogmaAttrCat = new DogmaAttributeCategory();
                TableInfo dogmaAttrCatTable = DatabaseManager.GetTableInfo<DogmaAttributeCategory>(tableInfoDogmaAttrCat);
                DatabaseManager.CreateTable(dogmaAttrCatTable);

                Console.WriteLine("Converting Dogma Attribute Categories");
                 
                count = ConvertDogmaAttributeCategoriesFromJSON(json,
                                                                    dogmaAttrCatTable);
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

        private int ConvertDogmaAttributeCategoriesFromJSON(string json,
                                                        TableInfo dogmaAttrCatTable)
        {
            int count = 0;
            DogmaAttributeCategory newDogmaAttributeCategory = null;
            JObject corpActivityArray = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);

            try
            {
                if (corpActivityArray != null)
                {
                    foreach (JToken token in corpActivityArray.Children())
                    {
                        newDogmaAttributeCategory = Newtonsoft.Json.JsonConvert.DeserializeObject<DogmaAttributeCategory>(token.First.ToString());
                        newDogmaAttributeCategory.dogmaAttributeCategoryID = Convert.ToInt32(token.Path);
                        DatabaseManager.InsertRecordForType<DogmaAttributeCategory>(dogmaAttrCatTable, newDogmaAttributeCategory);
                        count++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertDogmaAttributeCategoriesFromJSON");
            }

            return count;
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
                string path = fsdPath + "\\dogmaAttributes.json";
                string json = File.ReadAllText(path);


                DogmaAttribute tableInfoDogmaAttr = new DogmaAttribute();
                TableInfo dogmaAttrTable = DatabaseManager.GetTableInfo<DogmaAttribute>(tableInfoDogmaAttr);
                DatabaseManager.CreateTable(dogmaAttrTable);

                LanguageDescription tableInfoLangDscr = new LanguageDescription();

                TableInfo languageDescipriotnTableInfo = DatabaseManager.GetTableInfo(tableInfoLangDscr);
                languageDescipriotnTableInfo.Name = "DogmaAttributeDisplayName";
                DatabaseManager.CreateTable(languageDescipriotnTableInfo);

                languageDescipriotnTableInfo.Name = "DogmaAttributToolTipDescription";
                DatabaseManager.CreateTable(languageDescipriotnTableInfo);

                languageDescipriotnTableInfo.Name = "DogmaAttributToolTipTitle";
                DatabaseManager.CreateTable(languageDescipriotnTableInfo);

                Console.WriteLine("Converting Dogma Attributes");
                 
                count = ConvertDogmaAttributesFromJSON(json,
                                                           dogmaAttrTable,
                                                           languageDescipriotnTableInfo,
                                                           "DogmaAttributeDisplayName",
                                                           "DogmaAttributToolTipDescription",
                                                           "DogmaAttributToolTipTitle");

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

        private int ConvertDogmaAttributesFromJSON(string json,
                                                        TableInfo dogmaAttrCatTable,
                                                        TableInfo languageDscrTable,
                                                        string dogmaAttrDscrTableName,
                                                        string dogmaAttrToolTipeDscrName,
                                                        string dogmaAttrToolTipTitleName)
        {
            int count = 0;
            DogmaAttribute newDogmaAttribute = null;
            JObject corpActivityArray = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);

            try
            {
                if (corpActivityArray != null)
                {
                    foreach (JToken token in corpActivityArray.Children())
                    {
                        newDogmaAttribute = Newtonsoft.Json.JsonConvert.DeserializeObject<DogmaAttribute>(token.First.ToString());
                        DatabaseManager.InsertRecordForType<DogmaAttribute>(dogmaAttrCatTable, newDogmaAttribute);
                        count++;

                        if (newDogmaAttribute.displayNameID != null)
                        {
                            newDogmaAttribute.displayNameID.parentTypeId = newDogmaAttribute.attributeID;
                            languageDscrTable.Name = dogmaAttrDscrTableName;
                            DatabaseManager.InsertRecordForType<LanguageDescription>(languageDscrTable, newDogmaAttribute.displayNameID);
                        }

                        if (newDogmaAttribute.tooltipDescriptionID != null)
                        {
                            newDogmaAttribute.tooltipDescriptionID.parentTypeId = newDogmaAttribute.attributeID;
                            languageDscrTable.Name = dogmaAttrToolTipeDscrName;
                            DatabaseManager.InsertRecordForType<LanguageDescription>(languageDscrTable, newDogmaAttribute.tooltipDescriptionID);
                        }

                        if (newDogmaAttribute.tooltipTitleID != null)
                        {
                            newDogmaAttribute.tooltipTitleID.parentTypeId = newDogmaAttribute.attributeID;
                            languageDscrTable.Name = dogmaAttrToolTipTitleName;
                            DatabaseManager.InsertRecordForType<LanguageDescription>(languageDscrTable, newDogmaAttribute.tooltipTitleID);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertDogmaAttributeCategoriesFromJSON");
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
                string path = fsdPath + "\\dogmaEffects.json";
                string json = File.ReadAllText(path);


                DogmaEffect tableInfoDogmaEffect = new DogmaEffect();
                TableInfo dogmaEffectTable = DatabaseManager.GetTableInfo<DogmaEffect>(tableInfoDogmaEffect);
                DatabaseManager.CreateTable(dogmaEffectTable);

                LanguageDescription tableInfoLangDscr = new LanguageDescription();
                TableInfo languageDescipriotnTableInfo = DatabaseManager.GetTableInfo(tableInfoLangDscr);

                languageDescipriotnTableInfo.Name = "DogmaEffectDescription";
                DatabaseManager.CreateTable(languageDescipriotnTableInfo);

                languageDescipriotnTableInfo.Name = "DogmaEffectDisplayName";
                DatabaseManager.CreateTable(languageDescipriotnTableInfo);

                Console.WriteLine("Converting Dogma Effects");
                 
                count = ConvertDogmaEffectsFromJSON(json,
                                                        dogmaEffectTable,
                                                        languageDescipriotnTableInfo,
                                                        "DogmaEffectDescription",
                                                        "DogmaEffectDisplayName");
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

        private int ConvertDogmaEffectsFromJSON(string json,
                                                        TableInfo dogmaAttrCatTable,
                                                        TableInfo languageDscrTable,
                                                        string dogmaEffectDescriptionTableName,
                                                        string dogmaEffectDisplayNameTableName)
        {
            int count = 0;
            DogmaEffect newDogmaEffect = null;
            JObject corpActivityArray = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);

            try
            {
                if (corpActivityArray != null)
                {
                    foreach (JToken token in corpActivityArray.Children())
                    {
                        newDogmaEffect = Newtonsoft.Json.JsonConvert.DeserializeObject<DogmaEffect>(token.First.ToString());
                        newDogmaEffect.dogmaEffectID = newDogmaEffect.effectID;
                        DatabaseManager.InsertRecordForType<DogmaEffect>(dogmaAttrCatTable, newDogmaEffect);
                        count++;

                        if (newDogmaEffect.displayNameID != null)
                        {
                            newDogmaEffect.displayNameID.parentTypeId = newDogmaEffect.dogmaEffectID;
                            languageDscrTable.Name = dogmaEffectDescriptionTableName;
                            DatabaseManager.InsertRecordForType<LanguageDescription>(languageDscrTable, newDogmaEffect.displayNameID);
                        }

                        if (newDogmaEffect.descriptionID != null)
                        {
                            newDogmaEffect.descriptionID.parentTypeId = newDogmaEffect.dogmaEffectID;
                            languageDscrTable.Name = dogmaEffectDisplayNameTableName;
                            DatabaseManager.InsertRecordForType<LanguageDescription>(languageDscrTable, newDogmaEffect.descriptionID);
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

        #region "Factions"
        private bool ConvertFactions()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = fsdPath + "\\factions.json";
                string json = File.ReadAllText(path);


                Faction tableInfoFaction = new Faction();
                TableInfo factionTable = DatabaseManager.GetTableInfo<Faction>(tableInfoFaction);
                DatabaseManager.CreateTable(factionTable);

                FactionRaces tableInfoFactionRaces = new FactionRaces();
                TableInfo factionRacesTable = DatabaseManager.GetTableInfo<FactionRaces>(tableInfoFactionRaces);
                DatabaseManager.CreateTable(factionRacesTable);

                LanguageDescription tableInfoLangDscr = new LanguageDescription();
                TableInfo languageDescipriotnTableInfo = DatabaseManager.GetTableInfo(tableInfoLangDscr);

                languageDescipriotnTableInfo.Name = "FactionDescription";
                DatabaseManager.CreateTable(languageDescipriotnTableInfo);

                languageDescipriotnTableInfo.Name = "FactionName";
                DatabaseManager.CreateTable(languageDescipriotnTableInfo);

                languageDescipriotnTableInfo.Name = "FactionShortDescription";
                DatabaseManager.CreateTable(languageDescipriotnTableInfo);

                Console.WriteLine("Converting Factions");
                 
                count = ConvertFactionsFromJSON(json,
                                                    factionTable,
                                                    factionRacesTable,
                                                    languageDescipriotnTableInfo,
                                                    "FactionDescription",
                                                    "FactionName",
                                                    "FactionShortDescription");
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

        private int ConvertFactionsFromJSON(string json,
                                            TableInfo factionTable,
                                            TableInfo factionRacesTable,
                                            TableInfo languageDscrTable,
                                            string factionDscrTableName,
                                            string factionNameTableName,
                                            string factionShortDscrTableName)
        {
            int count = 0;
            Faction newFaction = null;
            FactionRaces newFactionRaces = null;
            JObject corpActivityArray = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);

            try
            {
                if (corpActivityArray != null)
                {
                    foreach (JToken token in corpActivityArray.Children())
                    {
                        newFaction = Newtonsoft.Json.JsonConvert.DeserializeObject<Faction>(token.First.ToString());
                        newFaction.factionID = Convert.ToInt64(token.Path);
                        DatabaseManager.InsertRecordForType<Faction>(factionTable, newFaction);

                        if (newFaction.memberRaces?.Count > 0)
                        {
                            foreach (int memberRace in newFaction.memberRaces)
                            {
                                newFactionRaces = new FactionRaces();
                                newFactionRaces.factionID = newFaction.factionID;
                                newFactionRaces.raceID = memberRace;
                                DatabaseManager.InsertRecordForType<FactionRaces>(factionRacesTable, newFactionRaces);
                            }
                        }

                        if (newFaction.descriptionID != null)
                        {
                            newFaction.descriptionID.parentTypeId = newFaction.factionID;
                            languageDscrTable.Name = factionDscrTableName;
                            DatabaseManager.InsertRecordForType<LanguageDescription>(languageDscrTable, newFaction.descriptionID);
                        }

                        if (newFaction.nameID != null)
                        {
                            newFaction.nameID.parentTypeId = newFaction.factionID;
                            languageDscrTable.Name = factionNameTableName;
                            DatabaseManager.InsertRecordForType<LanguageDescription>(languageDscrTable, newFaction.nameID);
                        }

                        if (newFaction.shortDescriptionID != null)
                        {
                            newFaction.shortDescriptionID.parentTypeId = newFaction.factionID;
                            languageDscrTable.Name = factionShortDscrTableName;
                            DatabaseManager.InsertRecordForType<LanguageDescription>(languageDscrTable, newFaction.shortDescriptionID);
                        }

                        count++;
                    }
                }
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
                string path = fsdPath + "\\graphicIDs.json";
                string json = File.ReadAllText(path);


                Graphic tableInfoFaction = new Graphic();
                TableInfo graphicTable = DatabaseManager.GetTableInfo<Graphic>(tableInfoFaction);
                DatabaseManager.CreateTable(graphicTable);

                Console.WriteLine("Converting Graphics");
                 
                count = ConvertGraphicsFromJSON(json,
                                                graphicTable);
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

        private int ConvertGraphicsFromJSON(string json,
                                            TableInfo graphicTable)
        {
            int count = 0;
            Graphic newGraphic = null;
            JObject jObject = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);

            try
            {
                if (jObject != null)
                {
                    foreach (JToken token in jObject.Children())
                    {
                        newGraphic = Newtonsoft.Json.JsonConvert.DeserializeObject<Graphic>(token.First.ToString());
                        newGraphic.graphicID = Convert.ToInt32(token.Path);

                        if (newGraphic.iconInfo != null)
                        {
                            newGraphic.iconFolder = newGraphic.iconInfo.folder;
                        }

                        DatabaseManager.InsertRecordForType<Graphic>(graphicTable, newGraphic);


                        count++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertGraphicsFromJSON");
            }

            return count;
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
                string path = fsdPath + "\\groups.json";
                string json = File.ReadAllText(path);


                EveGroup tableInfoGroup = new EveGroup();
                TableInfo groupTable = DatabaseManager.GetTableInfo<EveGroup>(tableInfoGroup);
                DatabaseManager.CreateTable(groupTable);

                LanguageDescription tableInfoLangDscr = new LanguageDescription();
                TableInfo languageDescipriotnTableInfo = DatabaseManager.GetTableInfo(tableInfoLangDscr);
                languageDescipriotnTableInfo.Name = "GroupName";
                DatabaseManager.CreateTable(languageDescipriotnTableInfo);

                Console.WriteLine("Converting Groups");
                 
                count = ConvertGroupsFromJSON(json,
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

        private int ConvertGroupsFromJSON(string json,
                                            TableInfo groupTable,
                                            TableInfo langDscrTable)
        {
            int count = 0;
            EveGroup newGroup = null;
            JObject jObject = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);

            try
            {
                if (jObject != null)
                {
                    foreach (JToken token in jObject.Children())
                    {
                        newGroup = Newtonsoft.Json.JsonConvert.DeserializeObject<EveGroup>(token.First.ToString());
                        newGroup.groupID = Convert.ToInt32(token.Path);
                        DatabaseManager.InsertRecordForType<EveGroup>(groupTable, newGroup);

                        if (newGroup.name != null)
                        {
                            newGroup.name.parentTypeId = newGroup.groupID;
                            DatabaseManager.InsertRecordForType<LanguageDescription>(langDscrTable, newGroup.name);
                        }

                        count++;
                    }
                }
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
                string path = fsdPath + "\\iconIDs.json";
                string json = File.ReadAllText(path);


                Icon tableInfoIcon = new Icon();
                TableInfo iconTable = DatabaseManager.GetTableInfo<Icon>(tableInfoIcon);
                DatabaseManager.CreateTable(iconTable);

                Console.WriteLine("Converting Icons");
                 
                count = ConvertIconsFromJSON(json,
                                                iconTable);
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

        private int ConvertIconsFromJSON(string json,
                                            TableInfo iconTable)
        {
            int count = 0;
            Icon newIcon = null;
            JObject jObject = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);

            try
            {
                if (jObject != null)
                {
                    foreach (JToken token in jObject.Children())
                    {
                        newIcon = Newtonsoft.Json.JsonConvert.DeserializeObject<Icon>(token.First.ToString());
                        newIcon.iconID = Convert.ToInt32(token.Path);
                        DatabaseManager.InsertRecordForType<Icon>(iconTable, newIcon);

                        count++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertGroupsFromJSON");
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
                string path = fsdPath + "\\marketGroups.json";
                string json = File.ReadAllText(path);


                MarketGroup tableInfoGroup = new MarketGroup();
                TableInfo marketGroupTable = DatabaseManager.GetTableInfo<MarketGroup>(tableInfoGroup);
                DatabaseManager.CreateTable(marketGroupTable);

                LanguageDescription tableInfoLangDscr = new LanguageDescription();
                TableInfo languageDescipriotnTableInfo = DatabaseManager.GetTableInfo(tableInfoLangDscr);

                languageDescipriotnTableInfo.Name = "MarketGroupDescription";
                DatabaseManager.CreateTable(languageDescipriotnTableInfo);

                languageDescipriotnTableInfo.Name = "MarketGroupName";
                DatabaseManager.CreateTable(languageDescipriotnTableInfo);

                Console.WriteLine("Converting Market Groups");
                 
                count = ConvertMarketGroupsromJSON(json,
                                                    marketGroupTable,
                                                    languageDescipriotnTableInfo,
                                                    "MarketGroupDescription",
                                                    "MarketGroupName");
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

        private int ConvertMarketGroupsromJSON(string json,
                                            TableInfo marketGroupTable,
                                            TableInfo langDscrTable,
                                            string dscrTablName,
                                            string nameTableName)
        {
            int count = 0;
            MarketGroup newGroup = null;
            JObject jObject = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);

            try
            {
                if (jObject != null)
                {
                    foreach (JToken token in jObject.Children())
                    {
                        newGroup = Newtonsoft.Json.JsonConvert.DeserializeObject<MarketGroup>(token.First.ToString());
                        newGroup.marketGroupId = Convert.ToInt32(token.Path);
                        DatabaseManager.InsertRecordForType<MarketGroup>(marketGroupTable, newGroup);

                        if (newGroup.nameID != null)
                        {
                            newGroup.nameID.parentTypeId = newGroup.marketGroupId;
                            langDscrTable.Name = nameTableName;
                            DatabaseManager.InsertRecordForType<LanguageDescription>(langDscrTable, newGroup.nameID);
                        }

                        if (newGroup.descriptionID != null)
                        {
                            newGroup.descriptionID.parentTypeId = newGroup.marketGroupId;
                            langDscrTable.Name = dscrTablName;
                            DatabaseManager.InsertRecordForType<LanguageDescription>(langDscrTable, newGroup.descriptionID);
                        }

                        count++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertGroupsFromJSON");
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
                string path = fsdPath + "\\metaGroups.json";
                string json = File.ReadAllText(path);


                MetaGroup tableInfoGroup = new MetaGroup();
                TableInfo metaGroupTable = DatabaseManager.GetTableInfo<MetaGroup>(tableInfoGroup);
                DatabaseManager.CreateTable(metaGroupTable);

                LanguageDescription tableInfoLangDscr = new LanguageDescription();
                TableInfo languageDescipriotnTableInfo = DatabaseManager.GetTableInfo(tableInfoLangDscr);

                languageDescipriotnTableInfo.Name = "MetaGroupName";
                DatabaseManager.CreateTable(languageDescipriotnTableInfo);

                Console.WriteLine("Converting Meta Groups");
                 
                count = ConvertMetaGroupsFromJSON(json,
                                                    metaGroupTable,
                                                    languageDescipriotnTableInfo);
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

        private int ConvertMetaGroupsFromJSON(string json,
                                            TableInfo metaGroupTable,
                                            TableInfo langDscrTable)
        {
            int count = 0;
            MetaGroup newGroup = null;
            JObject jObject = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);

            try
            {
                if (jObject != null)
                {
                    foreach (JToken token in jObject.Children())
                    {
                        newGroup = Newtonsoft.Json.JsonConvert.DeserializeObject<MetaGroup>(token.First.ToString());
                        newGroup.metaGroupID = Convert.ToInt32(token.Path);
                        DatabaseManager.InsertRecordForType<MetaGroup>(metaGroupTable, newGroup);

                        if (newGroup.nameID != null)
                        {
                            newGroup.nameID.parentTypeId = newGroup.metaGroupID;
                            DatabaseManager.InsertRecordForType<LanguageDescription>(langDscrTable, newGroup.nameID);
                        }

                        count++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertMetaGroupsFromJSON");
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
                string path = fsdPath + "\\npcCorporationDivisions.json";
                string json = File.ReadAllText(path);


                NPCCorporationDivision tableInfoCorpDivision = new NPCCorporationDivision();
                TableInfo corpDivisionTable = DatabaseManager.GetTableInfo<NPCCorporationDivision>(tableInfoCorpDivision);
                DatabaseManager.CreateTable(corpDivisionTable);

                LanguageDescription tableInfoLangDscr = new LanguageDescription();
                TableInfo languageDescipriotnTableInfo = DatabaseManager.GetTableInfo(tableInfoLangDscr);

                languageDescipriotnTableInfo.Name = "NPCCorporationDivisionLeaderTypeName";
                DatabaseManager.CreateTable(languageDescipriotnTableInfo);

                languageDescipriotnTableInfo.Name = "NPCCorporationDivisionName";
                DatabaseManager.CreateTable(languageDescipriotnTableInfo);

                Console.WriteLine("Converting NPC Corp Divisions");
                 
                count = ConvertNPCCorpDivisionsFromJSON(json,
                                                    corpDivisionTable,
                                                    languageDescipriotnTableInfo,
                                                    "NPCCorporationDivisionLeaderTypeName",
                                                    "NPCCorporationDivisionName");
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

        private int ConvertNPCCorpDivisionsFromJSON(string json,
                                            TableInfo npcCorpDivisionTable,
                                            TableInfo langDscrTable,
                                            string corpDivisionLeaderTypeNameTableName,
                                            string nameTableName)
        {
            int count = 0;
            NPCCorporationDivision newNPCCorpDivision = null;
            JObject jObject = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);

            try
            {
                if (jObject != null)
                {
                    foreach (JToken token in jObject.Children())
                    {
                        newNPCCorpDivision = Newtonsoft.Json.JsonConvert.DeserializeObject<NPCCorporationDivision>(token.First.ToString());
                        newNPCCorpDivision.npcCorporationDivisionID = Convert.ToInt32(token.Path);
                        DatabaseManager.InsertRecordForType<NPCCorporationDivision>(npcCorpDivisionTable, newNPCCorpDivision);

                        if (newNPCCorpDivision.nameID != null)
                        {
                            newNPCCorpDivision.nameID.parentTypeId = newNPCCorpDivision.npcCorporationDivisionID;
                            langDscrTable.Name = nameTableName;
                            DatabaseManager.InsertRecordForType<LanguageDescription>(langDscrTable, newNPCCorpDivision.nameID);
                        }

                        if (newNPCCorpDivision.leaderTypeNameID != null)
                        {
                            newNPCCorpDivision.leaderTypeNameID.parentTypeId = newNPCCorpDivision.npcCorporationDivisionID;
                            langDscrTable.Name = corpDivisionLeaderTypeNameTableName;
                            DatabaseManager.InsertRecordForType<LanguageDescription>(langDscrTable, newNPCCorpDivision.leaderTypeNameID);
                        }

                        count++;
                    }
                }
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
                string path = fsdPath + "\\npcCorporations.json";
                string json = File.ReadAllText(path);


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
                TableInfo langDscrTable = DatabaseManager.GetTableInfo<LanguageDescription>(tableInfoLangDscr);

                langDscrTable.Name = "NPCCorporationDescription";
                DatabaseManager.CreateTable(langDscrTable);

                NPCCorporationCorpDivision tableInfoNPCCorpDivision = new NPCCorporationCorpDivision();
                TableInfo npcCorpDivisionTable = DatabaseManager.GetTableInfo<NPCCorporationCorpDivision>(tableInfoNPCCorpDivision);
                DatabaseManager.CreateTable(npcCorpDivisionTable);

                NPCCorpLPOfferTable tableInfoNPCCorplpTable = new NPCCorpLPOfferTable();
                TableInfo npcCorpLPTable = DatabaseManager.GetTableInfo<NPCCorpLPOfferTable>(tableInfoNPCCorplpTable);
                DatabaseManager.CreateTable(npcCorpLPTable);

                langDscrTable.Name = "NPCCorporationName";
                DatabaseManager.CreateTable(langDscrTable);

                Console.WriteLine("Converting NPC Corporations");
                 
                count = ConvertNPCCorpsFromJSON(json,
                                                    npcCorpTable,
                                                    npcCorpAllowedRacesTable,
                                                    npcCorpTradesTable,
                                                    langDscrTable,
                                                    npcCorpDivisionTable,
                                                    npcCorpLPTable,
                                                    "NPCCorporationDescription",
                                                    "NPCCorporationName");
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

        private int ConvertNPCCorpsFromJSON(string json,
                                            TableInfo npcCorpTable,
                                            TableInfo npcCorpAllowedRacesTable,
                                            TableInfo npcCorpTradesTable,
                                            TableInfo langDscrTable,
                                            TableInfo npcCorpDivisionTable,
                                            TableInfo npcCorpLPTable,
                                            string dscrTableName,
                                            string nameTableName)
        {
            int count = 0;
            NPCCorporation newNPCCorp = null;
            NPCCorpAllowedRace newNPCCorpAllowedRace = null;
            NPCCorpLPOfferTable newNPCCorpLPOfferTable = null;
            JObject jObject = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);

            try
            {
                if (jObject != null)
                {
                    foreach (JToken token in jObject.Children())
                    {
                        newNPCCorp = Newtonsoft.Json.JsonConvert.DeserializeObject<NPCCorporation>(token.First.ToString());
                        newNPCCorp.npcCorporationID = Convert.ToInt32(token.Path);
                        DatabaseManager.InsertRecordForType<NPCCorporation>(npcCorpTable, newNPCCorp);

                        if (newNPCCorp.nameID != null)
                        {
                            newNPCCorp.nameID.parentTypeId = newNPCCorp.npcCorporationID;
                            langDscrTable.Name = nameTableName;
                            DatabaseManager.InsertRecordForType<LanguageDescription>(langDscrTable, newNPCCorp.nameID);
                        }

                        if (newNPCCorp.descriptionID != null)
                        {
                            newNPCCorp.descriptionID.parentTypeId = newNPCCorp.npcCorporationID;
                            langDscrTable.Name = dscrTableName;
                            DatabaseManager.InsertRecordForType<LanguageDescription>(langDscrTable, newNPCCorp.descriptionID);
                        }

                        if (newNPCCorp.allowedMemberRaces?.Count > 0)
                        {
                            foreach (int raceID in newNPCCorp.allowedMemberRaces)
                            {
                                newNPCCorpAllowedRace = new NPCCorpAllowedRace() { npcCorporationID = newNPCCorp.npcCorporationID, raceID = raceID };
                                DatabaseManager.InsertRecordForType<NPCCorpAllowedRace>(npcCorpAllowedRacesTable, newNPCCorpAllowedRace);
                            }
                        }

                        if (newNPCCorp.corporationTrades?.Count > 0)
                        {
                            foreach (CorporationTrade corporationTrade in newNPCCorp.corporationTrades)
                            {
                                corporationTrade.npcCorporationID = newNPCCorp.npcCorporationID;
                                DatabaseManager.InsertRecordForType<CorporationTrade>(npcCorpTradesTable, corporationTrade);
                            }
                        }

                        if (newNPCCorp.divisions?.Count > 0)
                        {
                            foreach (NPCCorporationCorpDivision division in newNPCCorp.divisions)
                            {
                                division.npcCorporationID = newNPCCorp.npcCorporationID;
                                DatabaseManager.InsertRecordForType<NPCCorporationCorpDivision>(npcCorpDivisionTable, division);
                            }
                        }

                        if (newNPCCorp.lpOfferTables?.Count > 0)
                        {
                            foreach (int offerID in newNPCCorp.lpOfferTables)
                            {
                                newNPCCorpLPOfferTable = new NPCCorpLPOfferTable();
                                newNPCCorpLPOfferTable.npcCorporationID = newNPCCorp.npcCorporationID;
                                newNPCCorpLPOfferTable.lpOfferTableID = offerID;
                                DatabaseManager.InsertRecordForType<NPCCorpLPOfferTable>(npcCorpLPTable, newNPCCorpLPOfferTable);
                            }
                        }
                        count++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertNPCCorpDivisionsFromJSON");
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
                string path = fsdPath + "\\planetResources.json";
                string json = File.ReadAllText(path);


                PlanetResource tableInfoPlanetResource = new PlanetResource();
                TableInfo planetResource = DatabaseManager.GetTableInfo<PlanetResource>(tableInfoPlanetResource);
                DatabaseManager.CreateTable(planetResource);

                Console.WriteLine("Converting Planet Resources");
                 
                count = ConvertPlanetResourcesFromJSON(json,
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

        private int ConvertPlanetResourcesFromJSON(string json,
                                            TableInfo planetResourcesTable)
        {
            int count = 0;
            PlanetResource newPlanetResource = null;
            JObject jObject = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);
            List<PlanetResource> batchPlanetResources = new List<PlanetResource>();

            try
            {
                if (jObject != null)
                {
                    foreach (JToken token in jObject.Children())
                    {
                        newPlanetResource = Newtonsoft.Json.JsonConvert.DeserializeObject<PlanetResource>(token.First.ToString());
                        newPlanetResource.planetID = Convert.ToInt64(token.Path);
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
                string path = fsdPath + "\\planetSchematics.json";
                string json = File.ReadAllText(path);


                PlanetSchematic tableInfoPlanetSchematic = new PlanetSchematic();
                TableInfo planetSchematicTable = DatabaseManager.GetTableInfo<PlanetSchematic>(tableInfoPlanetSchematic);
                DatabaseManager.CreateTable(planetSchematicTable);

                LanguageDescription tableInfoLangDscr = new LanguageDescription();
                TableInfo langDscrTableInfo = DatabaseManager.GetTableInfo(tableInfoLangDscr);
                langDscrTableInfo.Name = "PlanetSchematicName";
                DatabaseManager.CreateTable(langDscrTableInfo);

                PlanetSchematicPin tableInfoPlanetSchematicPin = new PlanetSchematicPin();
                TableInfo planetSchematicPinTable = DatabaseManager.GetTableInfo<PlanetSchematicPin>(tableInfoPlanetSchematicPin);
                DatabaseManager.CreateTable(planetSchematicPinTable);

                PlanetSchematicType tableInfoPlanetSchematicType = new PlanetSchematicType();
                TableInfo planetSchematicTypeTable = DatabaseManager.GetTableInfo<PlanetSchematicType>(tableInfoPlanetSchematicType);
                DatabaseManager.CreateTable(planetSchematicTypeTable);

                Console.WriteLine("Converting Planet Schematics");
                 
                count = ConvertPlanetSchematicsFromJSON(json,
                                                    planetSchematicTable,
                                                    langDscrTableInfo,
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

        private int ConvertPlanetSchematicsFromJSON(string json,
                                            TableInfo planetSchematicTable,
                                            TableInfo langDscrTable,
                                            TableInfo planetSchematicPinTable,
                                            TableInfo planetSchematicTypeTable)
        {
            int count = 0;
            PlanetSchematic newPlanetSchematic = null;
            PlanetSchematicPin newplanetSchematicPin = null;
            JObject jObject = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);

            try
            {
                if (jObject != null)
                {
                    foreach (JToken token in jObject.Children())
                    {
                        newPlanetSchematic = Newtonsoft.Json.JsonConvert.DeserializeObject<PlanetSchematic>(token.First.ToString());
                        newPlanetSchematic.planetSchematicID = Convert.ToInt32(token.Path);
                        DatabaseManager.InsertRecordForType<PlanetSchematic>(planetSchematicTable, newPlanetSchematic);

                        if (newPlanetSchematic.nameID != null)
                        {
                            newPlanetSchematic.nameID.parentTypeId = newPlanetSchematic.planetSchematicID;
                            DatabaseManager.InsertRecordForType<LanguageDescription>(langDscrTable, newPlanetSchematic.nameID);
                        }

                        if (newPlanetSchematic.pins?.Count > 0)
                        {
                            foreach (int pinId in newPlanetSchematic.pins)
                            {
                                newplanetSchematicPin = new PlanetSchematicPin();
                                newplanetSchematicPin.planetSchematicID = newPlanetSchematic.planetSchematicID;
                                newplanetSchematicPin.pinID = pinId;
                                DatabaseManager.InsertRecordForType<PlanetSchematicPin>(planetSchematicPinTable, newplanetSchematicPin);
                            }
                        }

                        if (newPlanetSchematic.types?.Count > 0)
                        {
                            foreach (PlanetSchematicType planetSchematicType in newPlanetSchematic.types)
                            {
                                planetSchematicType.planetSchematicID = newPlanetSchematic.planetSchematicID;
                                DatabaseManager.InsertRecordForType<PlanetSchematicType>(planetSchematicTypeTable, planetSchematicType);
                            }
                        }
                        count++;
                    }
                }
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
                string path = fsdPath + "\\races.json";
                string json = File.ReadAllText(path);


                Race tableInfoRace = new Race();
                TableInfo raceTable = DatabaseManager.GetTableInfo<Race>(tableInfoRace);
                DatabaseManager.CreateTable(raceTable);

                LanguageDescription tableInfoLangDscr = new LanguageDescription();
                TableInfo langDscrTableInfo = DatabaseManager.GetTableInfo(tableInfoLangDscr);

                langDscrTableInfo.Name = "RaceDescription";
                DatabaseManager.CreateTable(langDscrTableInfo);

                langDscrTableInfo.Name = "RaceName";
                DatabaseManager.CreateTable(langDscrTableInfo);

                RaceSkill tableInfoRaceSkill = new RaceSkill();
                TableInfo raceSkillTable = DatabaseManager.GetTableInfo<RaceSkill>(tableInfoRaceSkill);
                DatabaseManager.CreateTable(raceSkillTable);

                Console.WriteLine("Converting Races");
                 
                count = ConvertRacesFromJSON(json,
                                                raceTable,
                                                langDscrTableInfo,
                                                raceSkillTable,
                                                "RaceDescription",
                                                "RaceName");
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

        private int ConvertRacesFromJSON(string json,
                                            TableInfo raceTable,
                                            TableInfo langDscrTableInfo,
                                            TableInfo raceSkillTable,
                                            string dscrTableName,
                                            string nameTableName)
        {
            int count = 0;
            Race newRace = null;
            PlanetSchematicPin newplanetSchematicPin = null;
            JObject jObject = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);

            try
            {
                if (jObject != null)
                {
                    foreach (JToken token in jObject.Children())
                    {
                        newRace = Newtonsoft.Json.JsonConvert.DeserializeObject<Race>(token.First.ToString());
                        newRace.raceID = Convert.ToInt32(token.Path);
                        DatabaseManager.InsertRecordForType<Race>(raceTable, newRace);

                        if (newRace.nameID != null)
                        {
                            newRace.nameID.parentTypeId = newRace.raceID;
                            langDscrTableInfo.Name = nameTableName;
                            DatabaseManager.InsertRecordForType<LanguageDescription>(langDscrTableInfo, newRace.nameID);
                        }

                        if (newRace.descriptionID != null)
                        {
                            newRace.descriptionID.parentTypeId = newRace.raceID;
                            langDscrTableInfo.Name = dscrTableName;
                            DatabaseManager.InsertRecordForType<LanguageDescription>(langDscrTableInfo, newRace.descriptionID);
                        }

                        if (newRace.skills?.Count > 0)
                        {
                            foreach (RaceSkill skill in newRace.skills)
                            {
                                skill.raceID = newRace.raceID;
                                DatabaseManager.InsertRecordForType<RaceSkill>(raceSkillTable, skill);
                            }
                        }

                        count++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertRacesFromJSON");
            }

            return count;
        }
        #endregion

        #region "Research Agents"
        private bool ConvertResearchAgents()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = fsdPath + "\\researchAgents.json";
                string json = File.ReadAllText(path);


                ResearchAgent tableInfoResearchAgent = new ResearchAgent();
                TableInfo researchAgentTable = DatabaseManager.GetTableInfo<ResearchAgent>(tableInfoResearchAgent);
                DatabaseManager.CreateTable(researchAgentTable);

                Console.WriteLine("Converting Reserch Agents");
                 
                count = ConvertResearchAgentsFromJSON(json,
                                                    researchAgentTable);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Reserch Agents Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Reserch Agents ");
            return success;
        }

        private int ConvertResearchAgentsFromJSON(string json,
                                            TableInfo researchAgentTable)
        {
            int count = 0;
            ResearchAgent newResearchAgent = null;
            JObject jObject = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);

            try
            {
                if (jObject != null)
                {
                    foreach (JToken token in jObject.Children())
                    {
                        newResearchAgent = Newtonsoft.Json.JsonConvert.DeserializeObject<ResearchAgent>(token.First.ToString());
                        newResearchAgent.researchAgentID = Convert.ToInt32(token.Path);

                        if (newResearchAgent.skills?.Count > 0)
                        {
                            foreach (ResearchAgentSkillType skill in newResearchAgent.skills)
                            {
                                newResearchAgent.skillTypeID = skill.typeID;
                                DatabaseManager.InsertRecordForType<ResearchAgent>(researchAgentTable, newResearchAgent);
                                count++;
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertResearchAgentsFromJSON");
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
                string path = fsdPath + "\\skinLicenses.json";
                string json = File.ReadAllText(path);


                SkinLicense tableInfoSkinLicense = new SkinLicense();
                TableInfo skinLicenseTable = DatabaseManager.GetTableInfo<SkinLicense>(tableInfoSkinLicense);
                DatabaseManager.CreateTable(skinLicenseTable);

                Console.WriteLine("Converting Skin License");
                 
                count = ConvertSkinLicenseFromJSON(json,
                                                    skinLicenseTable);
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

        private int ConvertSkinLicenseFromJSON(string json,
                                            TableInfo skinLicenseTable)
        {
            int count = 0;
            SkinMaterial newSkinMaterial = null;
            JObject jObject = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);

            try
            {
                if (jObject != null)
                {
                    foreach (JToken token in jObject.Children())
                    {
                        newSkinMaterial = Newtonsoft.Json.JsonConvert.DeserializeObject<SkinMaterial>(token.First.ToString());
                        DatabaseManager.InsertRecordForType<SkinMaterial>(skinLicenseTable, newSkinMaterial);
                        count++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertSkinLicenseFromJSON");
            }

            return count;
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
                string path = fsdPath + "\\skinMaterials.json";
                string json = File.ReadAllText(path);


                SkinMaterial tableInfoSkinMaterial = new SkinMaterial();
                TableInfo skinMaterialTable = DatabaseManager.GetTableInfo<SkinMaterial>(tableInfoSkinMaterial);
                DatabaseManager.CreateTable(skinMaterialTable);

                Console.WriteLine("Converting Skin Materials");
                count = ConvertSkinLicenseFromJSON(json,
                                                    skinMaterialTable);
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

        private int ConvertSkinMaterialsFromJSON(string json,
                                            TableInfo skinLicenseTable)
        {
            int count = 0;
            SkinLicense newSkinLicense = null;
            JObject jObject = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);

            try
            {
                if (jObject != null)
                {
                    foreach (JToken token in jObject.Children())
                    {
                        newSkinLicense = Newtonsoft.Json.JsonConvert.DeserializeObject<SkinLicense>(token.First.ToString());
                        newSkinLicense.skinLicenseID = Convert.ToInt32(token.Path);
                        DatabaseManager.InsertRecordForType<SkinLicense>(skinLicenseTable, newSkinLicense);
                        count++;
                    }
                }
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
                string path = fsdPath + "\\skins.json";
                string json = File.ReadAllText(path);


                Skin tableInfoSkin = new Skin();
                TableInfo skinTable = DatabaseManager.GetTableInfo<Skin>(tableInfoSkin);
                DatabaseManager.CreateTable(skinTable);

                SkinType tableInfoSkinType = new SkinType();
                TableInfo skinTypelTable = DatabaseManager.GetTableInfo<SkinType>(tableInfoSkinType);
                DatabaseManager.CreateTable(skinTypelTable);

                Console.WriteLine("Converting Skins");
                count = ConvertSkinsFromJSON(json,
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

        private int ConvertSkinsFromJSON(string json,
                                            TableInfo skinLicenseTable,
                                            TableInfo skinTypeTable)
        {
            int count = 0;
            Skin newSkin = null;
            SkinType newSkinType = null;
            JObject jObject = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);
            List<Skin> batchSkins = new List<Skin>();
            List<SkinType> batchSkinTypes = new List<SkinType>();

            try
            {
                if (jObject != null)
                {
                    foreach (JToken token in jObject.Children())
                    {
                        newSkin = Newtonsoft.Json.JsonConvert.DeserializeObject<Skin>(token.First.ToString());
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
                string path = fsdPath + "\\sovereigntyUpgrades.json";
                string json = File.ReadAllText(path);


                SovereigntyUpgrade tableInfoSovereigntyUpgrade = new SovereigntyUpgrade();
                TableInfo sovereigntyUpgradeTable = DatabaseManager.GetTableInfo<SovereigntyUpgrade>(tableInfoSovereigntyUpgrade);
                DatabaseManager.CreateTable(sovereigntyUpgradeTable);

                Console.WriteLine("Converting Sovereignty Upgrades");
                count = ConvertSovereigntyUpgradesFromJSON(json,
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

        private int ConvertSovereigntyUpgradesFromJSON(string json,
                                            TableInfo sovUpgradeTable)
        {
            int count = 0;
            SovereigntyUpgrade newSovUpgrade = null;
            JObject jObject = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);

            try
            {
                if (jObject != null)
                {
                    foreach (JToken token in jObject.Children())
                    {
                        newSovUpgrade = Newtonsoft.Json.JsonConvert.DeserializeObject<SovereigntyUpgrade>(token.First.ToString());
                        newSovUpgrade.sovereigntyUpgradeID = Convert.ToInt32(token.Path);
                        DatabaseManager.InsertRecordForType<SovereigntyUpgrade>(sovUpgradeTable, newSovUpgrade);

                        count++;
                    }
                }
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
                string path = fsdPath + "\\stationOperations.json";
                string json = File.ReadAllText(path);


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
                TableInfo langDscrTable = DatabaseManager.GetTableInfo<LanguageDescription>(tableInfoLangDscr);

                langDscrTable.Name = "StationOperationName";
                DatabaseManager.CreateTable(langDscrTable);

                langDscrTable.Name = "StationOperationDescription";
                DatabaseManager.CreateTable(langDscrTable);

                Console.WriteLine("Converting Station Operations");
                count = ConvertStationOperationsFromJSON(json,
                                                    stationOperationTable,
                                                    stationOperationServiceTable,
                                                    stationTypeTable,
                                                    langDscrTable,
                                                    "StationOperationDescription",
                                                    "StationOperationName");
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

        private int ConvertStationOperationsFromJSON(string json,
                                            TableInfo stationOperationTable,
                                            TableInfo stationOperationServiceTable,
                                            TableInfo stationTypeTable,
                                            TableInfo langDscrTable,
                                            string dscrTableName,
                                            string nameTableName)
        {
            int count = 0;
            StationOperation newStationOperation = null;
            JObject jObject = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);

            try
            {
                if (jObject != null)
                {
                    foreach (JToken token in jObject.Children())
                    {
                        newStationOperation = Newtonsoft.Json.JsonConvert.DeserializeObject<StationOperation>(token.First.ToString());
                        newStationOperation.stationOperationID = Convert.ToInt32(token.Path);
                        DatabaseManager.InsertRecordForType<StationOperation>(stationOperationTable, newStationOperation);

                        if (newStationOperation.operationNameID != null)
                        {
                            newStationOperation.operationNameID.parentTypeId = newStationOperation.stationOperationID;
                            langDscrTable.Name = nameTableName;
                            DatabaseManager.InsertRecordForType<LanguageDescription>(langDscrTable, newStationOperation.operationNameID);
                        }

                        if (newStationOperation.descriptionID != null)
                        {
                            newStationOperation.descriptionID.parentTypeId = newStationOperation.stationOperationID;
                            langDscrTable.Name = dscrTableName;
                            DatabaseManager.InsertRecordForType<LanguageDescription>(langDscrTable, newStationOperation.descriptionID);
                        }

                        if (newStationOperation.services?.Count > 0)
                        {
                            StationOperationService sto = new StationOperationService();
                            foreach (int service in newStationOperation.services)
                            {
                                sto = new StationOperationService();
                                sto.stationOperationID = newStationOperation.stationOperationID;
                                sto.stationServiceID = service;
                                DatabaseManager.InsertRecordForType<StationOperationService>(stationOperationServiceTable, sto);
                            }
                        }

                        if (newStationOperation.stationTypes?.Count > 0)
                        {
                            foreach (StationType stationType in newStationOperation.stationTypes)
                            {
                                stationType.stationOperationID = newStationOperation.stationOperationID;
                                DatabaseManager.InsertRecordForType<StationType>(stationTypeTable, stationType);
                            }
                        }

                        count++;
                    }
                }
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
                string path = fsdPath + "\\stationServices.json";
                string json = File.ReadAllText(path);

                LanguageDescription tableInfoLangDscr = new LanguageDescription();
                TableInfo langDscrTable = DatabaseManager.GetTableInfo<LanguageDescription>(tableInfoLangDscr);
                langDscrTable.Name = "StationService";
                DatabaseManager.CreateTable(langDscrTable);

                Console.WriteLine("Converting Station Services");
                count = ConvertStationServicesFromJSON(json,
                                                    langDscrTable);
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

        private int ConvertStationServicesFromJSON(string json,
                                            TableInfo langDscrTable)
        {
            int count = 0;
            StationService newStationService = null;
            JObject jObject = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);

            try
            {
                if (jObject != null)
                {
                    foreach (JToken token in jObject.Children())
                    {
                        newStationService = Newtonsoft.Json.JsonConvert.DeserializeObject<StationService>(token.First.ToString());
                        newStationService.stationServiceID = Convert.ToInt32(token.Path);

                        if (newStationService.serviceNameID != null)
                        {
                            newStationService.serviceNameID.parentTypeId = newStationService.stationServiceID;
                            DatabaseManager.InsertRecordForType<LanguageDescription>(langDscrTable, newStationService.serviceNameID);
                        }

                        count++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertStationServicesFromJSON");
            }

            return count;
        }
        #endregion

        #region "Tournament Rule Sets"
        private bool ConvertTournamentRuleSets()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            bool success = true;
            int count = 0;
            try
            {
                string path = fsdPath + "\\tournamentRuleSets.json";
                string json = File.ReadAllText(path);

                TournamentRuleSet tableInfoTournamentRuleSet = new TournamentRuleSet();
                TableInfo tournamentRuleSetTable = DatabaseManager.GetTableInfo<TournamentRuleSet>(tableInfoTournamentRuleSet);
                DatabaseManager.CreateTable(tournamentRuleSetTable);

                TournamentRuleSetBannedGroup tableInfoTournamentRuleSetBannedGroup = new TournamentRuleSetBannedGroup();
                TableInfo tournamentRuleSetBannedGroupTable = DatabaseManager.GetTableInfo<TournamentRuleSetBannedGroup>(tableInfoTournamentRuleSetBannedGroup);
                DatabaseManager.CreateTable(tournamentRuleSetBannedGroupTable);

                TournamentRuleSetBannedType tableInfoTournamentRuleSetBannedType = new TournamentRuleSetBannedType();
                TableInfo tournamentRuleSetBannedTypeTable = DatabaseManager.GetTableInfo<TournamentRuleSetBannedType>(tableInfoTournamentRuleSetBannedType);
                DatabaseManager.CreateTable(tournamentRuleSetBannedTypeTable);

                TournamentRuleSetPointsGroup tableInfoTournamentRuleSetPointsGroup = new TournamentRuleSetPointsGroup();
                TableInfo tournamentRuleSetPointsGroupTable = DatabaseManager.GetTableInfo<TournamentRuleSetPointsGroup>(tableInfoTournamentRuleSetPointsGroup);
                DatabaseManager.CreateTable(tournamentRuleSetPointsGroupTable);

                TournamentRuleSetPointsType tableInfoTournamentRuleSetPointsType = new TournamentRuleSetPointsType();
                TableInfo tournamentRuleSetPointsTypeTable = DatabaseManager.GetTableInfo<TournamentRuleSetPointsType>(tableInfoTournamentRuleSetPointsType);
                DatabaseManager.CreateTable(tournamentRuleSetPointsTypeTable);

                Console.WriteLine("Converting Tournament Rule Sets");
                count = ConvertTournamentRuleSetsFromJSON(json,
                                                    tournamentRuleSetTable,
                                                    tournamentRuleSetBannedGroupTable,
                                                    tournamentRuleSetBannedTypeTable,
                                                    tournamentRuleSetPointsGroupTable,
                                                    tournamentRuleSetPointsTypeTable);
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine("Converting Tournament Rule Sets Failed: Ex = " + ex.Message);
            }

            sw.Stop();
            Utility.LogElapsedTime(sw, "Converting " + count.ToString() + " Tournament Rule Sets ");
            return success;
        }

        private int ConvertTournamentRuleSetsFromJSON(string json,
                                            TableInfo tournamentRuleSetTable,
                                            TableInfo tournamentRuleSetBannedGroupTable,
                                            TableInfo tournamentRuleSetBannedTypeTable,
                                            TableInfo tournamentRuleSetPointsGroupTable,
                                            TableInfo tournamentRuleSetPointsTypeTable)
        {
            int count = 0;
            TournamentRuleSet newTournamentRuleSet = null;
            JArray jArray = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);

            try
            {
                if (jArray != null)
                {
                    foreach (JObject jObject in jArray)
                    {
                        newTournamentRuleSet = Newtonsoft.Json.JsonConvert.DeserializeObject<TournamentRuleSet>(jObject.ToString());
                        DatabaseManager.InsertRecordForType<TournamentRuleSet>(tournamentRuleSetTable, newTournamentRuleSet);

                        if (newTournamentRuleSet.banned != null)
                        {
                            if (newTournamentRuleSet.banned.groups?.Count > 0)
                            {
                                TournamentRuleSetBannedGroup newBannedGroup = null;
                                foreach (int bannedGroup in newTournamentRuleSet.banned.groups)
                                {
                                    newBannedGroup = new TournamentRuleSetBannedGroup();
                                    newBannedGroup.ruleSetID = newTournamentRuleSet.ruleSetID;
                                    newBannedGroup.groupID = bannedGroup;
                                    DatabaseManager.InsertRecordForType<TournamentRuleSetBannedGroup>(tournamentRuleSetBannedGroupTable, newBannedGroup);
                                }
                            }
                            if (newTournamentRuleSet.banned.types?.Count > 0)
                            {
                                TournamentRuleSetBannedType newBannedType = null;
                                foreach (int bannedType in newTournamentRuleSet.banned.types)
                                {
                                    newBannedType = new TournamentRuleSetBannedType();
                                    newBannedType.ruleSetID = newTournamentRuleSet.ruleSetID;
                                    newBannedType.typeID = bannedType;
                                    DatabaseManager.InsertRecordForType<TournamentRuleSetBannedType>(tournamentRuleSetBannedTypeTable, newBannedType);
                                }
                            }
                        }

                        if (newTournamentRuleSet.points != null)
                        {
                            if (newTournamentRuleSet.points.groups?.Count > 0)
                            {
                                foreach (TournamentRuleSetPointsGroup pointGroup in newTournamentRuleSet.points.groups)
                                {
                                    pointGroup.ruleSetID = newTournamentRuleSet.ruleSetID;
                                    DatabaseManager.InsertRecordForType<TournamentRuleSetPointsGroup>(tournamentRuleSetPointsGroupTable, pointGroup);
                                }
                            }
                            if (newTournamentRuleSet.points.types?.Count > 0)
                            {
                                foreach (TournamentRuleSetPointsType pointType in newTournamentRuleSet.points.types)
                                {
                                    pointType.ruleSetID = newTournamentRuleSet.ruleSetID;
                                    DatabaseManager.InsertRecordForType<TournamentRuleSetPointsType>(tournamentRuleSetPointsTypeTable, pointType);
                                }
                            }
                        }

                        count++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertTournamentRuleSetsFromJSON");
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
                string path = fsdPath + "\\translationLanguages.json";
                string json = File.ReadAllText(path);

                TranslationLanguage tableInfoTranslationLanguage = new TranslationLanguage();
                TableInfo translationLanguageTable = DatabaseManager.GetTableInfo<TranslationLanguage>(tableInfoTranslationLanguage);
                DatabaseManager.CreateTable(translationLanguageTable);

                Console.WriteLine("Converting Trnslation Languages");
                count = ConvertTranslationLanguagesFromJSON(json,
                                                    translationLanguageTable);

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

        private int ConvertTranslationLanguagesFromJSON(string json,
                                            TableInfo translationLanguageTable)
        {
            int count = 0;
            TranslationLanguage newTranslationLanguage = Newtonsoft.Json.JsonConvert.DeserializeObject<TranslationLanguage>(json);

            try
            {
                if (newTranslationLanguage != null)
                {
                    newTranslationLanguage.transalationLanguageID = 1;
                    DatabaseManager.InsertRecordForType<TranslationLanguage>(translationLanguageTable, newTranslationLanguage);
                    count += 1;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error ocurred in ConvertTranslationLanguagesFromJSON");
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
                string path = fsdPath + "\\typeDogma.json";
                string json = File.ReadAllText(path);

                TypeDogmaAttribute tableInfoTypeDogmaAttribute = new TypeDogmaAttribute();
                TableInfo typeDogmaAttributeTable = DatabaseManager.GetTableInfo<TypeDogmaAttribute>(tableInfoTypeDogmaAttribute);
                DatabaseManager.CreateTable(typeDogmaAttributeTable);

                TypeDogmaEffect tableInfoTypeDogmaEffect = new TypeDogmaEffect();
                TableInfo typeDogmaEffectTable = DatabaseManager.GetTableInfo<TypeDogmaEffect>(tableInfoTypeDogmaEffect);
                DatabaseManager.CreateTable(typeDogmaEffectTable);

                Console.WriteLine("Converting Type Dogmas");
                count = ConvertTypeDogmasFromJSON(json,
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

        private int ConvertTypeDogmasFromJSON(string json,
                                            TableInfo typeDogmaAttributeTable,
                                            TableInfo typeDogmaEffectTable)
        {
            int count = 0;
            JObject jObject = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);
            TypeDogma newTypeDogma = null;
            List<TypeDogmaAttribute> batchAttributes = new List<TypeDogmaAttribute>();
            List<TypeDogmaEffect> batchEffects = new List<TypeDogmaEffect>();

            try
            {
                if (jObject != null)
                {
                    List<JToken> allTokens = jObject.Children().ToList();

                    foreach (JToken token in allTokens)
                    {
                        newTypeDogma = Newtonsoft.Json.JsonConvert.DeserializeObject<TypeDogma>(token.First.ToString());
                        newTypeDogma.typeID = Convert.ToInt32(token.Path);


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
                string path = fsdPath + "\\typeMaterials.json";
                string json = File.ReadAllText(path);

                TypeMaterial tableInfoTypeMaterial = new TypeMaterial();
                TableInfo typeMaterialTable = DatabaseManager.GetTableInfo<TypeMaterial>(tableInfoTypeMaterial);
                DatabaseManager.CreateTable(typeMaterialTable);

                Console.WriteLine("Converting Type Materials");
                count = ConvertTypeMaterialsFromJSON(json,
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

        private int ConvertTypeMaterialsFromJSON(string json,
                                            TableInfo typeMaterialTable)
        {
            int count = 0;
            JObject jObject = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);
            TypeMaterial newTypeMaterial = null;
            List<TypeMaterial> batchTypeMaterials = new List<TypeMaterial>();

            try
            {
                if (jObject != null)
                {
                    foreach (JToken token in jObject.Children())
                    {
                        newTypeMaterial = Newtonsoft.Json.JsonConvert.DeserializeObject<TypeMaterial>(token.First.ToString());
                        newTypeMaterial.typeID = Convert.ToInt32(token.Path);

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
                Console.WriteLine("Error ocurred in ConvertStationServicesFromJSON");
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
                string path = fsdPath + "\\types.json";
                string json = File.ReadAllText(path);

                EveType tableInfoEveType = new EveType();
                TableInfo eveTypeTable = DatabaseManager.GetTableInfo<EveType>(tableInfoEveType);
                DatabaseManager.CreateTable(eveTypeTable);

                LanguageDescription tableInfoLangDscr = new LanguageDescription();
                TableInfo langDscrTable = DatabaseManager.GetTableInfo<LanguageDescription>(tableInfoLangDscr);

                langDscrTable.Name = "EveTypeDescription";
                DatabaseManager.CreateTable(langDscrTable);

                langDscrTable.Name = "EveTypeName";
                DatabaseManager.CreateTable(langDscrTable);

                EveTypeMastery tableInfoEveTypeMastery = new EveTypeMastery();
                TableInfo eveTypeMasteryTable = DatabaseManager.GetTableInfo<EveTypeMastery>(tableInfoEveTypeMastery);
                DatabaseManager.CreateTable(eveTypeMasteryTable);

                EveTypeBonus tableInfoEveTypeBonus = new EveTypeBonus();
                TableInfo eveTypeBonusTable = DatabaseManager.GetTableInfo<EveTypeBonus>(tableInfoEveTypeBonus);

                eveTypeBonusTable.Name = "EveTypeMiscBonuses";
                DatabaseManager.CreateTable(eveTypeBonusTable);

                eveTypeBonusTable.Name = "EveTypeRoleBonuses";
                DatabaseManager.CreateTable(eveTypeBonusTable);

                eveTypeBonusTable.Name = "EveTypeBonuses";
                DatabaseManager.CreateTable(eveTypeBonusTable);

                langDscrTable.Name = "EveTypeBonusText";
                DatabaseManager.CreateTable(langDscrTable);

                Console.WriteLine("Converting Types");
                count = ConvertTypesFromJSON(json,
                                                eveTypeTable,
                                                langDscrTable,
                                                eveTypeMasteryTable,
                                                eveTypeBonusTable,
                                                "EveTypeDescription",
                                                "EveTypeName",
                                                "EveTypeMiscBonuses",
                                                "EveTypeRoleBonuses",
                                                "EveTypeBonuses",
                                                "EveTypeBonusText");

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

        private int ConvertTypesFromJSON(string json,
                                            TableInfo eveTypeTable,
                                            TableInfo langDscrTable,
                                            TableInfo eveTypeMasteryTable,
                                            TableInfo eveTypeBonusTable,
                                            string eveTypeDscrTableName,
                                            string eveTypeNameTableName,
                                            string miscBonusTableName,
                                            string roleBonusTableName,
                                            string eveTypeBonusTableName,
                                            string eveTypeBonusTextTableName)
        {
            int count = 0;
            JObject jObject = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);
            EveType newType = null;

            List<EveType> batchTypes = new List<EveType>();
            List<LanguageDescription> batchTypeDscr = new List<LanguageDescription>();
            List<LanguageDescription> batchTypeName = new List<LanguageDescription>();
            List<EveTypeBonus> batchEveTypeMiscBonus = new List<EveTypeBonus>();
            List<LanguageDescription> batchEveTypeMiscBonusText = new List<LanguageDescription>();
            List<EveTypeBonus> batchEveTypeRoleBonus = new List<EveTypeBonus>();
            List<LanguageDescription> batchEveTypeRoleBonusText = new List<LanguageDescription>();
            List<EveTypeBonus> batchEveTypeTraitBonus = new List<EveTypeBonus>();
            List<LanguageDescription> batchEveTypeTraitBonusText = new List<LanguageDescription>();
            List<EveTypeMastery> batchMasteries = new List<EveTypeMastery>();

            try
            {
                if (jObject != null)
                {
                    List<JToken> allTokens = jObject.Children().ToList();

                    foreach (JToken token in allTokens)
                    {
                        newType = Newtonsoft.Json.JsonConvert.DeserializeObject<EveType>(token.First.ToString());
                        newType.typeID = Convert.ToInt32(token.Path);
                        Utility.AddRecordToBatch<EveType>(eveTypeTable, ref batchTypes, newType);

                        if (newType.description != null)
                        {
                            newType.description.parentTypeId = newType.typeID;
                            langDscrTable.Name = eveTypeDscrTableName;
                            Utility.AddRecordToBatch<LanguageDescription>(langDscrTable, ref batchTypeDscr, newType.description);
                        }

                        if (newType.name != null)
                        {
                            newType.name.parentTypeId = newType.typeID;
                            langDscrTable.Name = eveTypeNameTableName;
                            Utility.AddRecordToBatch<LanguageDescription>(langDscrTable, ref batchTypeName, newType.name);
                        }

                        if (newType.traits != null)
                        {
                            if (newType.traits.miscBonuses?.Count > 0)
                            {
                                int traitCount = 0;
                                foreach (EveTypeBonus bonus in newType.traits.miscBonuses)
                                {
                                    traitCount += 1;
                                    bonus.typeID = newType.typeID;
                                    bonus.bonusedTypeID = traitCount;
                                    eveTypeBonusTable.Name = miscBonusTableName;
                                    Utility.AddRecordToBatch<EveTypeBonus>(eveTypeBonusTable, ref batchEveTypeMiscBonus, bonus);
                                    

                                    if (bonus.bonusText != null)
                                    {
                                        bonus.bonusText.parentTypeId = newType.typeID;
                                        bonus.bonusText.parentTypeId2 = bonus.bonusedTypeID;
                                        bonus.bonusText.parentTypeCategory = "Misc Bonus";
                                        langDscrTable.Name = eveTypeBonusTextTableName;
                                        Utility.AddRecordToBatch<LanguageDescription>(langDscrTable, ref batchEveTypeMiscBonusText, bonus.bonusText);
                                    }
                                }
                            }
                            if (newType.traits.roleBonuses?.Count > 0)
                            {
                                int bonusCount = 0;
                                foreach (EveTypeBonus bonus in newType.traits.roleBonuses)
                                {
                                    bonusCount += 1;
                                    bonus.typeID = newType.typeID;
                                    bonus.bonusedTypeID = bonusCount;
                                    eveTypeBonusTable.Name = roleBonusTableName;
                                    Utility.AddRecordToBatch<EveTypeBonus>(eveTypeBonusTable, ref batchEveTypeRoleBonus, bonus);

                                    if (bonus.bonusText != null)
                                    {
                                        bonus.bonusText.parentTypeId = newType.typeID;
                                        bonus.bonusText.parentTypeId2 = bonus.bonusedTypeID;
                                        bonus.bonusText.parentTypeCategory = "Role Bonus";
                                        langDscrTable.Name = eveTypeBonusTextTableName;
                                        Utility.AddRecordToBatch<LanguageDescription>(langDscrTable, ref batchEveTypeRoleBonusText, bonus.bonusText);
                                    }
                                }
                            }
                            if (newType.traits.types?.Count > 0)
                            {
                                foreach (EveTypeBonus bonus in newType.traits.types)
                                {
                                    bonus.typeID = newType.typeID;
                                    eveTypeBonusTable.Name = eveTypeBonusTableName;
                                    Utility.AddRecordToBatch<EveTypeBonus>(eveTypeBonusTable, ref batchEveTypeTraitBonus, bonus);

                                    if (bonus.bonusText != null)
                                    {
                                        bonus.bonusText.parentTypeId = newType.typeID;
                                        bonus.bonusText.parentTypeId2 = bonus.bonusedTypeID;
                                        bonus.bonusText.parentTypeCategory = "Type Bonus";
                                        langDscrTable.Name = eveTypeBonusTextTableName;
                                        Utility.AddRecordToBatch<LanguageDescription>(langDscrTable, ref batchEveTypeTraitBonusText, bonus.bonusText);
                                    }
                                }
                            }
                        }

                        if (newType.masteries?.Count > 0)
                        {
                            foreach (EveTypeMastery eveTypeMastery in newType.masteries)
                            {
                                eveTypeMastery.typeID = newType.typeID;
                                Utility.AddRecordToBatch<EveTypeMastery>(eveTypeMasteryTable, ref batchMasteries, eveTypeMastery);
                            }
                        }
                        count++;
                    }

                    Utility.InsertBatchRecord<EveType>(eveTypeTable, batchTypes);
                    langDscrTable.Name = eveTypeDscrTableName;
                    Utility.InsertBatchRecord<LanguageDescription>(langDscrTable, batchTypeDscr);
                    langDscrTable.Name = eveTypeNameTableName;
                    Utility.InsertBatchRecord<LanguageDescription>(langDscrTable, batchTypeName);
                    eveTypeBonusTable.Name = miscBonusTableName;
                    Utility.InsertBatchRecord<EveTypeBonus>(eveTypeBonusTable, batchEveTypeMiscBonus);
                    langDscrTable.Name = eveTypeBonusTextTableName;
                    Utility.InsertBatchRecord<LanguageDescription>(langDscrTable, batchEveTypeMiscBonusText);
                    eveTypeBonusTable.Name = roleBonusTableName;
                    Utility.InsertBatchRecord<EveTypeBonus>(eveTypeBonusTable, batchEveTypeRoleBonus);
                    langDscrTable.Name = eveTypeBonusTextTableName;
                    Utility.InsertBatchRecord<LanguageDescription>(langDscrTable, batchEveTypeRoleBonusText);
                    eveTypeBonusTable.Name = eveTypeBonusTableName;
                    Utility.InsertBatchRecord<EveTypeBonus>(eveTypeBonusTable, batchEveTypeTraitBonus);
                    langDscrTable.Name = eveTypeBonusTextTableName;
                    Utility.InsertBatchRecord<LanguageDescription>(langDscrTable, batchEveTypeTraitBonusText);
                    Utility.InsertBatchRecord<EveTypeMastery>(eveTypeMasteryTable, batchMasteries);
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
