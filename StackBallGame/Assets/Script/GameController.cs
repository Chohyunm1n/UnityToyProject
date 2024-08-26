using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private ObjectSpawn os;
    
    [SerializeField]
    private UIController uiController;
    
    private RandomColor randomColor;


    public bool IsGamePlay { private set; get; } = false;

    private void Awake()
    {
        os.SpawnObject();

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
}
