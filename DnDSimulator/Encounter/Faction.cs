using System.Collections.Generic;
using System.Linq;
using DnDSimulator.Character;
using DnDSimulator.Interfaces;

namespace DnDSimulator.Encounter
{
    /// <summary>
    /// Factions are the different sides of an encounter. There are generally two: the players, and the monsters.
    /// Sometimes, though, there are more... 
    /// </summary>
    public class Faction : IFaction
    {
        public IList<IActorGroup> Participants { get; } = new List<IActorGroup>();
        public bool Defeated => Participants.All(
            groups => groups.All(actors => actors.HitPoints.CurrentHitPoints <= 0)
            );
    }
}