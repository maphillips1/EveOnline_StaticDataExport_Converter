using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.BSD
{
    internal class StaStation
    {
        [Attributes.SQLiteType("INT")]
        public long stationID {  get; set; }
        [Attributes.SQLiteType("INT")]
        public long constellationID {  get; set; }
        [Attributes.SQLiteType("INT")]
        public long corporationID { get; set; }
        [Attributes.SQLiteType("INT")]
        public decimal dockingCostPerVolume { get; set; }
        [Attributes.SQLiteType("INT")]
        public long maxShipVolumeDockable {  get; set; }
        [Attributes.SQLiteType("INT")]
        public int officeRentalCost {  get; set; }
        [Attributes.SQLiteType("INT")]
        public int operationID { get; set; }
        [Attributes.SQLiteType("INT")]
        public long regionID { get; set; }
        [Attributes.SQLiteType("INT")]
        public decimal reprocessingEfficiency { get; set; }
        [Attributes.SQLiteType("INT")]
        public decimal reprocessingHangarFlag { get; set; }
        [Attributes.SQLiteType("INT")]
        public decimal reprocessingStationsTake {  get; set; }
        [Attributes.SQLiteType("INT")]
        public double security {  get; set; }
        [Attributes.SQLiteType("INT")]
        public long solarSystemID { get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string stationName { get; set; }
        [Attributes.SQLiteType("INT")]
        public int stationTypeID { get; set; }
        [Attributes.SQLiteType("INT")]
        public double x {  get; set; }
        [Attributes.SQLiteType("INT")]
        public double y { get; set; }
        [Attributes.SQLiteType("INT")]
        public double z { get; set; }
    }
}
