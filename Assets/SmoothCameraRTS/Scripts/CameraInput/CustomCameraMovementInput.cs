using UnityEngine;

namespace Vavilichev.UTAT.CameraMovement
{
    [RequireComponent(typeof(Camera))]
    public class CustomCameraMovementInput: CameraMovementInputBase
    {
        [SerializeField] private LayerMask _raycastMask;
        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private float _mouseSensitive = 1;
        [SerializeField] private float _keyboardSensitive = 1;
        
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
            var mouseInputDelta = ReadMouseInputDelta();
            var keyboardInputDelta = ReadKeyboardInputDelta();

            return (mouseInputDelta + keyboardInputDelta) * _camera.fieldOfView;
        }

        private Vector3 ReadMouseInputDelta()
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
                return Input.mousePositionDelta * _mouseSensitive;
            }

            return Vector3.zero;
        }

        private Vector3 ReadKeyboardInputDelta()
        {
            var vertical = Input.GetAxis("Vertical");
            var horizontal = Input.GetAxis("Horizontal");

            return new Vector3(-horizontal, -vertical, 0) * _keyboardSensitive;
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

            if (IsLayerInMask(hit.collider.gameObject.layer, _groundMask))
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