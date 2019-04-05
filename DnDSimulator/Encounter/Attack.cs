using System;
using System.Threading.Tasks;
using DnDSimulator.Character;
using DnDSimulator.Interfaces;

namespace DnDSimulator.Encounter
{
    internal class Attack
    {
        public Attack(IDie attackRoll, IDice damageRoll)
        {
            AttackRoll = attackRoll ?? throw new ArgumentNullException(nameof(attackRoll));
            DamageRoll = damageRoll ?? throw new ArgumentNullException(nameof(damageRoll));
        }

        public IDie AttackRoll { get; set; }
        public IDice DamageRoll { get; set; }

        public async Task ActAsync(IActor target)
        {
            if (await AttackRoll.RollAsync() >= target.ArmorClass)
            {
                await target.DamageAsync(DamageRoll);
            }
        }
    }
}