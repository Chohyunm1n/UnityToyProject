using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private PlatformSpawner platformSpawner;
    
    [SerializeField]
    private UIController uiController;

    [Header("SFX")]
    [SerializeField]
    private AudioClip gameOverClip;
    [SerializeField]
    private AudioClip gameClearClip;
    
    [Header("VFX")]
    [SerializeField]
    private GameObject gameOverEffect;
    [SerializeField] 
    private GameObject gameClearEffect;
    
    private RandomColor randomColor;
    private AudioSource audioSource;
    private int brokePlatformCount = 0; // 현재 부서진 플랫폼 수
    private int totalPlatformCount = 0; //전체
    private int currentScore = 0; //현재 점수
    
    public bool IsGamePlay { private set; get; } = false;

    private void Awake()
    {
        //platformSpawner.SpawnObject();
        audioSource = GetComponent<AudioSource>();
        currentScore = PlayerPrefs.GetInt("CURRENTSCORE");
        uiController.CurrentScore = currentScore;
        totalPlatformCount = platformSpawner.SpawnObject();
        randomColor = GetComponent<RandomColor>();
        randomColor.ColorHSV();
    }


    private IEnumerator Start()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0) ||         PlayerPrefs.GetInt("DEACTIVATEMAIN") == 1)
            {
                GameStart();
                yield break;
            }
            
            yield return null;
        }
    }

    private void GameStart()
    {
        IsGamePlay = true;
        uiController.GameStart();
    }

    public void GameOver(Vector3 position)
    {
        IsGamePlay = false;
        
        audioSource.clip = gameOverClip;
        audioSource.Play();
        gameOverEffect.transform.position = position;
        gameOverEffect.SetActive(true);

        UpdateHighScore();
        
        uiController.GameOver(currentScore);

        PlayerPrefs.SetInt("CURRENTSCORE", 0);
        StartCoroutine(nameof(SceneLoadToOnClick));
        
    }

    public void GameClear()
    {
        IsGamePlay = false;

        audioSource.clip = gameClearClip;
        audioSource.Play();
        gameClearEffect.SetActive(true);
        
        UpdateHighScore(); 
        uiController.GameClear();
        
        PlayerPrefs.SetInt("LEVEL", PlayerPrefs.GetInt("LEVEL")+1);

        PlayerPrefs.SetInt("CURRENTSCORE", currentScore);
        StartCoroutine(nameof(SceneLoadToOnClick));
        
    }
    public void OnCollisionWithPlatform(int addedScore = 1)
    {
        brokePlatformCount++;
        uiController.LevelProgressBar = (float)brokePlatformCount / (float)totalPlatformCount;
        
        currentScore += addedScore;
        uiController.CurrentScore = currentScore;
    }
    
    private void UpdateHighScore()
    {
        if (currentScore > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HIGHSCORE", currentScore);
        }
    }

    private IEnumerator SceneLoadToOnClick()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }

            yield return null;
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("CURRENTSCORE",0);
    }

    [ContextMenu("Reset All PlayerPrefs")]
    private void ResetAll()
    {
        PlayerPrefs.DeleteAll();
    }
}
