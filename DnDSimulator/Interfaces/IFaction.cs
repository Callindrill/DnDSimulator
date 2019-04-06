using System.Collections.Generic;
using DnDSimulator.Encounter;

namespace DnDSimulator.Interfaces
{
    public interface IFaction
    {
        IList<IActorGroup> Participants { get; }
        bool Defeated { get; }
    }
}