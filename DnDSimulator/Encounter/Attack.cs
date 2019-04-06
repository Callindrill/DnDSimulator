using System;
using System.Threading.Tasks;
using DnDSimulator.Interfaces;

namespace DnDSimulator.Encounter
{
    internal class Attack : IAttack
    {
        public Attack(IDie attackRoll, IDamage damage)
        {
            AttackRoll = attackRoll ?? throw new ArgumentNullException(nameof(attackRoll));
            Damage = damage ?? throw new ArgumentNullException(nameof(damage));
        }

        public IDie AttackRoll { get; set; }
        public IDamage Damage { get; set; }

        public async Task ActAsync(IActor target)
        {
            if (await AttackRoll.RollAsync() >= target.ArmorClass)
            {
                await target.DamageAsync(Damage);
            }
        }
    }
}