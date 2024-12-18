using System;
using UnityEngine;

namespace Vavilichev.UTAT.ThickTick
{
    public class FrameTicker : MonoBehaviour
    {
        public event Action<float> OnTick;

        private bool _isUnscaledTime;

        public static FrameTicker Create(bool isUnscaledTime = false)
        {
            var mono = new GameObject("[TICKER]");
            var ticker = mono.AddComponent<FrameTicker>();
            ticker._isUnscaledTime = isUnscaledTime;

            return ticker;
        }
        
        private void Update()
        {
            var dt = _isUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
            OnTick?.Invoke(dt);
        }
    }
}