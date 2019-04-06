using System;
using System.Linq;
using System.Threading.Tasks;
using DnDSimulator.Encounter;
using DnDSimulator.Interfaces;

namespace DnDSimulator.Helpers
{
    public class Damage : IDamage
    {
        public Damage(IDice damageDice, int damageBonus, DamageType damageType)
        {
            DamageDice = damageDice ?? throw new ArgumentNullException(nameof(damageDice));
            DamageBonus = damageBonus;
            DamageType = damageType;
        }

        public IDice DamageDice { get; set; }
        public int DamageBonus { get; set; }
        public DamageType DamageType { get; set; }

        public async Task<int> RollAsync()
        {
            return await DamageDice.GetTotalRollAsync() + DamageBonus;
        }
    }
}