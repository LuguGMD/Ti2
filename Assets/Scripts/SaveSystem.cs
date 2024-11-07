using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    /*private void Start()
    {
        PlayerData data = new PlayerData(2);
        SavePlayerData(data);
    }*/

    public void SavePlayerData(PlayerData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/playerData.json", json);
    }

    public PlayerData LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/playerData.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
