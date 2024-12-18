using UnityEngine;

namespace Vavilichev.UTAT.CameraMovement
{
    public class SmoothCameraMovementHandler : ICameraMovementHandler
    {
        private readonly CameraMovementProperties _properties;
        private Vector3 _cachedCameraPosition;

        public SmoothCameraMovementHandler(CameraMovementProperties  properties)
        {
            _properties = properties;
            _cachedCameraPosition = properties.Pivot.position;
        }

        public void Move(Vector3 inputDelta)
        {
            _cachedCameraPosition -= new Vector3(inputDelta.x, 0, inputDelta.y) * _properties.Speed;

            _properties.Pivot.position = Vector3.Lerp(_properties.Pivot.position, _cachedCameraPosition, Time.deltaTime / _properties.Smoothness);    
        }
    }
}