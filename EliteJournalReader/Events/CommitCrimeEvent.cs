using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace EliteJournalReader.Events
{
    //When written: when a crime is recorded against the player
    //Parameters:
    //�	CrimeType
    //�	Faction
    //Optional parameters (depending on crime)
    //�	Victim
    //�	Fine
    //�	Bounty
    public class CommitCrimeEvent : JournalEvent<CommitCrimeEvent.CommitCrimeEventArgs>
    {
        public CommitCrimeEvent() : base("CommitCrime") { }

        public class CommitCrimeEventArgs : JournalEventArgs
        {
            public override void Initialize(JObject evt)
            {
                base.Initialize(evt);
                CrimeType = evt.Value<string>("CrimeType").ToEnum(CrimeType.Unknown);
                Faction = evt.Value<string>("Faction");
                Fine = evt.Value<int?>("Fine");
                Bounty = evt.Value<int?>("Bounty");
            }

            public CrimeType CrimeType { get; set; }
            public string Faction { get; set; }
            public string Victim { get; set; }
            public int? Fine { get; set; }
            public int? Bounty { get; set; }
        }
    }
}