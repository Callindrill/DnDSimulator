using System.Collections.Generic;
using System.Threading.Tasks;

namespace DnDSimulator.Interfaces
{
    public interface IActorGroup : IList<IActor>
    {
        int Initiative { get; set; }

        void Add(
            int armorClass,
            int currentHitPoints,
            int maxHitPoints,
            int temporaryHitPoints,
            int proficiency,
            int strength,
            int dexterity,
            int constitution,
            int intelligence,
            int wisdom,
            int charisma);

        Task RollInitiativeAsync();
    }
}