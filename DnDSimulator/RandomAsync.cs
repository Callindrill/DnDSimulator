using System;
using System.Threading;
using System.Threading.Tasks;
using DnDSimulator.Interfaces;

namespace DnDSimulator
{
    public class RandomAsync : IRandomAsync
    {
        private readonly Random _random = new Random();
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        public async Task<int> NextAsync()
        {
            await _semaphore.WaitAsync();
            try
            {
                return _random.Next();
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<int> NextAsync(int maxValue)
        {
            await _semaphore.WaitAsync();
            try
            {
                return _random.Next(maxValue);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<int> NextAsync(int minValue, int maxValue)
        {
            await _semaphore.WaitAsync();
            try
            {
                return _random.Next(minValue, maxValue);
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<double> NextDoubleAsync()
        {
            await _semaphore.WaitAsync();
            try
            {
                return _random.NextDouble();
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task NextBytesAsync(byte[] buffer)
        {
            await _semaphore.WaitAsync();
            try
            {
                _random.NextBytes(buffer);
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}