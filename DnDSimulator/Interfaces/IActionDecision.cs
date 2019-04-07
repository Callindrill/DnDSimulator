using DnDSimulator.Encounter;

namespace DnDSimulator.Interfaces
{
    public interface IActionDecision
    {
        ActionType ActionType { get; set; }
        IActor TargetActor { get; set; }
    }
}