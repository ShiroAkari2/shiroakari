using UnityEngine;
public class RunePuzzle:MonoBehaviour,IInteractable
{
    public GameObject door; public string questId="restore_seal"; bool solved;
    public string Prompt=>solved?"The ancient seal is restored":"Activate the rune";
    public void Interact(){if(solved)return;solved=true;if(door)door.SetActive(false);if(QuestManager.Instance){QuestManager.Instance.Complete(questId);QuestManager.Instance.SetObjective("The sealed path is open. Continue exploring!");}Debug.Log("The runes resonate. The way forward is open!");}
}
