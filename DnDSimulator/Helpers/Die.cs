using System;
using System.Threading.Tasks;
using DnDSimulator.Interfaces;

namespace DnDSimulator
{
    public class Die : IDie
    {
        public delegate Die Factory(int numberOfSides, int bonusToResult, bool alwaysRollsAverage);

        private readonly IRandomAsync _randomAsync;

        public Die(
            int numberOfSides,
            int bonusToResult,
            bool alwaysRollsAverage,
            IRandomAsync randomAsync
        )
        {
            _randomAsync = randomAsync ?? throw new ArgumentNullException(nameof(randomAsync));
            NumberOfSides = numberOfSides;
            BonusToResult = bonusToResult;
            AlwaysRollsAverage = alwaysRollsAverage;
        }

        public int NumberOfSides { get; }
        public int BonusToResult { get; }
        public bool AlwaysRollsAverage { get; }

        public async Task<int> RollAsync()
        {
            if (AlwaysRollsAverage) return await AverageAsync();
            var dieResult = await _randomAsync.NextAsync(1, NumberOfSides + 1);
            return dieResult + BonusToResult;
        }

        private async Task<int> AverageAsync()
        {
            return await Task.Run(() => (1 + NumberOfSides) / 2 + BonusToResult);
        }
    }
}