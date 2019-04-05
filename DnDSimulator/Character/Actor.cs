using System;
using System.Linq;
using System.Threading.Tasks;
using DnDSimulator.Interfaces;

namespace DnDSimulator.Character
{
    public class Actor : IActor
    {
        public delegate Actor Factory(
            int armorClass,
            int hitPoints,
            int maxHitPoints,
            int proficiency,
            int strength,
            int dexterity,
            int constitution,
            int intelligence,
            int wisdom,
            int charisma);

        public Actor(
            int armorClass,
            int hitPoints,
            int maxHitPoints,
            int proficiency,
            int strength,
            int dexterity,
            int constitution,
            int intelligence,
            int wisdom,
            int charisma,
            Abilities.Factory abilityScoreFactory)
        {
            if (abilityScoreFactory == null) throw new ArgumentNullException(nameof(abilityScoreFactory));
            ArmorClass = armorClass;
            HitPoints = hitPoints;
            MaxHitPoints = maxHitPoints;
            Proficiency = proficiency;
            AbilityScores = abilityScoreFactory(strength, dexterity, constitution, intelligence, wisdom, charisma);
        }

        public int ArmorClass { get; set; }
        public int HitPoints { get; set; }
        public int MaxHitPoints { get; set; }
        public int Proficiency { get; set; }
        public IAbilities AbilityScores { get; set; }
        public async Task Damage(IDice damageRoll)
        {
            var damageDealt = (await Task.WhenAll(damageRoll.RollAllAsync())).Sum();
            HitPoints = Math.Max(0, HitPoints - damageDealt);
        }
        public async Task Heal(IDice damageRoll)
        {
            var healingDone = await FlipSignAsync((await Task.WhenAll(damageRoll.RollAllAsync())).Sum());
            HitPoints = Math.Min(MaxHitPoints, HitPoints + healingDone);
        }
        public async Task<int> FlipSignAsync(int value)
        {
            return await Task.Run(() =>-value);
        }
    }
}