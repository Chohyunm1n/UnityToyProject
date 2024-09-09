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
    
    

    private void Awake()
    {
        currentLevel.text = (PlayerPrefs.GetInt("LEVEL") + 1).ToString();
        nextLevel.text = (PlayerPrefs.GetInt("LEVEL") + 2).ToString();
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
    }
    public float LevelProgressBar { set => levelProgressBar.fillAmount = value; }
    
    public int CurrentScore {set => currentScore.text = value.ToString();}
}
