using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private ObjectSpawn os;

    private void Awake()
    {
        os.SpawnObject();
    }
}
