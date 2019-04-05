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

        public Actor(
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
            int charisma,
            HitPoints.Factory hitPointsFactory,
            Abilities.Factory abilityScoreFactory)
        {
            if (hitPointsFactory == null) throw new ArgumentNullException(nameof(hitPointsFactory));
            if (abilityScoreFactory == null) throw new ArgumentNullException(nameof(abilityScoreFactory));
            ArmorClass = armorClass;
            Proficiency = proficiency;
            HitPoints = hitPointsFactory(currentHitPoints, maxHitPoints, temporaryHitPoints);
            AbilityScores = abilityScoreFactory(strength, dexterity, constitution, intelligence, wisdom, charisma);
        }

        public int ArmorClass { get; set; }
        public int Proficiency { get; set; }
        public IHitPoints HitPoints { get; set; }
        public IAbilities AbilityScores { get; set; }
        public async Task DamageAsync(IDice damageRoll)
        {
            var damageDealt = (await Task.WhenAll(damageRoll.RollAllAsync())).Sum();
            //TODO: If you are resistant to the type, you'll take half of this.
            HitPoints.LoseHitPoints(damageDealt);
        }
        public async Task Heal(IDice damageRoll)
        {
            var healingDone = await FlipSignAsync((await Task.WhenAll(damageRoll.RollAllAsync())).Sum());
            HitPoints.GainHitPoints(healingDone);
        }
        public async Task<int> FlipSignAsync(int value)
        {
            return await Task.Run(() =>-value);
        }
    }
}