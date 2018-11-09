using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    public GameObject groundTile;
    public float segmentLenght;

    public int levelLenght = 20;

    void Start()
    {
        LayoutGround();
    }

    void Update()
    {
        
    }

    void LayoutGround()
    {

        for(int i = 0; i < levelLenght; i++)
        {
            Instantiate(groundTile, new Vector3(segmentLenght * i, 0, 0), Quaternion.identity);
        }
    }
}
