using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static int bestScore;
    [SerializeField] private TMP_InputField playerNameText;
    [SerializeField] TextMeshProUGUI bestScoreText;
    public static string playerName;
    public static string bestPlayerName;

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
    
    public void SetPlayerName()
    {
        playerName = playerNameText.text;

    }
    
    public void NewBestScore(int score)
    {
        bestScore = score;
        
        SaveBestScore();
    }

    [System.Serializable]
    class SaveData
    {
        public int bestScore;
        public string playerName;
    }

    public void SaveBestScore()
    {
        SaveData data = new SaveData();
        data.bestScore = bestScore;
        data.playerName = playerName;

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
            bestPlayerName = data.playerName;
            bestScoreText.text = ("Best Score : " + bestPlayerName + " : " + bestScore);
        }
    }
}
