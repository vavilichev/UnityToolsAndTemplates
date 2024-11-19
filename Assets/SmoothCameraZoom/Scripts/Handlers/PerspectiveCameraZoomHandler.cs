using UnityEngine;

namespace Vavilichev.UTAT.CameraZoom
{
    public class PerspectiveCameraZoomHandler : IZoomHandler
    {
        private readonly Camera _camera;
        private readonly ZoomProperties _properties;
        private float _fov;
        private float _velocity;

        public PerspectiveCameraZoomHandler(Camera camera, ZoomProperties properties)
        {
            _camera = camera;
            _properties = properties;
            _fov = _camera.fieldOfView;
        }

        public void Zoom(float inputDelta)
        {
            var inputDeltaWithSpeed = inputDelta * _properties.ZoomSpeed;

            _fov = Mathf.Clamp(_fov - inputDeltaWithSpeed, _properties.ZoomMin, _properties.ZoomMax);

            var newFov = Mathf.SmoothDamp(
                _camera.fieldOfView,
                _fov, 
                ref _velocity,
                _properties.Smoothness);

            _camera.fieldOfView = newFov;
        }
    }
}