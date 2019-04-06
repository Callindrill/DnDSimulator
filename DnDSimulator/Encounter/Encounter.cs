using System.Collections.Generic;
using System.Linq;
using DnDSimulator.Interfaces;

namespace DnDSimulator.Encounter
{
    public class Encounter : IEncounter
    {
        public IList<IFaction> Combatants { get; } = new List<IFaction>();

        public IFaction GetWinningFaction()
        {
            // Get the first "two" possible winners.
            var possibleWinners = Combatants.Where(i => !i.Defeated).Take(2).ToList();

            // If there weren't two, we have just one winner... return them. Otherwise, no one has won yet.
            return (possibleWinners.Count == 1) ? possibleWinners[0] : null;
        }
    }
}