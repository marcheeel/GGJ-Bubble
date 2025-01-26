using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy_Goblin : MonoBehaviour
{
    [Header("Audio")]
    [Space(10)]
    [SerializeField] private AudioSource goblinAudioSource;
    [Space(5)]
    [SerializeField] private AudioClip chargeBowClip;
    [SerializeField] private AudioClip shootArrowClip;
    [Space(15)]

    [Header("Animations")]
    [Space(10)]
    [SerializeField] private Animator anim;
    [Space(15)]
    
    [Header("Tutorial Mode")]
    [Space(10)]
    [SerializeField] private bool tutorial;
    [Space(15)]
    
    [Header("Stats")]
    [Space(10)]
    public int hp = 2;
    [SerializeField] float arrowSpeed = 10;
    [SerializeField] GameObject arrow;
    [Space(5)]
    [SerializeField] Transform firePoint;
    [SerializeField] Transform leftPoint;
    [SerializeField] Transform rightPoint;
    [Space(5)]
    [SerializeField] Vector3 playerPos;
    [SerializeField] bool playerInRange;

    private void Start()
    {
        anim = GetComponent<Animator>();
        goblinAudioSource = GetComponent<AudioSource>();
    }

    public void CheckHP()
    {
        if (hp <= 0)
        {
            hp = 0;
            if (!tutorial)
            {
                anim.SetTrigger("death");
            }

            Destroy(gameObject);
        }
        else
        {
            if (!tutorial)
            {
                anim.SetTrigger("hurt");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {      
        if (collision.CompareTag("Player"))
        {
            playerPos = collision.transform.position;
            playerInRange = true;
            
            if (!tutorial)
            {
                anim.SetTrigger("attack");
                goblinAudioSource.clip = chargeBowClip;
                goblinAudioSource.Play();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void Attack()
    {
        // Call it on animation event at the end of charging the bow 
        
        Transform actualPlayerPos = FindAnyObjectByType<Fingers>().GetComponent<Transform>();

        float distanceLeft = Vector3.Distance(playerPos, leftPoint.position);
        float distanceRight = Vector3.Distance(playerPos, rightPoint.position);

        if (distanceLeft < distanceRight)
        {
            firePoint = leftPoint;
        }
        else if (distanceLeft > distanceRight)
        {
            firePoint = rightPoint;
        }

        GameObject InstanciatedArrow = Instantiate(arrow, firePoint.position, firePoint.rotation);
        Rigidbody2D rbArrow = InstanciatedArrow.GetComponent<Rigidbody2D>();
        Vector3 direction = ((Vector3)actualPlayerPos.position - firePoint.position).normalized;
        rbArrow.AddForce(direction * arrowSpeed, ForceMode2D.Impulse);
        goblinAudioSource.clip = shootArrowClip;
        goblinAudioSource.Play();
    }
}