using System.Threading.Tasks;

namespace DnDSimulator.Interfaces
{
    internal interface IAttack
    {
        IDie AttackRoll { get; set; }
        IDamage Damage { get; set; }
        Task ActAsync(IActor target);
    }
}