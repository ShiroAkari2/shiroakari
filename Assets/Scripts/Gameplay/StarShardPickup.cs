using UnityEngine;
public class StarShardPickup : MonoBehaviour,IInteractable
{
    public string itemId="Star Shard";
    public string Prompt=>"Collect "+itemId;
    public void Interact(){if(InventorySystem.Instance)InventorySystem.Instance.Add(itemId);gameObject.SetActive(false);}
}
