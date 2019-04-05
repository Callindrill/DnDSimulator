using System.Threading.Tasks;

namespace DnDSimulator.Interfaces
{
    public interface IActor
    {
        int ArmorClass { get; set; }
        int HitPoints { get; set; }
        int MaxHitPoints { get; set; }
        int Proficiency { get; set; }
        IAbilities AbilityScores { get; set; }
        Task Damage(IDice damageRoll);
        Task Heal(IDice damageRoll);
    }
}