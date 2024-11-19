using UnityEngine;

namespace Vavilichev.UTAT.CameraZoom
{
    public class ScrollWheelCameraZoomController : CameraZoomController
    {
        protected override float ReadInputDelta()
        {
            return Input.GetAxis("Mouse ScrollWheel");
        }
    }
}