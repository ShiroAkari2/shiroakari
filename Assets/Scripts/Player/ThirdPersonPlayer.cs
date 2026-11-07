using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonPlayer : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 4f;
    public float runSpeed = 7f;
    public float jumpHeight = 1.5f;
    public float gravity = -20f;
    public float rotationSpeed = 12f;
    public Transform cameraTransform;

    CharacterController controller;
    Vector3 verticalVelocity;

    void Awake() => controller = GetComponent<CharacterController>();

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 input = new Vector3(h, 0, v).normalized;
        Vector3 move = input;

        if (cameraTransform)
        {
            Vector3 forward = cameraTransform.forward; forward.y = 0; forward.Normalize();
            Vector3 right = cameraTransform.right; right.y = 0; right.Normalize();
            move = (forward * v + right * h).normalized;
        }

        if (move.sqrMagnitude > 0.01f)
        {
            Quaternion target = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, rotationSpeed * Time.deltaTime);
        }

        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        controller.Move(move * speed * Time.deltaTime);

        if (controller.isGrounded && verticalVelocity.y < 0) verticalVelocity.y = -2f;
        if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
            verticalVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);
    }
}
