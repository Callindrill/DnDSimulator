using System.Threading.Tasks;

namespace DnDSimulator.Interfaces
{
    public interface IActor
    {
        int ArmorClass { get; set; }
        int Proficiency { get; set; }
        IHitPoints HitPoints { get; set; }
        IAbilities AbilityScores { get; set; }
        Task DamageAsync(IDice damageRoll);
        Task Heal(IDice damageRoll);
    }
}