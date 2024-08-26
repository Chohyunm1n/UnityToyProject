using UnityEngine;

public class PlatformPartController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1.5f;

    private MeshRenderer meshRenderer;
    private new Rigidbody rigidbody;
    private new Collider collider;

    private void Awake()
    {
        
    }

    public void BreakingPart()
    {

    }
}
