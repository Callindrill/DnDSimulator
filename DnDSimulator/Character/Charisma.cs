using DnDSimulator.Interfaces;

namespace DnDSimulator.Character
{
    public class Charisma : Ability, ICharisma
    {
        public delegate Charisma Factory(int score);

        public Charisma(int score) : base(score)
        {
        }
    }
}