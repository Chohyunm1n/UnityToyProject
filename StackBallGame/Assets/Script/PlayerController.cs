using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    
    [Header ("Components")]
    [SerializeField]
    private GameController gameController;
    
    [Header("Player Jump")]
    [SerializeField]
    private float jump = 5;
    [SerializeField]
    private float dropForce = -10;
    
    [Header("Player Jump Sound")]
    [SerializeField]
    private AudioClip jumSound;
    [SerializeField]
    private AudioClip jumpBreakClip;
    [SerializeField]
    private AudioClip powerBreakClip;

    [Header("Player Material")]
    [SerializeField]
    private Material playerMaterial;

    [SerializeField]
    private Transform crashMotion;

    [SerializeField]
    private ParticleSystem[] particle;

    private Vector3 crashMotionValue = new Vector3(0, 0.22f, 0.1f);
    private bool isClicked = false;
    
    private Rigidbody rb;
    private AudioSource audioSource;
    private PlayerPowerMode playerPowerMode;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        playerPowerMode = GetComponent<PlayerPowerMode>();
    }

    private void Update()
    {
        if ( !gameController.IsGamePlay) return;
        
        UpdateMoustButton();
        UpdateDropToSmash();
        
        playerPowerMode.UpdatePowerMode(isClicked);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (!isClicked)
        {
            //중복 충돌 방지
            if (rb.velocity.y > 0) return;

            OnJumpProcess(collision);
        }
        else
        {
            
            //if (collision.gameObject.CompareTag("BreakPart"))
            //{
            //    var platform = collision.transform.parent.GetComponent<PlatformController>();

            //    if (platform.isCollision == false)
            //    {
            //        platform.BreakAllParts();
                    
            //        PlaySound(jumpBreakClip);
                    
            //        gameController.OnCollisionWithPlatform();
                    
            //    }
            //}
            //else if (collision.gameObject.CompareTag("NonBreakPart"))
            //{
            //    rb.isKinematic = true;
                
            //    Debug.Log("Game Over");
            //}

            if (playerPowerMode.IsPowerMode)
            {
                if (collision.gameObject.CompareTag("BreakPart") ||
                    collision.gameObject.CompareTag("NonBreakPart"))
                {
                    OnCollisionWithBreakPart(collision, powerBreakClip, 2);
                }
            }
            else
            {
                if (collision.gameObject.CompareTag("BreakPart"))
                {
                    OnCollisionWithBreakPart(collision, jumpBreakClip, 1);
                }
                else if (collision.gameObject.CompareTag("NonBreakPart"))
                {
                    gameController.GameOver(transform.position);
                    gameObject.SetActive(false);
                    //rb.isKinematic = true;

                    //Debug.Log("Game Over");
                }
            }
            
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        if (rb.velocity.y > 0) return;
        if (isClicked) return;
        OnJumpProcess(collision);
    }

    private void OnJumpProcess(Collision collision)
    {
        rb.velocity = new Vector3 (0, jump, 0);

        PlaySound(jumSound);
        OnCrashMotion(collision.transform);
        OnParticle();
    }
    
    private void PlaySound(AudioClip clip)
    {
        audioSource.Play();
        audioSource.clip = clip;
        audioSource.Play();
    }

    private void OnCrashMotion(Transform target)
    {
        Transform ts = Instantiate(crashMotion, target);
        ts.position = transform.position - crashMotionValue;
        ts.localEulerAngles = new Vector3 (0,0,Random.Range(0,360));
        float randomScale = Random.Range(0.3f, 0.5f);
        ts.localScale = new Vector3(randomScale, randomScale, 1);

        ts.GetComponent<MeshRenderer>().material.color = playerMaterial.color;

    }

    private void OnParticle()
    {
        for (int i = 0; i < particle.Length; i++)
        {
            if (particle[i].gameObject.activeSelf) continue;

            particle[i].gameObject.SetActive(true);

            particle[i].transform.position = transform.position - crashMotionValue;

            var mainModule = particle[i].main;
            mainModule.startColor = playerMaterial.color;
            break;
        }
    }
    
    private void UpdateMoustButton()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isClicked = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isClicked = false;
        }
    }

    private void UpdateDropToSmash()
    {
        if (Input.GetMouseButton(0) && isClicked)
        {
            rb.velocity = new Vector3(0, dropForce, 0);
        }
    }
    
    private void OnCollisionWithBreakPart(Collision collision, AudioClip clip, int addedScore)
    {
        var platform = collision.transform.parent.GetComponent<PlatformController>();

        if (platform.isCollision == false)
        {
            platform.BreakAllParts();

            PlaySound(clip);

            gameController.OnCollisionWithPlatform(addedScore);
        }
    }
    
    
}
