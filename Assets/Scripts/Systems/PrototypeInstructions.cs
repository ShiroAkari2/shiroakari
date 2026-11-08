using UnityEngine;
public class PrototypeInstructions:MonoBehaviour
{
    void OnGUI(){GUI.Box(new Rect(Screen.width-265,15,250,175),"SHIROAKARI'S PROJECT\nWASD: Move · Shift: Run\nSpace: Jump · Mouse: Look\nE: Interact · J: Journal\nI: Inventory\nF5: Save · F9: Load\nEsc: Toggle Cursor");}
}
