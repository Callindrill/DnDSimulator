using System.Threading.Tasks;
using DnDSimulator.Encounter;

namespace DnDSimulator.Interfaces
{
    public interface IActor
    {
        int ArmorClass { get; set; }
        int Proficiency { get; set; }
        IHitPoints HitPoints { get; set; }
        IAbilities AbilityScores { get; set; }

        Task<int> RollInitiativeAsync();
        Task DamageAsync(IDamage damageRoll);
        Task Heal(IDice damageRoll);
        Task ActAsync(IActionDecision actionDecision);
    }
}