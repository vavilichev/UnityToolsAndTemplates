using UnityEngine;

namespace Vavilichev.UTAT.CameraZoom
{
    public class OrthographicCameraZoomHandler : IZoomHandler
    {
        private readonly Camera _camera;
        private readonly ZoomProperties _properties;
        private float _orthoSize;
        private float _velocity;

        public OrthographicCameraZoomHandler(Camera camera, ZoomProperties properties)
        {
            _camera = camera;
            _properties = properties;
            _orthoSize = _camera.orthographicSize;
        }

        public void Zoom(float inputDelta)
        {
            var inputDeltaWithSpeed = inputDelta * _properties.ZoomSpeed;

            _orthoSize = Mathf.Clamp(_orthoSize - inputDeltaWithSpeed, _properties.ZoomMin, _properties.ZoomMax);

            var newOrthoSize = Mathf.SmoothDamp(
                _camera.orthographicSize,
                _orthoSize, 
                ref _velocity,
                _properties.Smoothness);

            _camera.orthographicSize = newOrthoSize;
        }
    }

}
