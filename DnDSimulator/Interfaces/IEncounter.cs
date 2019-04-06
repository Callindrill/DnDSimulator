using System.Collections.Generic;
using DnDSimulator.Encounter;

namespace DnDSimulator.Interfaces
{
    public interface IEncounter
    {
        IList<IFaction> Combatants { get; }
        IFaction GetWinningFaction();
    }
}