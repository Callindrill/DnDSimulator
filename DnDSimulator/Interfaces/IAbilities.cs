namespace DnDSimulator.Interfaces
{
    public interface IAbilities
    {
        IStrength Strength { get; set; }
        IDexterity Dexterity { get; set; }
        IConstitution Constitution { get; set; }
        IIntelligence Intelligence { get; set; }
        IWisdom Wisdom { get; set; }
        ICharisma Charisma { get; set; }
    }
}