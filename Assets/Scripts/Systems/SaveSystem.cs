using System;using System.Collections.Generic;using UnityEngine;
public class SaveSystem:MonoBehaviour
{
    const string Key="ShiroAkari_Save_v02"; public Transform player;
    [Serializable] class SaveData{public Vector3 position;public List<string> journalIds=new List<string>();public List<string> journalTitles=new List<string>();public List<string> journalBodies=new List<string>();public List<string> journalCategories=new List<string>();public List<string> inventoryIds=new List<string>();public List<int> inventoryCounts=new List<int>();public List<string> completedQuests=new List<string>();}
    void Update(){if(Input.GetKeyDown(KeyCode.F5))Save();if(Input.GetKeyDown(KeyCode.F9))Load();}
    public void Save()
    {
        if(!player)return;var d=new SaveData{position=player.position};
        if(KnowledgeJournal.Instance)foreach(var e in KnowledgeJournal.Instance.Entries){d.journalIds.Add(e.id);d.journalTitles.Add(e.title);d.journalBodies.Add(e.body);d.journalCategories.Add(e.category);}
        if(InventorySystem.Instance)foreach(var p in InventorySystem.Instance.Snapshot()){d.inventoryIds.Add(p.Key);d.inventoryCounts.Add(p.Value);}
        if(QuestManager.Instance)d.completedQuests=QuestManager.Instance.CompletedSnapshot();
        PlayerPrefs.SetString(Key,JsonUtility.ToJson(d));PlayerPrefs.Save();Debug.Log("ShiroAkari save complete.");
    }
    public void Load()
    {
        if(!player||!PlayerPrefs.HasKey(Key))return;var d=JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString(Key));
        var cc=player.GetComponent<CharacterController>();if(cc)cc.enabled=false;player.position=d.position;if(cc)cc.enabled=true;
        var entries=new List<KnowledgeJournal.Entry>();for(int i=0;i<d.journalIds.Count;i++)entries.Add(new KnowledgeJournal.Entry{id=d.journalIds[i],title=d.journalTitles[i],body=d.journalBodies[i],category=d.journalCategories[i]});
        if(KnowledgeJournal.Instance)KnowledgeJournal.Instance.Restore(entries);
        var inv=new Dictionary<string,int>();for(int i=0;i<d.inventoryIds.Count;i++)inv[d.inventoryIds[i]]=d.inventoryCounts[i];
        if(InventorySystem.Instance)InventorySystem.Instance.Restore(inv);
        if(QuestManager.Instance)QuestManager.Instance.Restore(d.completedQuests);
        Debug.Log("ShiroAkari save loaded.");
    }
}
