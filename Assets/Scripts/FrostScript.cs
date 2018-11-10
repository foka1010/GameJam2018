using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostScript : MonoBehaviour
{
    public float Speed = 2f;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(gameObject.transform.position.x, 0, 0), Time.deltaTime * Speed);
    }
}
