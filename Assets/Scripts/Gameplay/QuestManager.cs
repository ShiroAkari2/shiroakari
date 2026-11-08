using System.Collections.Generic;
using UnityEngine;
public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance{get;private set;}
    readonly HashSet<string> completed=new HashSet<string>();
    string objective="Speak with the Meadow Keeper, then investigate the ancient rune.";
    void Awake(){Instance=this;DontDestroyOnLoad(gameObject);}
    public void SetObjective(string value){objective=value;}
    public void Complete(string id){if(completed.Add(id))objective="Completed: "+id;}
    public List<string> CompletedSnapshot()=>new List<string>(completed);
    public void Restore(List<string> values){completed.Clear();if(values!=null)foreach(string v in values)completed.Add(v);}
    void OnGUI(){GUI.Box(new Rect(18,Screen.height-76,410,48),"JOURNEY  ·  "+objective);}
}
