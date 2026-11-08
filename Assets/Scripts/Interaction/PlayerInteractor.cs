using UnityEngine;
public class PlayerInteractor : MonoBehaviour
{
    public Camera viewCamera; public float distance=3.5f; public LayerMask mask=~0;
    string prompt="";
    void Update()
    {
        prompt="";
        if(!viewCamera)return;
        if(Physics.Raycast(viewCamera.transform.position,viewCamera.transform.forward,out RaycastHit hit,distance,mask))
        {
            IInteractable target=hit.collider.GetComponentInParent<IInteractable>();
            if(target!=null){ prompt=target.Prompt; if(Input.GetKeyDown(KeyCode.E))target.Interact(); }
        }
    }
    void OnGUI()
    {
        if(prompt=="")return;
        GUIStyle s=new GUIStyle(GUI.skin.box){fontSize=17,alignment=TextAnchor.MiddleCenter};
        GUI.Box(new Rect(Screen.width/2f-170,Screen.height-95,340,42),"E  ·  "+prompt,s);
    }
}
