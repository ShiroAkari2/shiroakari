using UnityEngine;

public class OrbitFollowCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 2.5f, -5f);
    public float sensitivity = 3f;
    public float followSmooth = 12f;
    public float minPitch = -25f, maxPitch = 65f;
    float yaw, pitch = 12f;

    void Start()
    {
        if (target) yaw = target.eulerAngles.y;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (!target) return;
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            yaw += Input.GetAxis("Mouse X") * sensitivity;
            pitch -= Input.GetAxis("Mouse Y") * sensitivity;
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        }
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 desired = target.position + rotation * offset;
        transform.position = Vector3.Lerp(transform.position, desired, followSmooth * Time.deltaTime);
        transform.rotation = rotation;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool locked = Cursor.lockState != CursorLockMode.Locked;
            Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !locked;
        }
    }
}
