using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField] private Transform lastPlatform;

    private float platformWeigth = 4;

    private void Update()
    {
        FollowTarget();
        
    }

    private void FollowTarget()
    {
        if (transform.position.y > target.position.y && transform.position.y > lastPlatform.position.y + platformWeigth)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            
        }
}
}
