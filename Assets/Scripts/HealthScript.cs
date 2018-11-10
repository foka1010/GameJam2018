using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{
    public GameObject heart;
    public int Lives;

    public int offset = 110;
    int currentXPosition;

    public Sprite full;
    public Sprite empty;

    int liveIndex;

    public List<GameObject> heartsList = new List<GameObject>();

    void Start()
    {
        currentXPosition = 0;

        for(int i = 0; i < Lives; i++)
        {
            GameObject hearthGO = Instantiate(heart, gameObject.transform.position, Quaternion.identity) as GameObject;
            hearthGO.transform.SetParent(gameObject.transform);
            hearthGO.GetComponent<RectTransform>().anchoredPosition = new Vector3(currentXPosition, 0, 0);
            currentXPosition -= offset;
            heartsList.Add(hearthGO);
        }

        liveIndex = heartsList.Count - 1;
    }

    public void TakeLive()
    {
        if(liveIndex > -1)
        {
            heartsList[liveIndex].GetComponent<Image>().sprite = empty;
            liveIndex--;
        }
    }

    public void AddLive()
    {
        if(liveIndex < heartsList.Count-1)
        {
            liveIndex++;
            heartsList[liveIndex].GetComponent<Image>().sprite = full;
        }
    }
}
