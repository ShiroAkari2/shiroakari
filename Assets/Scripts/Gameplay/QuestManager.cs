using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }
    readonly HashSet<string> completed = new HashSet<string>();
    string latest = "Explore the meadow and find the ancient rune.";

    void Awake() { Instance = this; DontDestroyOnLoad(gameObject); }
    public void Complete(string id)
    {
        if (completed.Add(id)) latest = "Completed: " + id;
    }
    void OnGUI()
    {
        GUI.Box(new Rect(20, Screen.height-75, 360, 48), "JOURNEY  |  " + latest);
    }
}
