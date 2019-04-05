using System;
using System.Collections.Generic;
using System.Text;
using DnDSimulator.Interfaces;

namespace DnDSimulator.Character
{
    public class HitPoints : IHitPoints
    {
        public delegate HitPoints Factory(int currentHitPoints, int maxHitPoints, int temporaryHitPoints);
        public int CurrentHitPoints { get; set; }
        public int MaxHitPoints { get; set; }
        public int TemporaryHitPoints { get; set; }

        public HitPoints(int currentHitPoints, int maxHitPoints, int temporaryHitPoints)
        {
            if (maxHitPoints <= 0) throw new ArgumentOutOfRangeException(nameof(maxHitPoints));
            if (temporaryHitPoints < 0) throw new ArgumentOutOfRangeException(nameof(temporaryHitPoints));
            if (!currentHitPoints.IsBetween(0, maxHitPoints)) throw new ArgumentOutOfRangeException(nameof(currentHitPoints));
            CurrentHitPoints = currentHitPoints;
            MaxHitPoints = maxHitPoints;
            TemporaryHitPoints = temporaryHitPoints;
        }

        public void LoseHitPoints(int hitPointsToLose)
        {
            if (TemporaryHitPoints > 0)
            {
                if (hitPointsToLose > TemporaryHitPoints)
                {
                    hitPointsToLose -= TemporaryHitPoints;
                    TemporaryHitPoints = 0;
                }
                else
                {
                    TemporaryHitPoints -= hitPointsToLose;
                    hitPointsToLose = 0;
                }
            }
            CurrentHitPoints = Math.Max(0, CurrentHitPoints - hitPointsToLose);
        }

        public void GainHitPoints(int hitPointsToGain)
        {
            CurrentHitPoints = Math.Max(MaxHitPoints, CurrentHitPoints + hitPointsToGain);
        }

        public void GainTemporaryHitPoints(int temporaryHitPointsToGain, bool overrideExistingTemporaryHitPoints)
        {
            if (TemporaryHitPoints == 0 || overrideExistingTemporaryHitPoints)
                TemporaryHitPoints = temporaryHitPointsToGain;
        }
    }
}
