using UnityEngine;

[CreateAssetMenu(menuName = "ShiroAkari/Knowledge Entry")]
public class KnowledgeEntry : ScriptableObject
{
    public string entryId = "entry_001";
    public string title = "A curious discovery";
    [TextArea(3, 8)] public string body = "Write a short, verified fun fact here.";
    public string category = "Nature";
}
