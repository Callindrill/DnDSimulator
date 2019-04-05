using DnDSimulator.Interfaces;

namespace DnDSimulator.Character
{
    public class Dexterity : Ability, IDexterity
    {
        public delegate Dexterity Factory(int score);

        public Dexterity(int score) : base(score)
        {
        }
    }
}