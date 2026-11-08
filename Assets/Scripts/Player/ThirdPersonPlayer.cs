using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonPlayer : MonoBehaviour
{
    public float walkSpeed=4f, runSpeed=7f, jumpHeight=1.5f, gravity=-20f, rotationSpeed=12f;
    public Transform cameraTransform;
    CharacterController controller; Vector3 verticalVelocity;
    void Awake(){ controller=GetComponent<CharacterController>(); }
    void Update()
    {
        float h=Input.GetAxisRaw("Horizontal"), v=Input.GetAxisRaw("Vertical");
        Vector3 move=new Vector3(h,0,v).normalized;
        if(cameraTransform)
        {
            Vector3 f=cameraTransform.forward; f.y=0; f.Normalize();
            Vector3 r=cameraTransform.right; r.y=0; r.Normalize();
            move=(f*v+r*h).normalized;
        }
        if(move.sqrMagnitude>.01f)
            transform.rotation=Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(move),rotationSpeed*Time.deltaTime);
        controller.Move(move*(Input.GetKey(KeyCode.LeftShift)?runSpeed:walkSpeed)*Time.deltaTime);
        if(controller.isGrounded && verticalVelocity.y<0) verticalVelocity.y=-2f;
        if(controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
            verticalVelocity.y=Mathf.Sqrt(jumpHeight*-2f*gravity);
        verticalVelocity.y+=gravity*Time.deltaTime;
        controller.Move(verticalVelocity*Time.deltaTime);
    }
}
