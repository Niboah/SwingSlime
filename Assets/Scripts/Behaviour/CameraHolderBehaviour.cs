using UnityEngine;
using UnityEngine.UI;

public class CameraHolderBehaviour : MonoBehaviour
{
    public float sensitivity = 0.3f;
    public float rayDistance = 100f;
    public LayerMask rayColisionLayer;
    public RawImage crosshair;

    private Camera _shootCamera;
    private RaycastHit _hit;

    void Start()
    {
        _shootCamera = GetComponentInChildren<Camera>();
    }

    public void UpdateCamera(Vector3 rotation)
    {
        if (_shootCamera == null) return;

        Vector3 objectRotation = _shootCamera.transform.rotation.eulerAngles;

        float temp = objectRotation.x + (rotation.y * -sensitivity);
        if (temp > 60 && temp < 90) objectRotation.x = 60;
        else if (temp < 300 && temp > 270) objectRotation.x = 300;
        else objectRotation.x = temp;

        objectRotation.y += rotation.x * sensitivity;
        objectRotation.z = 0;

        _shootCamera.transform.rotation = Quaternion.Euler(objectRotation);

        shootRayCast();
    }

    private void shootRayCast() 
    {
        if (Physics.Raycast(_shootCamera.transform.position, _shootCamera.transform.forward, out _hit, rayDistance, rayColisionLayer))
        {
            Debug.DrawRay(_shootCamera.transform.position, _shootCamera.transform.forward * _hit.distance, Color.green, 0.1f);
            crosshair.color = Color.green;
        }
        else
        {
            Debug.DrawRay(_shootCamera.transform.position, _shootCamera.transform.forward * 50f, Color.red, 0.1f);
            crosshair.color = Color.red;
        }
    }

    public bool GetRaycastHit(out RaycastHit _hit)
    {
        return Physics.Raycast(_shootCamera.transform.position, _shootCamera.transform.forward, out _hit, rayDistance, rayColisionLayer);
    }

    public Vector3 GetDirection() 
    {
        return _shootCamera.transform.forward;
    }
}
