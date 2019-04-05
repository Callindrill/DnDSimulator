using DnDSimulator.Interfaces;

namespace DnDSimulator.Character
{
    public class Wisdom : Ability, IWisdom
    {
        public delegate Wisdom Factory(int score);

        public Wisdom(int score) : base(score)
        {
        }
    }
}