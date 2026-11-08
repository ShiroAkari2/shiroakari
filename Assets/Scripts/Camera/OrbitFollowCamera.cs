using UnityEngine;
public class OrbitFollowCamera : MonoBehaviour
{
    public Transform target; public Vector3 offset=new Vector3(0,2.5f,-5f);
    public float sensitivity=3f, followSmooth=12f, minPitch=-25f, maxPitch=65f;
    float yaw,pitch=12f;
    void Start(){ if(target) yaw=target.eulerAngles.y; SetCursor(true); }
    void Update(){ if(Input.GetKeyDown(KeyCode.Escape)) SetCursor(Cursor.lockState!=CursorLockMode.Locked); }
    void LateUpdate()
    {
        if(!target)return;
        if(Cursor.lockState==CursorLockMode.Locked)
        { yaw+=Input.GetAxis("Mouse X")*sensitivity; pitch-=Input.GetAxis("Mouse Y")*sensitivity; pitch=Mathf.Clamp(pitch,minPitch,maxPitch); }
        Quaternion rot=Quaternion.Euler(pitch,yaw,0);
        transform.position=Vector3.Lerp(transform.position,target.position+rot*offset,followSmooth*Time.deltaTime);
        transform.rotation=rot;
    }
    void SetCursor(bool locked){ Cursor.lockState=locked?CursorLockMode.Locked:CursorLockMode.None; Cursor.visible=!locked; }
}
