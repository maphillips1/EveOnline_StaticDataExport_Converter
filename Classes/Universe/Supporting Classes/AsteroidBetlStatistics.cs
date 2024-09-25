using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveStaticDataExportConverter.Classes.Universe.Supporting_Classes
{
    internal class AsteroidBetlStatistics
    {
        [Attributes.SQLiteType("INT")]
        [Attributes.SQLiteIndex()]
        public long asteroidBeltID {  get; set; }
        [Attributes.SQLiteType("INT")]
        public decimal density { get; set; }
        [Attributes.SQLiteType("INT")]
        public double eccentricity { get; set; }
        [Attributes.SQLiteType("INT")]
        public decimal escapeVelocity { get; set; }
        [Attributes.SQLiteType("INT")]
        public bool fragmented { get; set; }
        [Attributes.SQLiteType("INT")]
        public decimal life { get; set; }
        [Attributes.SQLiteType("INT")]
        public bool locked { get; set; }
        [Attributes.SQLiteType("INT")]
        public double massDust { get; set; }
        [Attributes.SQLiteType("INT")]
        public double massGas { get; set; }
        [Attributes.SQLiteType("INT")]
        public double orbitPeriod { get; set; }
        [Attributes.SQLiteType("INT")]
        public double orbitRadius { get; set; }
        [Attributes.SQLiteType("INT")]
        public double pressure { get; set; }
        [Attributes.SQLiteType("INT")]
        public double radius { get; set; }
        [Attributes.SQLiteType("INT")]
        public double rotationRate { get; set; }
        [Attributes.SQLiteType("TEXT")]
        public string spectralClass { get; set; }
        [Attributes.SQLiteType("INT")]
        public double surfaceGravity { get; set; }
        [Attributes.SQLiteType("INT")]
        public double temperature { get; set; }
    }
}
