using UnityEngine;

public class RunePuzzle : MonoBehaviour, IInteractable
{
    public string Prompt => solved ? "Ancient seal restored" : "Activate the rune [E]";
    public GameObject door;
    public string questId = "restore_seal";
    bool solved;

    public void Interact()
    {
        if (solved) return;
        solved = true;
        if (door) door.SetActive(false);
        if (QuestManager.Instance) QuestManager.Instance.Complete(questId);
        Debug.Log("The runes resonate. The way forward is open!");
    }
}
