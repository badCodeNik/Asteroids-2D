using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Services
{
    public class TimerService
    {
        public async UniTask StartTimer(float delay, Action onComplete)
        {
             await UniTask.Delay(TimeSpan.FromSeconds(delay));
             onComplete?.Invoke();
        }
        
        public async UniTask StartTimer(float delay, Action onComplete, CancellationToken cancellationToken)
        {
            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: cancellationToken);
                onComplete?.Invoke();
            }
            catch (OperationCanceledException)
            {
                // Таймер был отменён
            }
        }
        
        public async UniTask Wait(float seconds, CancellationToken cancellationToken = default)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(seconds), cancellationToken: cancellationToken);
        }
        
        // Зацикленный таймер
        public async UniTask StartLoopTimer(
            float interval, 
            Action onTick, 
            CancellationToken cancellationToken = default)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(interval), cancellationToken: cancellationToken);
                    onTick?.Invoke();
                }
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Loop timer cancelled");
            }
        }
    }
}