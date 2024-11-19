using UnityEngine;

namespace Vavilichev.UTAT.CameraZoom
{
    public abstract class CameraZoomController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private ZoomProperties _zoomProperties;

        private IZoomHandler _zoomHandler;

        private void Awake()
        {
            _zoomHandler = _camera.orthographic 
                ? new OrthographicCameraZoomHandler(_camera, _zoomProperties)
                : new PerspectiveCameraZoomHandler(_camera, _zoomProperties);
        }

        private void LateUpdate()
        {
            var inputDelta = ReadInputDelta();

            _zoomHandler.Zoom(inputDelta);
        }

        protected abstract float ReadInputDelta();
    }
}