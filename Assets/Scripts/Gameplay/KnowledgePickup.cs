using UnityEngine;

public class KnowledgePickup : MonoBehaviour, IInteractable
{
    public string entryId = "meadow_001";
    public string entryTitle = "Why are leaves green?";
    [TextArea(2,5)] public string fact =
        "Chlorophyll absorbs mostly red and blue light, while reflecting more green light.";
    public string Prompt => "Discover: " + entryTitle;

    public void Interact()
    {
        if (KnowledgeJournal.Instance)
            KnowledgeJournal.Instance.Unlock(entryId, entryTitle, fact);
        gameObject.SetActive(false);
    }
}
