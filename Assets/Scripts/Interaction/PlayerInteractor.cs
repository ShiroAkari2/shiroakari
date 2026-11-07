using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    public Camera viewCamera;
    public float distance = 3.5f;
    public LayerMask mask = ~0;
    public string currentPrompt = "";

    void Update()
    {
        currentPrompt = "";
        if (!viewCamera) return;
        if (Physics.Raycast(viewCamera.transform.position, viewCamera.transform.forward,
            out RaycastHit hit, distance, mask))
        {
            var target = hit.collider.GetComponentInParent<IInteractable>();
            if (target != null)
            {
                currentPrompt = target.Prompt;
                if (Input.GetKeyDown(KeyCode.E)) target.Interact();
            }
        }
    }

    void OnGUI()
    {
        if (string.IsNullOrEmpty(currentPrompt)) return;
        GUIStyle style = new GUIStyle(GUI.skin.box) { fontSize = 18, alignment = TextAnchor.MiddleCenter };
        GUI.Box(new Rect(Screen.width/2f-150, Screen.height-100, 300, 42), currentPrompt + "  [E]", style);
    }
}
