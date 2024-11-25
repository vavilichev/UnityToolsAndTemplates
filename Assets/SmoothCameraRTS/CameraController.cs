using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float dragSpeed = 2.0f; // Скорость перемещения камеры
    public float smoothSpeed = 1f;

    private Vector3 _cachedPointerPosition;
    private Vector3 _cachedCameraPosition;
    private bool _isDragging = false;
    private Vector3 _cachedDelta;

    private void Awake()
    {
        _cachedCameraPosition = transform.position;
    }

    void Update()
    {
       
        if (Input.GetMouseButtonDown(0))
        {
            _isDragging = true;
            _cachedPointerPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
        }
        
        if (_isDragging)
        {
            var pointerPosition = Input.mousePosition;
            var pointerMovementDelta = (pointerPosition - _cachedPointerPosition) * Time.deltaTime;
            _cachedCameraPosition -= new Vector3(pointerMovementDelta.x, 0, pointerMovementDelta.y) * dragSpeed;
            _cachedPointerPosition = pointerPosition;
        }
        
        transform.position = Vector3.Lerp(transform.position, _cachedCameraPosition, smoothSpeed * Time.deltaTime);    
    }
}