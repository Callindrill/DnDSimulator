using System;
using System.Collections.Generic;
using System.Text;
using DnDSimulator.Encounter;
using DnDSimulator.Interfaces;

namespace DnDSimulator.Behavior
{
    class ActionDecision : IActionDecision
    {
        public ActionType ActionType { get; set; }
        public IActor TargetActor { get; set; }
    }
}
