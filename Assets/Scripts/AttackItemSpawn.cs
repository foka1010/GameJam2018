using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackItemSpawn : MonoBehaviour
{
    
    public GameObject[] items;
    public float deathTime = 3f;
    private Transform thisTransform;
    private GameObject currentSpawn;
    private GameObject spawnedItem;
    private int spawnNumber;

    private float spawnTime;
    private float time;
    public float minTime = 1f;
    public float maxTime =3f;


    // Start is called before the first frame update
    void Start()
    {
        thisTransform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        spawnNumber = Random.Range(0, items.Length);
        currentSpawn = items[spawnNumber];
        //spawnedItem= Instantiate (items[spawnNumber], thisTransform);
        // spawnedItem
  
    }

    void FixedUpdate()
    {

        //Counts up
        time += Time.deltaTime;

        //Check if its the right time to spawn the object
        if (time >= spawnTime)
        {
            SpawnObject();
            SetRandomTime();
        

        }
      //  DetachChildren();
        WaitAndDestroy();

    }

    /// <summary>
    /// //......functions
    /// </summary>

    void WaitAndDestroy()
    {
        if (spawnedItem != null)
        {
            Destroy(spawnedItem, deathTime);
        }
    }

    void DetachChildren()
    {
        spawnedItem.transform.parent = null;
    }

    void SpawnObject() {
        time = 0;
        spawnedItem = Instantiate(items[spawnNumber], thisTransform);
        spawnedItem.transform.parent = null;

    }

    void SetRandomTime()
    {
        spawnTime = Random.Range(minTime, maxTime);
    }

}



