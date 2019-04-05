using DnDSimulator.Interfaces;

namespace DnDSimulator.Character
{
    public class Strength : Ability, IStrength
    {
        public delegate Strength Factory(int score);

        public Strength(int score) : base(score)
        {
        }
    }
}