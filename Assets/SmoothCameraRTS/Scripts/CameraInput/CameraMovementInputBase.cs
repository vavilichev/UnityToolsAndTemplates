using UnityEngine;

namespace Vavilichev.UTAT.CameraMovement
{
    public abstract class CameraMovementInputBase : MonoBehaviour
    {
        [SerializeField] protected CameraMovementProperties _properties;

        private ICameraMovementHandler _cameraMovementHandler;
        
        protected virtual void Awake()
        {
            _cameraMovementHandler = CreateMovementHandler();
        }

        private void Update()
        {
            var inputDelta = ReadInputDelta();
            _cameraMovementHandler.Move(inputDelta);
        }

        protected abstract ICameraMovementHandler CreateMovementHandler();
        protected abstract Vector3 ReadInputDelta();
    }
}