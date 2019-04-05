namespace DnDSimulator.Interfaces
{
    public interface IHitPoints
    {
        int CurrentHitPoints { get; set; }
        int MaxHitPoints { get; set; }
        int TemporaryHitPoints { get; set; }
        void LoseHitPoints(int hitPointsToLose);
        void GainHitPoints(int hitPointsToGain);
        void GainTemporaryHitPoints(int temporaryHitPointsToGain, bool overrideExistingTemporaryHitPoints);
    }
}