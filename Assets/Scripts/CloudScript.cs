using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour
{
    Vector3 finalPosition;
    ScoreController scoreControllerScript;
    public float min;
    public float max;

    public GameObject destroyEffect;

    void Start()
    {
        scoreControllerScript = GameObject.FindObjectOfType<ScoreController>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Projectile")
        {
            Instantiate(destroyEffect, collision.transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(collision.gameObject);
            scoreControllerScript.trapsDestroyed++;
        }
    }
}
