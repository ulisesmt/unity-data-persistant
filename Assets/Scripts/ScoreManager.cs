using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public string playerName;
    public int playerScore;
    private string scoresFile = "scores.json";
    public string bestScoreName = "";
    public int bestScore;

    private void Awake()
    {
        if (Instance != null) {
            Destroy(gameObject);
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SaveScore() {

        ScoreData data = new ScoreData();
        data.playerName = playerName;
        data.playerScore = playerScore;

        string json = JsonUtility.ToJson(data);

        /*Regex rgx = new Regex("[^a-zA-Z0-9 -]");
        string strPlayerName = rgx.Replace(playerName, "");
        string fileName = strPlayerName.Trim() + ".json";*/
        //File.AppendAllText(Application.persistentDataPath + "/" + scoresFile, json);
        File.AppendAllText(Application.persistentDataPath + "/" + scoresFile, json + "|");
        LoadTopScores();


    }

    public void LoadBestScore() {

        string path = Application.persistentDataPath + "/" + scoresFile;

        if(File.Exists(path)){
            string scoresLines = File.ReadAllText(path);
            string[] scoreJson = scoresLines.Split('|');
            string firstScore = scoreJson.First();
            ScoreData data = JsonUtility.FromJson<ScoreData>(firstScore);
            bestScoreName = data.playerName;
            bestScore = data.playerScore;
            /*foreach (var json in scoreJson)
            {
                if(!string.IsNullOrEmpty(json))   
                {
                    Debug.Log(json);
                    ScoreData data = JsonUtility.FromJson<ScoreData>(json);
                    Debug.Log(data.playerName);
                    Debug.Log(data.playerScore);
                }
                
            }*/

        }
    }

    public string LoadTopScores() {
        var scoreList = new List<KeyValuePair<string, int>>();
        string scoreListText = "";
        //IDictionary<string, int> scoreList = new Dictionary<string, int>();
        string path = Application.persistentDataPath + "/" + scoresFile;
        if (File.Exists(path))
        {
            string scoresLines = File.ReadAllText(path);
            string[] scoreJson = scoresLines.Split('|');
            foreach (var json in scoreJson)
            {
                if (!string.IsNullOrEmpty(json))
                {
                    ScoreData data = JsonUtility.FromJson<ScoreData>(json);
                    scoreList.Add(new KeyValuePair<string, int>(data.playerName, data.playerScore));
                    
                }

            }
            scoreList.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));
            File.Delete(path);
            int i = 0;
            foreach (KeyValuePair<string, int> sc in scoreList)
            {
                
                scoreListText = scoreListText + sc.Key + " : " + sc.Value + "\n";
                ScoreData dataP = new ScoreData();
                dataP.playerName = sc.Key;
                dataP.playerScore = sc.Value;
                string jsonP = JsonUtility.ToJson(dataP);
                File.AppendAllText(path, jsonP + "|");
                i++;
                if (i == 10) { break; }

            }
        }
        return scoreListText;
    }
}
