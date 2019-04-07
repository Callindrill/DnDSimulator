using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DnDSimulator.Encounter;
using DnDSimulator.Interfaces;

namespace DnDSimulator.Behavior
{
    static class ActionStrategy //This will eventually become the "hit someone else" strategy... For now, it's all there is.
    {
        /// <summary>
        /// Return what the current actor should do given the current state of the encounter.
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="encounter"></param>
        /// <returns></returns>
        public static IActionDecision DecideAction(this IActor actor, IEncounter encounter)
        {
            var chosenOpponent = encounter.Factions
                .Where(f => !f.Participants.SelectMany(p => p).Contains(actor))
                .SelectMany(f => f.Participants)
                .SelectMany(p => p)
                .FirstOrDefault(a => a.HitPoints.CurrentHitPoints > 0);

            return new ActionDecision()
            {
                ActionType = ActionType.Attack,
                TargetActor = chosenOpponent
            };
        }
    }
}
