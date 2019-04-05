using System.Collections.Generic;
using System.Linq;

namespace DnDSimulator.Encounter
{
    public class Encounter
    {
        public IList<Faction> Combatants { get; } = new List<Faction>();

        public bool WinCondition => Combatants.TakeWhile(i => i.Defeated).Count() == Combatants.Count - 1;
    }
}