using System.Collections.Generic;
using UnityEngine;
public class KnowledgeJournal : MonoBehaviour
{
    public static KnowledgeJournal Instance {get;private set;}
    [System.Serializable] public class Entry { public string id,title,body,category; }
    readonly List<Entry> entries=new List<Entry>(); bool visible;
    public IReadOnlyList<Entry> Entries=>entries;
    void Awake(){ if(Instance&&Instance!=this){Destroy(gameObject);return;} Instance=this; DontDestroyOnLoad(gameObject); }
    public void Unlock(string id,string title,string body,string category="Nature")
    {
        if(entries.Exists(e=>e.id==id))return;
        entries.Add(new Entry{id=id,title=title,body=body,category=category});
        Debug.Log("Journal discovered: "+title);
    }
    public void Restore(List<Entry> saved){ entries.Clear(); if(saved!=null)entries.AddRange(saved); }
    void Update(){if(Input.GetKeyDown(KeyCode.J))visible=!visible;}
    void OnGUI()
    {
        if(!visible)return;
        GUI.Box(new Rect(18,18,430,Mathf.Min(Screen.height-36,360)),"KNOWLEDGE JOURNAL  [J]  ·  "+entries.Count+" discoveries");
        if(entries.Count==0){GUI.Label(new Rect(38,58,390,40),"No discoveries yet. Explore and interact with glowing objects.");return;}
        int y=55;
        foreach(var e in entries)
        {
            GUI.Label(new Rect(38,y,390,22),"["+e.category+"] "+e.title); y+=22;
            GUI.Label(new Rect(48,y,370,48),e.body); y+=54;
            if(y>Screen.height-55)break;
        }
    }
}
