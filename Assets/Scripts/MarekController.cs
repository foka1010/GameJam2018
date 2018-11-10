﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarekController : MonoBehaviour
{
    ScoreController scoreControllerScript;
    public GameObject scoreControllerGO;
    FreeParallax paralaxScript;
    public GameObject deathEffect;

    public bool isAlive = true;
    public int Lives = 5;

    AudioSource MarekAudioSource;
    public GameObject booEffect;

    Rigidbody2D marekRb;
    LineRenderer Beam;
    public float jumpForce = 10f;
    public float speed = 5f;
    float currentSpeed;

    public GameObject chargeAttackEffect;
    public Transform groundCheck;
    public Transform gunPosition;
    public LayerMask whatIsGround;
    public bool grounded = false;
    bool justGrounded = false;

    public GameObject apple;
    public float appleManaCost = 100f;
    public GameObject shootEffect;
    public GameObject jumpEffect;

    public ParticleSystem LSMI;

    public float maxMana  = 1000f;
    public float currentManaState;

    public float rechargeSpeed = 5f;

    public bool CanDoLSMI;
    public float LSMIManaCost = 250f;
    bool isDoingLSMI = false;

    public GameObject hitEffectGO;
    public ParticleSystem hitEffect;

    public float LSMICharge = 1f;

    public float floatingForce = 1f;
    float currentLSMIChargeTime;

    public bool isDoingSomething;
    public float hitKickForce = 7f;

    public Vector2 shotForce = new Vector2(1000, 500);
    public float chargeDistance = 2f;
    public LayerMask whatIsFog;
    public GameObject fogDestroyEffect;
    public GameObject frostDestroyEffect;

    HealthScript healthScript;
    public GameObject hsGO;

    public float chargeCost = 200f;

    void Start()
    {
        healthScript = hsGO.GetComponent<HealthScript>();
        scoreControllerScript = scoreControllerGO.GetComponent<ScoreController>();
        Beam = GetComponent<LineRenderer>();
        marekRb = GetComponent<Rigidbody2D>();
        currentManaState = maxMana;
        MarekAudioSource = GetComponent<AudioSource>();
        currentLSMIChargeTime = LSMICharge;
        currentSpeed = speed;
        paralaxScript = GameObject.FindObjectOfType<FreeParallax>();
    }

    void Update()
    {
        if(isAlive)
        {
            paralaxScript.Speed = -marekRb.velocity.x;
            DoJump();
            ShootApple();
            RechargeMana();
            LumosSolemMaximaIncantatem();
            ChargeAttack();
            Die();
        }
    }

    private void FixedUpdate()
    {
        if (isAlive)
        {
            Move();
        }
    }

    void DoJump()
    {
        grounded = Physics2D.OverlapPoint(groundCheck.position, whatIsGround);

        if (grounded)
        {
            if (justGrounded == false)
            {
                justGrounded = true;
                Instantiate(jumpEffect, groundCheck.position, Quaternion.identity);
                CanDoLSMI = false;
                LSMI.Stop();
                isDoingSomething = false;
                ResetCharge();
            }
        }
        else
        {
            justGrounded = false;
        }

        grounded = Physics2D.OverlapPoint(groundCheck.position, whatIsGround);

        if (Input.GetButtonDown("Jump"))
        {
            if(grounded)
            {
                Instantiate(jumpEffect, groundCheck.position, Quaternion.identity);
                marekRb.velocity = new Vector2(marekRb.velocity.x, jumpForce);
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            if(!grounded)
            {
                CanDoLSMI = true;
            }
        }
    }

    void Move()
    {
        marekRb.velocity = new Vector2(currentSpeed, marekRb.velocity.y);
    }

    void ShootApple()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if(currentManaState > appleManaCost)
            {
                GameObject instance = Instantiate(shootEffect, gunPosition.position, Quaternion.identity);
                instance.transform.SetParent(gameObject.transform);

                GameObject projectile = Instantiate(apple, gunPosition.position, Quaternion.identity);
                projectile.GetComponent<Rigidbody2D>().AddForce(shotForce);

                currentManaState -= appleManaCost;
            }
        }
    }

    void RechargeMana()
    {
        if(currentManaState < maxMana)
        {
            if(!isDoingSomething)
            {
                currentManaState += rechargeSpeed * Time.deltaTime;
            }
        }
    }

    void LumosSolemMaximaIncantatem()
    {
        if(grounded == false)
        {
            if(currentManaState > 0)
            {
                if (Input.GetButton("Jump"))
                {
                    if (CanDoLSMI)
                    {
                        Charge();
                        if(currentLSMIChargeTime < 0)
                        {
                            if(isDoingLSMI == false)
                            {
                                isDoingSomething = true;
                                isDoingLSMI = true;
                                LSMI.Play();
                                hitEffect.Play();
                            }
                            currentManaState -= LSMIManaCost * Time.deltaTime;
                            RaycastHit2D hit = Physics2D.Raycast(groundCheck.transform.position, Vector2.down);
                            Beam.enabled = true;

                            Beam.SetPosition(1, new Vector3( 0,-gameObject.transform.position.y,0));

                            hitEffectGO.transform.position = new Vector3(gameObject.transform.position.x, 0, 0);

                            if(marekRb.velocity.y < 0)
                            {
                                marekRb.velocity = new Vector2(marekRb.velocity.x, -floatingForce);
                            }

                            if (hit.transform.tag == "Frost")
                            {
                                TrapDestroyed();
                                Destroy(hit.transform.gameObject);
                                marekRb.velocity = new Vector2(marekRb.velocity.x, hitKickForce);
                                Instantiate(frostDestroyEffect, hit.point, Quaternion.identity);
                            }
                        }
                    }
                }
                if (Input.GetButtonUp("Jump"))
                {
                    if (isDoingLSMI)
                    {
                        Beam.enabled = false;
                        //ResetCharge();
                        LSMI.Stop();
                        hitEffect.Stop();

                        isDoingSomething = false;
                        isDoingLSMI = false;
                    }
                }
            }
            else
            {
                Beam.enabled = false;
                hitEffect.Stop();

                ResetCharge();
                LSMI.Stop();
                isDoingLSMI = false;
                isDoingSomething = false;
            }
        }
    }

    void Charge()
    {
        if(currentLSMIChargeTime > 0)
        {
            currentLSMIChargeTime -= Time.deltaTime;
        }
    }

    void ResetCharge()
    {
        currentLSMIChargeTime = LSMICharge;
    }


    void ChargeAttack()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            if(currentManaState > chargeCost)
            {
                currentManaState -= chargeCost;
                Instantiate(chargeAttackEffect, gameObject.transform.position, Quaternion.identity);

                RaycastHit2D hit2 = Physics2D.Raycast(gunPosition.transform.position, Vector2.right, chargeDistance, whatIsFog);

                if (hit2.transform.gameObject != null)
                {
                    if (hit2.transform.tag == "Fog")
                    {
                        TrapDestroyed();
                        Destroy(hit2.transform.gameObject);
                        Instantiate(fogDestroyEffect, hit2.point, Quaternion.identity);
                    }
                }
            }
        }
    }

    void TrapDestroyed()
    {
        scoreControllerScript.trapsDestroyed++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Fog")
        {
            healthScript.TakeLive();
            Lives--;
            marekRb.velocity = new Vector2(marekRb.velocity.x, hitKickForce);
            Destroy(collision.gameObject);
            scoreControllerScript.trapsDestroyed++;
            Instantiate(booEffect, gameObject.transform.position, Quaternion.identity);
        }
        if (collision.gameObject.tag == "Frost")
        {
            healthScript.TakeLive();
            Lives--;
            marekRb.velocity = new Vector2(marekRb.velocity.x, hitKickForce);
            Destroy(collision.gameObject);
            scoreControllerScript.trapsDestroyed++;
            Instantiate(booEffect, gameObject.transform.position, Quaternion.identity);
        }
        if (collision.gameObject.tag == "Cloud")
        {
            healthScript.TakeLive();
            Lives--;
            marekRb.velocity = new Vector2(marekRb.velocity.x, hitKickForce);
            Destroy(collision.gameObject);
            scoreControllerScript.trapsDestroyed++;
            Instantiate(booEffect, gameObject.transform.position, Quaternion.identity);
        }
    }

    void Die()
    {
        if(Lives == 0)
        {
            Instantiate(deathEffect, gameObject.transform.position, Quaternion.identity);
            paralaxScript.Speed = 0;
            isAlive = false;
            marekRb.velocity = Vector2.zero;
            scoreControllerScript.ShowEndPanel();
        }
    }
}
