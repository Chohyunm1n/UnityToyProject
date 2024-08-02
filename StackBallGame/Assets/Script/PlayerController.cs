using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Jump")]
    [SerializeField]
    private float jump = 5;

    [Header("Player Jump Sound")]
    [SerializeField]
    private AudioClip jumSound;


    [Header("Player Material")]
    [SerializeField]
    private Material playerMaterial;

    [SerializeField]
    private Transform crashMotion;

    [SerializeField]
    private ParticleSystem[] particle;

    private Vector3 crashMotionValue = new Vector3(0, 0.22f, 0.1f);


    private Rigidbody rb;

    private AudioSource audioSource;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //중복 충돌 방지
        if (rb.velocity.y > 0) return;


        rb.velocity = new Vector3 (0, jump, 0);

        PlaySound(jumSound);
        //OnCrashMotion(collision.transform);


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
}
