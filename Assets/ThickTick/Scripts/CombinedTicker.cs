using System;
using UnityEngine;

namespace Vavilichev.UTAT.ThickTick
{
    public class CombinedTicker : MonoBehaviour
    {
        public event Action<float> OnFrameTick;
        public event Action OnSecondTick;

        private bool _isUnscaledTime;
        private float _localSecondTimer;

        public static CombinedTicker Create(bool isUnscaledTime = false)
        {
            var mono = new GameObject("[TICKER]");
            var ticker = mono.AddComponent<CombinedTicker>();
            ticker._isUnscaledTime = isUnscaledTime;

            return ticker;
        }
        
        private void Update()
        {
            var dt = _isUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
            OnFrameTick?.Invoke(dt);
            
            _localSecondTimer += dt;
            if (_localSecondTimer > 1)
            {
                _localSecondTimer -= 1;
                OnSecondTick?.Invoke();
            }
        }
    }
}