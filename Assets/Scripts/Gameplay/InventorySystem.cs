using System.Collections.Generic;
using UnityEngine;
public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance {get;private set;}
    readonly Dictionary<string,int> items=new Dictionary<string,int>(); bool visible;
    void Awake(){if(Instance&&Instance!=this){Destroy(gameObject);return;}Instance=this;DontDestroyOnLoad(gameObject);}
    public void Add(string id,int amount=1){if(!items.ContainsKey(id))items[id]=0;items[id]+=amount;Debug.Log("Collected "+id+" x"+amount);}
    public Dictionary<string,int> Snapshot()=>new Dictionary<string,int>(items);
    public void Restore(Dictionary<string,int> data){items.Clear();if(data!=null)foreach(var p in data)items[p.Key]=p.Value;}
    void Update(){if(Input.GetKeyDown(KeyCode.I))visible=!visible;}
    void OnGUI()
    {
        if(!visible)return;
        GUI.Box(new Rect(Screen.width-260,18,240,Mathf.Min(300,60+items.Count*28)),"INVENTORY  [I]");
        int y=52;if(items.Count==0)GUI.Label(new Rect(Screen.width-240,y,200,25),"Your bag is empty.");
        foreach(var p in items){GUI.Label(new Rect(Screen.width-240,y,200,25),p.Key+"  × "+p.Value);y+=27;}
    }
}
