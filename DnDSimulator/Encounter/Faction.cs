using System.Collections.Generic;
using System.Linq;
using DnDSimulator.Character;

namespace DnDSimulator.Encounter
{
    public class Faction
    {
        public IList<ActorGroup> Participants { get; } = new List<ActorGroup>();
        public bool Defeated => Participants.All(
            groups => groups.All(actors => actors.HitPoints.CurrentHitPoints <= 0)
            );
    }
}