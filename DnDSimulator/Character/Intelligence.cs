using DnDSimulator.Interfaces;

namespace DnDSimulator.Character
{
    public class Intelligence : Ability, IIntelligence
    {
        public delegate Intelligence Factory(int score);

        public Intelligence(int score) : base(score)
        {
        }
    }
}