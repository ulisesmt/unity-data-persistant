using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoresUI : MonoBehaviour
{
    public Text sList;
    // Start is called before the first frame update
    void Start()
    {
        string scoreListTxt = ScoreManager.Instance.LoadTopScores();
        sList.text = scoreListTxt;

    }

    public void BackMenu()
    {
        SceneManager.LoadScene("menu");
    }

}
