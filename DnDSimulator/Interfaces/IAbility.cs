namespace DnDSimulator.Interfaces
{
    public interface IAbility
    {
        int Modifier { get; }
        int Score { get; set; }
    }
}