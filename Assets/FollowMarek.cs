using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMarek : MonoBehaviour
{
    public GameObject follow;

    float positionY;

    void Start()
    {
        positionY = follow.transform.position.y;
    }

    void Update()
    {
        Vector3 newPosition = new Vector3(follow.transform.position.x, positionY , -20f);
        gameObject.transform.position = newPosition;
    }
}
