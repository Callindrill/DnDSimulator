using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DnDSimulator.Encounter;
using DnDSimulator.Helpers;
using DnDSimulator.Interfaces;

namespace DnDSimulator.Character
{
    public class Actor : IActor
    {
        private readonly IDice _rollableDice;

        public delegate Actor Factory(
            string name,
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
            string name,
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
            Name = name ?? throw new ArgumentNullException(nameof(name));
            ArmorClass = armorClass;
            Proficiency = proficiency;
            HitPoints = hitPointsFactory(currentHitPoints, maxHitPoints, temporaryHitPoints);
            AbilityScores = abilityScoreFactory(strength, dexterity, constitution, intelligence, wisdom, charisma);
        }

        public IList<DamageType> Vulnerabilities { get; } = new List<DamageType>();
        public IList<DamageType> Resistances { get; } = new List<DamageType>();
        public IList<DamageType> Immunities { get; } = new List<DamageType>();

        public string Name { get; set; }
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

        public async Task ActAsync(IActionDecision actionDecision)
        {
            //Don't care what you picked... you're hitting something. This will eventually get injected in, I'd wager.
            //for now, everyone uses a rapier (finesse)
            var bestModifier = Math.Max(AbilityScores.Strength.Modifier, AbilityScores.Dexterity.Modifier);
            var damageDie = 8;

            _rollableDice.Clear(); //Put down any dice you may already be holding...
            _rollableDice.Add(
                numberOfSides: 20,
                bonusToResult: (bestModifier + Proficiency),
                alwaysRollsAverage: false);
            var attack = await _rollableDice.RollAndTakeHighestAsync(count: 1);
            _rollableDice.Clear();

            var baseRoll = attack - (bestModifier + Proficiency);
            if (baseRoll != 1)
            {
                if (baseRoll == 20) //Crit!
                {
                    _rollableDice.Add(numberOfSides: damageDie, bonusToResult: 0, alwaysRollsAverage: false);
                    _rollableDice.Add(numberOfSides: damageDie, bonusToResult: bestModifier, alwaysRollsAverage: false);
                    var damage = new Damage(_rollableDice, DamageType.Piercing);
                    await actionDecision.TargetActor.DamageAsync(damage);
                }
                else if (attack >= actionDecision.TargetActor.ArmorClass)
                {
                    _rollableDice.Add(numberOfSides: damageDie, bonusToResult: bestModifier, alwaysRollsAverage: false);
                    var damage = new Damage(_rollableDice, DamageType.Piercing);
                    await actionDecision.TargetActor.DamageAsync(damage);

                }
            }

            _rollableDice.Clear();
        }
    }
}