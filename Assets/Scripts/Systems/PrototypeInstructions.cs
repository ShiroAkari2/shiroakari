using UnityEngine;

public class PrototypeInstructions : MonoBehaviour
{
    void OnGUI()
    {
        GUI.Box(new Rect(Screen.width-255, 15, 240, 125),
            "SHIROAKARI'S PROJECT\nWASD: Move | Shift: Run\nSpace: Jump | E: Interact\nJ: Journal\nF5 Save | F9 Load");
    }
}
