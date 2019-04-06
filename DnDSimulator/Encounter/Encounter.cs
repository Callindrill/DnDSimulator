using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DnDSimulator.Interfaces;

namespace DnDSimulator.Encounter
{
    public class Encounter : IEncounter
    {
        public int Round { get; set; }
        public IList<IFaction> Factions { get; } = new List<IFaction>();

        public IFaction GetWinningFaction()
        {
            // Get the first "two" possible winners.
            var possibleWinners = Factions.Where(i => !i.Defeated).Take(2).ToList();

            // If there weren't two, we have just one winner... return them. Otherwise, no one has won yet.
            return (possibleWinners.Count == 1) ? possibleWinners[0] : null;
        }

        public async Task StartEncounter()
        {
            foreach (var faction in Factions)
            {
                foreach (var factionParticipant in faction.Participants)
                {
                   await factionParticipant.RollInitiativeAsync();
                }
            }
        }

        public async Task<IFaction> RunEncounter() //TODO: Probably want to put in some kind of decision maker that will look over each actor and decide on actions.
        {
            return await Task.Run(() =>
            {
                do
                {

                } while (GetWinningFaction() == null);
                return GetWinningFaction();
            });
        }
    }
}