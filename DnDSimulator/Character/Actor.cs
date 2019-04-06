using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DnDSimulator.Encounter;
using DnDSimulator.Interfaces;

namespace DnDSimulator.Character
{
    public class Actor : IActor
    {
        private readonly IDice _rollableDice;

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
            IDice rollableDice,
            HitPoints.Factory hitPointsFactory,
            Abilities.Factory abilityScoreFactory)
        {
            if (hitPointsFactory == null) throw new ArgumentNullException(nameof(hitPointsFactory));
            if (abilityScoreFactory == null) throw new ArgumentNullException(nameof(abilityScoreFactory));
            _rollableDice = rollableDice ?? throw new ArgumentNullException(nameof(rollableDice));
            ArmorClass = armorClass;
            Proficiency = proficiency;
            HitPoints = hitPointsFactory(currentHitPoints, maxHitPoints, temporaryHitPoints);
            AbilityScores = abilityScoreFactory(strength, dexterity, constitution, intelligence, wisdom, charisma);
        }

        public IList<DamageType> Vulnerabilities { get; } = new List<DamageType>();
        public IList<DamageType> Resistances { get; } = new List<DamageType>();
        public IList<DamageType> Immunities { get; } = new List<DamageType>();

        public int ArmorClass { get; set; }
        public int Proficiency { get; set; }
        public IHitPoints HitPoints { get; set; }
        public IAbilities AbilityScores { get; set; }

        public async Task<int> RollInitiativeAsync() //TODO: Probably going to need a strategy pattern here based on "class". Autofac Keyed.
        {
            _rollableDice.Clear();
            _rollableDice.Add(20, AbilityScores.Dexterity.Modifier, false);
            var initiative = await _rollableDice.RollAndTakeHighestAsync(1);
            _rollableDice.Clear();

            return initiative;
        }

        public async Task DamageAsync(IDamage damage)
        {
            if (IsImmune(damage.DamageType)) return;
            var damageDealt = await damage.RollAsync();
            if (IsResistant(damage.DamageType)) damageDealt /= 2;
            if (IsVulnerable(damage.DamageType)) damageDealt *= 2;
            HitPoints.LoseHitPoints(damageDealt);
        }

        public async Task Heal(IDice damageRoll)
        {
            var healingDone = await damageRoll.GetTotalRollAsync();
            HitPoints.GainHitPoints(healingDone);
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
    }
}