using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public int trapsSpawned = 0;
    public int trapsDestroyed = 0;

    public GameObject EndPanel;
    bool canRestart = false;

    MarekController marek;

    public Image temperatureBar;

    void Start()
    {
        EndPanel.SetActive(false);
        marek = GameObject.FindObjectOfType<MarekController>();
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
        EndPanel.SetActive(true);
        canRestart = true;
        temperatureBar.fillAmount = 0.5f;
    }

    public void ShowEndPanel()
    {
        StartCoroutine(EndScreen());
        Debug.Log("lol");
    }
}
