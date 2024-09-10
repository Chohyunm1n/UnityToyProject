using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [Header("Common")]
    [SerializeField] private TextMeshProUGUI currentLevel;
    
    [SerializeField] private TextMeshProUGUI nextLevel;
    
    
    [Header("Main")]
    [SerializeField]
    private GameObject mainPanel;

    [Header("InGame")]
    [SerializeField]
    private Image levelProgressBar;

    [SerializeField]
    private TextMeshProUGUI currentScore;

    
    [Header("GameOver")]
    [SerializeField]
    private GameObject gameOverPanel;
    
    [SerializeField]
    private TextMeshProUGUI textCurrentScore;
    [SerializeField]
    private TextMeshProUGUI textHighScore;
    
    [Header("GameClear")]
    [SerializeField]
    private GameObject gameClearPanel;

    [SerializeField]
    private TextMeshProUGUI textLevelCompleted;


    private void Awake()
    {
        currentLevel.text = (PlayerPrefs.GetInt("LEVEL") + 1).ToString();
        nextLevel.text = (PlayerPrefs.GetInt("LEVEL") + 2).ToString();

        if (PlayerPrefs.GetInt("DEACTIVATEMAIN") == 0)
        {
            mainPanel.SetActive(true);
        }
        else
        {
            mainPanel.SetActive(false);
        }
    }
    public void GameStart()
    {
        mainPanel.SetActive(false);
    }

    public void GameOver(int currentScore)
    {
        textCurrentScore.text = $"SCORE\n{currentScore}";
        textHighScore.text = $"HIGHSCORE\n{PlayerPrefs.GetInt("HIGHSCORE")}";
        
        gameOverPanel.SetActive(true);
        
        PlayerPrefs.SetInt("DEACTIVATEMAIN", 0);
    }

    public void GameClear()
    {
        textLevelCompleted.text = $"LEVEL {PlayerPrefs.GetInt("LEVEL") + 1}\nCOMPLETED!";
        
        gameClearPanel.SetActive((true));
        
        PlayerPrefs.SetInt("DEACTIVATEMAIN", 1);

    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("DEACTIVATEMAIN", 0);
    }

    public float LevelProgressBar { set => levelProgressBar.fillAmount = value; }
    
    public int CurrentScore {set => currentScore.text = value.ToString();}
}
