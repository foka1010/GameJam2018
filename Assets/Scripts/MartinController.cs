using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MartinController : MonoBehaviour
{
    public float speed = 5f;
    public float upAndDownSpeed = 5f;
    public float upAndDownRadius = 5f;

    public GameObject trap;

    public GameObject powerEffect;

    Animator martinAnimator;
    int spellHash = Animator.StringToHash("spell");

    float height;

    void Start()
    {
        height = transform.position.y;
        StartCoroutine(LeaveTraps());

        martinAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        
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
            yield return new WaitForSeconds(2f);
            martinAnimator.SetTrigger(spellHash);
            yield return new WaitForSeconds(0.8f);
            Instantiate(trap, gameObject.transform.position, Quaternion.identity);
            Instantiate(powerEffect, gameObject.transform.position, Quaternion.identity);
        }
    }
}
