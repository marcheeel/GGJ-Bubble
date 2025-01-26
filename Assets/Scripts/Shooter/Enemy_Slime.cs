using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy_Slime : MonoBehaviour
{
    private AudioSource slimeAudioSource;
    
    [Header("Animations")]
    [Space(10)]
    [SerializeField] private Animator anim;
    [Space(15)]
    
    [Header("Tutorial Mode")]
    [Space(10)]
    [SerializeField] private bool tutorial;
    [Space(15)]

    [Header("Movement and HP")]
    [Space(10)]
    public int hp = 2;
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private Transform playerPos;
    [SerializeField] private bool canChasePlayer;
    [Space(5)]
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float timeBetweenJumps = 1.5f;
    [SerializeField] private float jumpCooldown = 0.1f;
    [Space(15)]

    [Header("Pathfinding")]
    [Space(10)]
    [SerializeField] private Transform currentTarget;
    [Space(5)]
    [SerializeField] private Transform waypointA;
    [SerializeField] private Transform waypointB;


    public void Start()
    {
        anim = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        if (!tutorial)
        {
            slimeAudioSource = GetComponent<AudioSource>();
        }

        currentTarget = waypointA;
    }
    
    public void Update()
    {
        if (jumpCooldown > 0)
        {
            jumpCooldown -= Time.deltaTime;
        }

        if (canChasePlayer && playerPos != null && jumpCooldown <= 0)
        {
            JumpTowards(playerPos.position);
        }
        else if (!canChasePlayer && jumpCooldown <= 0)
        {
            JumpTowards(currentTarget.position);

            if (Vector2.Distance(transform.position, currentTarget.position) < 1.5f)
            {
                currentTarget = currentTarget == waypointA ? waypointB : waypointA;
            }
        }
    }

    private void JumpTowards(Vector2 targetPosition)
    {
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        Vector2 jumpVector = new Vector2(direction.x * moveSpeed, rigidbody2D.velocity.y + jumpForce);

        if (!tutorial)
        {
            anim.SetTrigger("jump");
        }

        rigidbody2D.velocity = jumpVector;

        if (slimeAudioSource != null)
        {
            slimeAudioSource.Play();
        }

        jumpCooldown = timeBetweenJumps;

        if ((direction.x > 0 && transform.localScale.x < 0) || (direction.x < 0 && transform.localScale.x > 0))
        {
            Flip();
        }
    }
    
    private void Flip()
    {
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
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
            playerPos = collision.transform;
            canChasePlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerPos = null;
            canChasePlayer = false;
        }
    }
}