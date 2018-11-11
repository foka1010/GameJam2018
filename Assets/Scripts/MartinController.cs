using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MartinController : MonoBehaviour
{
    public float speed = 5f;
    public float upAndDownSpeed = 5f;
    public float upAndDownRadius = 5f;

    public float minTimeToWait = 1f;
    public float maxTimeToWait = 3f;


    ScoreController scoreScript;
    public GameObject scoreControllerGO;

    public List<GameObject> traps = new List<GameObject>();

    public GameObject powerEffect;
    Coroutine martinCoroutine;
    Coroutine martinAudioCoroutine;

    float height;
    MarekController marek;

    Animator martinAnimator;
    int spellHash = Animator.StringToHash("spell");

    AudioSource MartinAudioSource;
    public List<AudioClip> MartinAudios = new List<AudioClip>();

    bool isCharging;

    void Start()
    {
        MartinAudioSource = GetComponent<AudioSource>();
        scoreScript = scoreControllerGO.GetComponent<ScoreController>();
        height = transform.position.y;
        martinCoroutine = StartCoroutine(LeaveTraps());
        martinAudioCoroutine = StartCoroutine(PlayRandomSound());
        marek = GameObject.FindObjectOfType<MarekController>();
        martinAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        if(!marek.isAlive)
        {
            StopCoroutine(martinCoroutine);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        transform.position = new Vector3(transform.position.x + speed * Time.deltaTime ,height + Mathf.Sin(Time.time*upAndDownSpeed) * upAndDownRadius, 0.0f);
    }


    IEnumerator LeaveTraps()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(minTimeToWait,maxTimeToWait));
            martinAnimator.SetTrigger(spellHash);
            yield return new WaitForSeconds(0.8f);

            int randomIndex = Random.Range(0, traps.Count);
            Instantiate(traps[randomIndex], gameObject.transform.position, Quaternion.identity);
            isCharging = true;
            PlayAudio(randomIndex);
            Instantiate(powerEffect, gameObject.transform.position, Quaternion.identity);
            AddTrap();
            yield return new WaitForSeconds(0.5f);
            isCharging = false;
        }
    }

    void AddTrap()
    {
        scoreScript.trapsSpawned++;
    }

    void PlayAudio(int number)
    {
        if (MartinAudioSource.isPlaying)
        {
            MartinAudioSource.Stop();
        }

        MartinAudioSource.clip = MartinAudios[number];
        MartinAudioSource.Play();
    }

    IEnumerator PlayRandomSound()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(4f, 7f));
            if(!isCharging)
            {
                PlayAudio(Random.Range(3, 7));
            }
        }
    }
}
