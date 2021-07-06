using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    public Text topScore;
    // Start is called before the first frame update
    void Start()
    {
        ScoreManager.Instance.LoadBestScore();
        topScore.text = (ScoreManager.Instance.bestScoreName == "")?"":"Best Score: " + ScoreManager.Instance.bestScoreName + " : " + ScoreManager.Instance.bestScore;
    }

    public void StartGame() {
        SceneManager.LoadScene("main");
    }

    public void SetPlayerName(string playerName) {
        Regex rgx = new Regex("[^a-zA-Z0-9 -]");
        string strPlayerName = rgx.Replace(playerName, "");
        ScoreManager.Instance.playerName = strPlayerName;
    }

    public void LoadAllScores() {
        SceneManager.LoadScene("scores");
    }

    public void Exit()
    {
        //Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
