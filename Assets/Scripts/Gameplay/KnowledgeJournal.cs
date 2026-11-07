using System.Collections.Generic;
using UnityEngine;

public class KnowledgeJournal : MonoBehaviour
{
    public static KnowledgeJournal Instance { get; private set; }
    readonly HashSet<string> unlocked = new HashSet<string>();
    string latestTitle = "", latestBody = "";
    bool showJournal;

    void Awake()
    {
        if (Instance && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Unlock(string id, string title, string body)
    {
        if (!unlocked.Add(id)) return;
        latestTitle = title; latestBody = body;
        Debug.Log("Journal discovered: " + title);
    }

    void Update() { if (Input.GetKeyDown(KeyCode.J)) showJournal = !showJournal; }

    void OnGUI()
    {
        if (!showJournal) return;
        GUI.Box(new Rect(20, 20, 390, 210), "KNOWLEDGE JOURNAL  [J]");
        GUI.Label(new Rect(40, 55, 350, 30), "Discovered entries: " + unlocked.Count);
        GUI.Label(new Rect(40, 90, 350, 30), latestTitle);
        GUI.Label(new Rect(40, 120, 350, 90), latestBody);
    }
}
