using System.Collections.Generic;
using System.Linq;
using DnDSimulator.Character;

namespace DnDSimulator.Encounter
{
    public class Faction
    {
        public IList<Actor> Participants { get; } = new List<Actor>();
        public bool Defeated => Participants.All(i => i.HitPoints <= 0);
    }
}