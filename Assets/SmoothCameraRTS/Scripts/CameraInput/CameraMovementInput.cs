using UnityEngine;

namespace  Vavilichev.UTAT.CameraMovement
{
    [RequireComponent(typeof(Camera))]
    public class CameraMovementInput : CameraMovementInputBase
    {
        [SerializeField] private LayerMask _raycastMask;
        
        private Camera _camera;
        private bool _dragEnabled;

        protected override void Awake()
        {
            base.Awake();
            
            _camera = GetComponent<Camera>();
        }

        protected override ICameraMovementHandler CreateMovementHandler()
        {
            return new SmoothCameraMovementHandler(_properties);
        }

        protected override Vector3 ReadInputDelta()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (IsClickedOnGround())
                {
                    _dragEnabled = true;
                }
                return Vector3.zero;
            }

            if (Input.GetMouseButtonUp(0))
            {
                _dragEnabled = false;
                return Vector3.zero;
            }
            
            if (_dragEnabled && Input.GetMouseButton(0))
            {
                return Input.mousePositionDelta;
            }

            return Vector3.zero;
        }

        private bool IsClickedOnGround()
        {
            var pointerScreenPosition = Input.mousePosition;
            var ray = _camera.ScreenPointToRay(pointerScreenPosition);
            var result = Physics.Raycast(ray, out var hit, float.MaxValue, _raycastMask.value);

            if (!result)
            {
                return false;
            }

            if (IsLayerInMask(hit.collider.gameObject.layer, _raycastMask))
            {
                return true;
            }

            return false;
        }
        
        private bool IsLayerInMask(int layer, LayerMask mask)
        {
            return (mask.value & (1 << layer)) != 0;
        }
    }
}