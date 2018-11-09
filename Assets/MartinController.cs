using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MartinController : MonoBehaviour
{
    public float speed = 5f;
    public float upAndDownSpeed = 5f;
    public float upAndDownRadius = 5f;

    public GameObject trap;

    float height;

    void Start()
    {
        height = transform.position.y;
        StartCoroutine(LeaveTraps());
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
            Instantiate(trap, gameObject.transform.position, Quaternion.identity);
        }
    }
}
