using UnityEngine;

namespace Vavilichev.UTAT.CameraMovement
{
    public interface ICameraMovementHandler
    {
        void Move(Vector3 inputDelta);
    }
}