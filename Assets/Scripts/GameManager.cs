using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int bestScore;
    public static TextMeshProUGUI playerName;

    private void Awake()
    {
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadBestScore();
    }
    [System.Serializable]
    class SaveData
    {
        public int bestScore;
    }

    public void SaveBestScore()
    {
        SaveData data = new SaveData();
        data.bestScore = bestScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadBestScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestScore = data.bestScore;
        }
    }
}
