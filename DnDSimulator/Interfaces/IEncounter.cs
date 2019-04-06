using System.Collections.Generic;
using System.Threading.Tasks;
using DnDSimulator.Encounter;

namespace DnDSimulator.Interfaces
{
    public interface IEncounter
    {
        IList<IFaction> Factions { get; }
        IFaction GetWinningFaction();
        Task StartEncounter();
        Task<IFaction> RunEncounter();
    }
}