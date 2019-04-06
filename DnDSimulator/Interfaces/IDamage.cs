using System.Threading.Tasks;
using DnDSimulator.Encounter;

namespace DnDSimulator.Interfaces
{
    public interface IDamage
    {
        IDice DamageDice { get; set; }
        DamageType DamageType { get; set; }
        Task<int> RollAsync();
    }
}