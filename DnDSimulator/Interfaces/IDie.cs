using System.Threading.Tasks;

namespace DnDSimulator.Interfaces
{
    public interface IDie
    {
        int NumberOfSides { get; }
        int BonusToResult { get; }
        bool AlwaysRollsAverage { get; }
        Task<int> RollAsync();
    }
}