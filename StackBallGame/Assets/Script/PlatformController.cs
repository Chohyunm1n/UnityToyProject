using System.Collections;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField]
    private PlatformPartController[] parts;
    [SerializeField]
    private float removeDuration = 1;

    public bool isCollision {  private set; get; } = false;


    public void BreakAllParts()
    {
        if (isCollision == false)
        {
            isCollision = true;
        }

        if (transform.parent != null)
        {
            transform.parent = null;
        }

        foreach (PlatformPartController part in parts)
        {
            part.BreakingPart();
        }

        StartCoroutine(nameof(RemoveParts));
    }

    private IEnumerator RemoveParts()
    {
        yield return new WaitForSeconds(removeDuration);

        gameObject.SetActive(false);
    }
}
