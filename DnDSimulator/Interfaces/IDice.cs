using System.Collections.Generic;
using System.Threading.Tasks;

namespace DnDSimulator.Interfaces
{
    public interface IDice : IList<IDie>
    {
        void Add(int numberOfSides, int bonusToResult, bool alwaysRollsAverage);
        IEnumerable<Task<int>> RollAllAsync();
        IEnumerable<int> RollAll();
    }
}