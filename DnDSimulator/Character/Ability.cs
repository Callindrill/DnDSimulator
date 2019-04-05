using System;
using DnDSimulator.Interfaces;

namespace DnDSimulator.Character
{
    public class Ability : IAbility
    {
        public Ability(int score)
        {
            if (!score.IsBetween(1, 30)) throw new ArgumentOutOfRangeException(nameof(score));
            Score = score;
        }

        public int Score { get; set; }
        public int Modifier => (int) Math.Floor((Score - 10) / 2.0);
    }
}