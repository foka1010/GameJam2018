using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public int trapsSpawned = 0;
    public float trapsDestroyed = 0;

    public GameObject EndPanel;
    bool canRestart = false;

    AudioSource gameAudio;

    public List<AudioClip> gameAudios = new List<AudioClip>();


    MarekController marek;

    public Image temperatureBar;
    public Text scoreText;

    void Start()
    {
        EndPanel.SetActive(false);
        marek = GameObject.FindObjectOfType<MarekController>();
        gameAudio = GetComponent<AudioSource>();
        PlayAudio(0);
    }

    void Update()
    {
        RestartGame();
    }

    void RestartGame()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            if(!marek.isAlive)
            {
                if(canRestart)
                {
                    SceneManager.LoadScene(0);
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
        temperatureBar.fillAmount = trapsDestroyed / trapsSpawned;
        scoreText.text = (Mathf.RoundToInt((trapsDestroyed / trapsSpawned) * 100f).ToString() + "%");
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
}
