using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private ObjectSpawn os;

    private RandomColor randomColor;


    private void Awake()
    {
        os.SpawnObject();

        randomColor = GetComponent<RandomColor>();
        randomColor.ColorHSV();
    }

}
