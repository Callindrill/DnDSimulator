using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DnDSimulator.Interfaces;

namespace DnDSimulator
{
    public class Dice : List<IDie>, IDice
    {
        public Dice(Die.Factory dieFactory)
        {
            DieFactory = dieFactory ?? throw new ArgumentNullException(nameof(dieFactory));
        }

        private Die.Factory DieFactory { get; }
        
        public void Add(int numberOfSides, int bonusToResult, bool alwaysRollsAverage)
        {
            Add(DieFactory(numberOfSides, bonusToResult, alwaysRollsAverage));
        }

        public async Task<int> GetTotalRollAsync()
        {
            return (await Task.WhenAll(RollAllAsync())).Sum();
        }

        public IEnumerable<Task<int>> RollAllAsync()
        {
            return this.Select(die => die.RollAsync());
        }

        public IEnumerable<int> RollAll()
        {
            return (Task.WhenAll(RollAllAsync()).Result);
        }
    }
}