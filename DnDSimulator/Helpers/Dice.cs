using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DnDSimulator.Interfaces;

namespace DnDSimulator
{
    public class Dice : IDice
    {
        private readonly IList<IDie> _dice = new List<IDie>();

        public Dice(Die.Factory dieFactory)
        {
            DieFactory = dieFactory ?? throw new ArgumentNullException(nameof(dieFactory));
        }

        private Die.Factory DieFactory { get; }

        public int Count => _dice.Count;
        public bool IsReadOnly => _dice.IsReadOnly;


        public IEnumerator<IDie> GetEnumerator()
        {
            return _dice.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(IDie item)
        {
            _dice.Add(item);
        }

        public void Add(int numberOfSides, int bonusToResult, bool alwaysRollsAverage)
        {
            Add(DieFactory(numberOfSides, bonusToResult, alwaysRollsAverage));
        }

        public void Clear()
        {
            _dice.Clear();
        }

        public bool Contains(IDie item)
        {
            return _dice.Contains(item);
        }

        public void CopyTo(IDie[] array, int arrayIndex)
        {
            _dice.CopyTo(array, arrayIndex);
        }

        public bool Remove(IDie item)
        {
            return _dice.Remove(item);
        }

        public int IndexOf(IDie item)
        {
            return _dice.IndexOf(item);
        }

        public void Insert(int index, IDie item)
        {
            _dice.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _dice.RemoveAt(index);
        }

        public IDie this[int index]
        {
            get => _dice[index];
            set => _dice[index] = value;
        }

        public IEnumerable<Task<int>> RollAllAsync()
        {
            return _dice.Select(die => die.RollAsync());
        }

        public IEnumerable<int> RollAll()
        {
            //return RollAllAsync().Select(task => task.Result);
            return (Task.WhenAll(RollAllAsync()).Result);
        }
    }
}