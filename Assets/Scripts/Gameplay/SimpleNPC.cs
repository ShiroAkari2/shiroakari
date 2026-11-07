using UnityEngine;

public class SimpleNPC : MonoBehaviour, IInteractable
{
    [TextArea(2,6)] public string dialogue =
        "Welcome, traveler. The old meadow remembers every footstep. Seek the rune beyond the stones.";
    public string Prompt => "Talk";
    bool show;

    public void Interact() => show = !show;

    void OnGUI()
    {
        if (!show) return;
        GUI.Box(new Rect(Screen.width/2f-220, Screen.height-190, 440, 110), "MEADOW KEEPER");
        GUI.Label(new Rect(Screen.width/2f-200, Screen.height-155, 400, 65), dialogue);
        if (GUI.Button(new Rect(Screen.width/2f+130, Screen.height-95, 70, 25), "Close")) show = false;
    }
}
