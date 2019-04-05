using System.Threading.Tasks;

namespace DnDSimulator.Interfaces
{
    public interface IRandomAsync
    {
        Task<int> NextAsync();
        Task<int> NextAsync(int maxValue);
        Task<int> NextAsync(int minValue, int maxValue);
        Task<double> NextDoubleAsync();
        Task NextBytesAsync(byte[] buffer);
    }
}