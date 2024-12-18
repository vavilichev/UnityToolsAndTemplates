using UnityEngine;
using Vavilichev.UTAT.ThickTick;

namespace ThickTick.Example.Scripts
{
    public class ExampleCombinedTicker : MonoBehaviour
    {
        [SerializeField] private bool _isUnscaled;
        
        private CombinedTicker _ticker;
        
        private void Start()
        {
            _ticker = CombinedTicker.Create(_isUnscaled);
            Debug.Log($"Combined ticker is created. Is unscaled: {_isUnscaled}...");
            
            _ticker.OnFrameTick += OnFrameTick;
            _ticker.OnSecondTick += OnSecondTick;
        }

        private void OnDestroy()
        {
            _ticker.OnFrameTick -= OnFrameTick;
            _ticker.OnSecondTick -= OnSecondTick;
        }

        private void OnFrameTick(float deltaTime)
        {
            Debug.Log($"Combined Ticker: On Frame Tick. DeltaTime = {deltaTime}");
        }

        private void OnSecondTick()
        {
            Debug.Log($"Combined Ticker: On Second Tick.");
        }
    }
}