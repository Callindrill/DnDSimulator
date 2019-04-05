using DnDSimulator.Interfaces;

namespace DnDSimulator.Character
{
    public class Constitution : Ability, IConstitution
    {
        public delegate Constitution Factory(int score);

        public Constitution(int score) : base(score)
        {
        }
    }
}