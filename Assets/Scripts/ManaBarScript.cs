using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBarScript : MonoBehaviour
{
    MarekController marekScript;
    public GameObject Marek;

    Image manaBar;

    void Start()
    {
        manaBar = GetComponent<Image>();
        marekScript = Marek.GetComponent<MarekController>();
    }

    void Update()
    {
        manaBar.fillAmount = countFillAamount();
    }

    float countFillAamount()
    {
        float fillAmount = marekScript.currentManaState / marekScript.maxMana;
        return fillAmount;
    }
}
