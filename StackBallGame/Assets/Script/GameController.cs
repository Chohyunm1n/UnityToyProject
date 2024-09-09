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
    [Header("VFX")]
    [SerializeField]
    private GameObject gameOverEffect;
    
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
        totalPlatformCount = platformSpawner.SpawnObject();
        randomColor = GetComponent<RandomColor>();
        randomColor.ColorHSV();
    }


    private IEnumerator Start()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
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
    
}
