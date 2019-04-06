using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DnDSimulator.Encounter;
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
        public IList<DamageType> Vulnerabilities { get; } = new List<DamageType>();
        public IList<DamageType> Resistances { get; } = new List<DamageType>();
        public IList<DamageType> Immunities { get; } = new List<DamageType>();

        public async Task DamageAsync(IDamage damage)
        {
            if (IsImmune(damage.DamageType)) return;
            var damageDealt = await damage.RollAsync();
            if (IsResistant(damage.DamageType)) damageDealt /= 2;
            if (IsVulnerable(damage.DamageType)) damageDealt *= 2;
            HitPoints.LoseHitPoints(damageDealt);
        }

        private bool IsImmune(DamageType damageType)
        {
            return Immunities.Contains(damageType);
        }
        private bool IsResistant(DamageType damageType)
        {
            return Resistances.Contains(damageType);
        }

        private bool IsVulnerable(DamageType damageType)
        {
            return Vulnerabilities.Contains(damageType);
        }

        public async Task Heal(IDice damageRoll)
        {
            var healingDone = await damageRoll.GetTotalRollAsync();
            HitPoints.GainHitPoints(healingDone);
        }
    }
}