using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public int trapsSpawned = 0;
    public float trapsDestroyed = 0;

    public Text timeText;
    public GameObject EndPanel;
    public GameObject Instructions;
    bool canRestart = false;
    public float time;

    public Image teta;
    public Image slovensko;

    public List<Sprite> tetaSprites = new List<Sprite>();
    public List<Sprite> slovenskoSprites = new List<Sprite>();

    AudioSource gameAudio;

    public List<AudioClip> gameAudios = new List<AudioClip>();

    MarekController marek;

    public Image temperatureBar;
    public Text scoreText;
    bool gamePaused;
    float score;
    bool gameEded;

    void Start()
    {
        EndPanel.SetActive(false);
        Instructions.SetActive(true);
        marek = GameObject.FindObjectOfType<MarekController>();
        gameAudio = GetComponent<AudioSource>();
        PlayAudio(0);
        StartGame();
    }

    void Update()
    {
        RestartGame();
        if(gamePaused)
        {
            ResumeGame();
        }

        if(!gamePaused)
        {
            time -= Time.deltaTime;
            timeText.text = Mathf.RoundToInt(time).ToString();
        }

        if(time < 0)
        {
            marek.win = true;

            timeText.enabled = false;
            gameEded = true;
            marek.Lives = 0;
        }
    }

    void RestartGame()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            if(!marek.isAlive)
            {
                if(canRestart)
                {
                    SceneManager.LoadScene(1);
                }
            }
        }
    }

    IEnumerator EndScreen()
    {
        yield return new WaitForSeconds(4f);
        PlayAudio(1);
        EndPanel.SetActive(true);
        canRestart = true;

        if (!marek.isAlive)
        {
            if(gameEded)
            {
                score = trapsDestroyed / trapsSpawned;
            }
            else
            {
                score = 0;
            }
        }

        temperatureBar.fillAmount = score;
        scoreText.text = (Mathf.RoundToInt((score) * 100f).ToString() + "%");

        if(score < 0.6)
        {
            teta.sprite = tetaSprites[1];
            slovensko.sprite = slovenskoSprites[1];
        }
        else
        {
            teta.sprite = tetaSprites[0];
            slovensko.sprite = slovenskoSprites[0];
        }
    }

    public void ShowEndPanel()
    {
        StartCoroutine(EndScreen());
        Debug.Log("lol");
    }

    void PlayAudio(int number)
    {
        if (gameAudio.isPlaying)
        {
            gameAudio.Stop();
        }

        gameAudio.clip = gameAudios[number];
        gameAudio.Play();
    }

    void StartGame()
    {
        Time.timeScale = 0;
        gamePaused = true;
    }

    void ResumeGame()
    {
        if(Input.anyKeyDown)
        {
            if (gamePaused)
            {
                Time.timeScale = 1;
                Instructions.SetActive(false);
                gamePaused = false;
            }
        }
    }
}
