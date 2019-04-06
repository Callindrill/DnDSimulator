using System.Collections.Generic;

namespace DnDSimulator.Interfaces
{
    public interface IActorGroup : IList<IActor>
    {
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
    }
}