using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    const string SaveKey = "ShiroAkari_PlayerPosition";
    public Transform player;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5)) Save();
        if (Input.GetKeyDown(KeyCode.F9)) Load();
    }

    public void Save()
    {
        if (!player) return;
        PlayerPrefs.SetString(SaveKey, JsonUtility.ToJson(new PositionData { position = player.position }));
        PlayerPrefs.Save();
        Debug.Log("Game saved.");
    }

    public void Load()
    {
        if (!player || !PlayerPrefs.HasKey(SaveKey)) return;
        var data = JsonUtility.FromJson<PositionData>(PlayerPrefs.GetString(SaveKey));
        var cc = player.GetComponent<CharacterController>();
        if (cc) cc.enabled = false;
        player.position = data.position;
        if (cc) cc.enabled = true;
        Debug.Log("Game loaded.");
    }

    [System.Serializable] class PositionData { public Vector3 position; }
}
