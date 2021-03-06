using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace EliteJournalReader.Events
{
    //When Written: detailed discovery scan of a star, planet or moon
    //Parameters(star)
    //	Bodyname: name of body
    //	DistanceFromArrivalLS
    //	StarType: Stellar classification (for a star)
    //	StellarMass: mass as multiple of Sols mass
    //	Radius
    //	AbsoluteMagnitude
    //	RotationPeriod (seconds)
    //	SurfaceTemperature
    //	Age_MY: age in millions of years
    //	Rings: [ array ] - if present
    //
    //Parameters(Planet/Moon) 
    //	Bodyname: name of body
    //	DistanceFromArrivalLS
    //	TidalLock: 1 if tidally locked
    //	TerraformState: Terraformable, Terraforming, Terraformed, or null
    //	PlanetClass
    //	Atmosphere
    //	Volcanism
    //	SurfaceGravity
    //	SurfaceTemperature
    //	SurfacePressure
    //	Landable: true (if landable)
    //	Materials: JSON object with material names and percentage occurrence
    //	RotationPeriod (seconds)
    //	Rings [ array of info ] - if rings present
    //
    // Orbital Parameters for any Star/Planet/Moon (except main star of single-star system)
    //	SemiMajorAxis
    //	Eccentricity
    //	OrbitalInclination
    //	Periapsis
    //	OrbitalPeriod (seconds)
    //
    // Rings properties
    //	Name
    //	RingClass
    //	MassMT - ie in megatons
    //	InnerRad
    //	OuterRad
    //
    // STAR TYPES:
    // 
    // (Main sequence:) O B A F G K M L T Y 
    // (Proto stars:) TTS AeBe  
    // (Wolf-Raylet:) W WN WNC WC WO
    // (Carbon stars:) CS C CN CJ CH CHd
    // MS S 
    // (white dwarfs:) D DA DAB DAO DAZ DAV DB DBZ DBV DO DOV DQ DC DCV DX
    // N (=Neutron)
    // H (=Black Hole)
    // X (=exotic)
    // SupermassiveBlackHole
    // A_BlueWhiteSuperGiant
    // F_WhiteSuperGiant
    // M_RedSuperGiant
    // M_RedGiant
    // K_OrangeGiant
    // RoguePlanet
    // Nebula
    // StellarRemnantNebula
    // 
    // PLANET CLASSES:
    // 
    // Metal rich body
    // High metal content body
    // Rocky body
    // Icy body
    // Rocky ice body
    // Earthlike body
    // Water world
    // Ammonia world
    // Water giant
    // Water giant with life
    // Gas giant with water based life
    // Gas giant with ammonia based life
    // Sudarsky class I gas giant (also class II, III, IV, V)
    // Helium rich gas giant
    // Helium gas giant
    // 
    // ATMOSPHERE CLASSES:
    // 
    // No atmosphere
    // Suitable for water-based life
    // Ammonia and oxygen
    // Ammonia
    // Water
    // Carbon dioxide
    // Sulphur dioxide
    // Nitrogen
    // Water-rich
    // Methane-rich
    // Ammonia-rich
    // Carbon dioxide-rich
    // Methane
    // Helium
    // Silicate vapour
    // Metallic vapour
    // Neon-rich
    // Argon-rich
    // Neon
    // Argon
    // Oxygen
    // 
    // VOLCANISM CLASSES:
    // (all with possible 'minor' or 'major' qualifier)
    // 
    // None
    // Water Magma
    // Sulphur Dioxide Magma
    // Ammonia Magma
    // Methane Magma
    // Nitrogen Magma
    // Silicate Magma
    // Metallic Magma
    // Water Geysers
    // Carbon Dioxide Geysers
    // Ammonia Geysers
    // Methane Geysers
    // Nitrogen Geysers
    // Helium Geysers
    // Silicate Vapour Geysers
    public class ScanEvent : JournalEvent<ScanEvent.ScanEventArgs>
    {
        public ScanEvent() : base("Scan") { }

        public class ScanEventArgs : JournalEventArgs
        {
            public override void Initialize(JObject evt)
            {
                base.Initialize(evt);
                BodyName = evt.Value<string>("BodyName");
                DistanceFromArrivalLs = evt.Value<double>("DistanceFromArrivalLS");
                StarType = evt.Value<string>("StarType").ToEnum(StarType.Unknown);
                StellarMass = evt.Value<double?>("StellarMass");
                Radius = evt.Value<double?>("StellarMass");
                AbsoluteMagnitude = evt.Value<double?>("StellarMass");
                RotationPeriod = evt.Value<double>("StellarMass");
                Age = evt.Value<double?>("Age_MY");
                Rings = evt["Rings"]?.ToObject<PlanetRing[]>();

                TidalLock = evt.Value<bool?>("TidalLock") ?? false;
                TerraformState = evt.Value<string>("TerraformState").ToEnum(TerraformState.None);
                PlanetClass = evt.Value<string>("PlanetClass").ToEnum(PlanetClass.Unknown);
                Atmosphere = evt.Value<string>("Atmosphere").ToEnum(AtmosphereClass.Unknown);
                Volcanism = evt.Value<string>("Volcanism").ToEnum(VolcanismClass.Unknown);
                SurfaceGravity = evt.Value<double?>("SurfaceGravity");
                SurfaceTemperature = evt.Value<double?>("SurfaceTemperature");
                SurfacePressure = evt.Value<double?>("SurfacePressure");
                Landable = evt.Value<bool?>("Landable") ?? false;
                Materials = evt["Materials"]?.ToObject<Dictionary<string, double>>();

                SemiMajorAxis = evt.Value<double?>("SemiMajorAxis");
                Eccentricity = evt.Value<double?>("Eccentricity");
                OrbitalInclination = evt.Value<double?>("OrbitalInclination");
                Periapsis = evt.Value<double?>("Periapsis");
                OrbitalPeriod = evt.Value<double?>("OrbitalPeriod");
            }

            public double? Eccentricity { get; set; }
            public double? Periapsis { get; set; }
            public double? OrbitalInclination { get; set; }
            public double? Age { get; set; }

            public string BodyName { get; set; }
            public double DistanceFromArrivalLs { get; set; }
            public StarType StarType { get; set; }
            public double? StellarMass { get; set; }
            public double? Radius { get; set; }
            public double? AbsoluteMagnitude { get; set; }
            public double? OrbitalPeriod { get; set; }
            public double RotationPeriod { get; set; }
            public PlanetRing[] Rings { get; set; }

            public bool? TidalLock { get; set; }
            public TerraformState TerraformState { get; set; }
            public PlanetClass PlanetClass { get; set; }
            public AtmosphereClass Atmosphere { get; set; }
            public VolcanismClass Volcanism { get; set; }
            public double? SemiMajorAxis { get; set; } // not in description of event
            public double? SurfaceGravity { get; set; } // not in description of event
            public double? SurfaceTemperature { get; set; }
            public double? SurfacePressure { get; set; }
            public bool? Landable { get; set; }
            public Dictionary<string, double> Materials { get; set; }
        }
    }

    public struct PlanetRing
    {
        public string Name;
        public string RingClass;
        public double MassMT;
        public double InnerRad;
        public double OuterRad;
    }
}
