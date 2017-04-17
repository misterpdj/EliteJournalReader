using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace EliteJournalReader.Events
{
    //When Written: when starting the game; used to update to the current status of the cargo inventory
    //Parameters:
    //•	Inventory, which consists of:
    //•	Name
    //•	Count

    public class CargoEvent : JournalEvent<CargoEvent.CargoEventArgs>
    {
        public CargoEvent() : base("Cargo") { }

        public class CargoEventArgs : JournalEventArgs
        {
            public struct CargoItems
            {
                public string Name;
                public int Count;
            }

            public override void Initialize(JObject evt)
            {
                base.Initialize(evt);
                Items = evt["Inventory"].ToObject<CargoItems[]>();
            }


            public CargoItems[] Items { get; set; }
        }
    }
}
