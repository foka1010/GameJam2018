using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSMIScript : MonoBehaviour
{
    public GameObject marek;
    LineRenderer LSMI;
    public ParticleSystem hitEffect;
    public GameObject hitEffectGO;

    public GameObject lol;
    public Transform laserOrigin;
    public float offset;

    void Start()
    {
        LSMI = GetComponent<LineRenderer>();
    }


    void Update()
    {
        Laser();
    }

    void Laser()
    {
        RaycastHit2D hit = Physics2D.Raycast(laserOrigin.position, Vector2.down);

        Vector3 hitPosition = new Vector3(hit.point.x, hit.point.y, 0);

        //hitEffectGO.transform.localPosition = hitPosition - gameObject.transform.position;
        //Physics2D.queriesStartInColliders = false;


        hitEffectGO.transform.position = hit.point;

        if (Input.GetButton("Jump"))
        {

            hitEffect.Play();
        }
        else
        {
            hitEffect.Stop();
        }

    }
}
