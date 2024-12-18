using System;
using UnityEngine;

namespace Vavilichev.UTAT.ThickTick
{
    public class SecondsTicker : MonoBehaviour
    {
        public event Action OnTick;

        private bool _isUnscaledTime;
        private float _localSecondTimer;

        public static SecondsTicker Create(bool isUnscaledTime = false)
        {
            var mono = new GameObject("[TICKER]");
            var ticker = mono.AddComponent<SecondsTicker>();
            ticker._isUnscaledTime = isUnscaledTime;

            return ticker;
        }
        
        private void Update()
        {
            var dt = _isUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
            
            _localSecondTimer += dt;
            if (_localSecondTimer > 1)
            {
                _localSecondTimer -= 1;
                OnTick?.Invoke();
            }
        }
    }
}