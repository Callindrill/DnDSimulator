using System.Threading.Tasks;

namespace DnDSimulator.Interfaces
{
    public interface IActor
    {
        int ArmorClass { get; set; }
        int Proficiency { get; set; }
        IHitPoints HitPoints { get; set; }
        IAbilities AbilityScores { get; set; }
        Task DamageAsync(IDamage damageRoll);
        Task Heal(IDice damageRoll);
    }
}