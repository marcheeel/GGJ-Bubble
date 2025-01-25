using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public static Controller current;
    
    [SerializeField] Fingers fingers;
    [SerializeField] DragAndDrop2D dragAndDrop;

    [Header("Movement")] 
    [Space(15)] 
    public bool canMove;
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float jumpForce = 5f;
    [Space (10)]
    Rigidbody2D rb2D;
    [SerializeField] bool isGrounded;

    [Header("Transformation Spell")] 
    [Space(15)]
    SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite originalFormSprite;
    [SerializeField] private BoxCollider2D originalFormCollider;
    [SerializeField] private bool transformed;
    [SerializeField] private Sprite transformationSprite;
    [SerializeField] private BoxCollider2D transformationCollider;

    [Header("Animations")]
    [Space(10)]
    [SerializeField] private Animator anim;
    [Space(15)]
    
    [Header("Tutorial Mode")]
    [Space(10)]
    [SerializeField] private bool tutorial;
    public bool transformationUnlocked = false;
    [Space(15)]
    
    [Header("Sprites And Animations")]
    [Space(15)]
    [SerializeField] public int hp = 3;
    [Space(10)]
    [SerializeField] private Sprite fullHpSprite;
    [SerializeField] private RuntimeAnimatorController fullHpAnimations;
    [Space(5)]
    [SerializeField] private Sprite lightDamagedSprite;
    [SerializeField] private RuntimeAnimatorController lightDamagedAnimations;
    [Space(5)]
    [SerializeField] private Sprite seriouslyDamagedSprite;
    [SerializeField] private RuntimeAnimatorController seriouslyDamagedAnimations;

    public int money;

    private void Awake()
    {
        if (current != null)
        {
            Destroy(gameObject);
        }
        else
        {
            current = this;
        }
        
        canMove = true;
    }

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalFormSprite = spriteRenderer.sprite;
        transformed = false;

        fingers = GetComponent<Fingers>();

        dragAndDrop = FindAnyObjectByType<DragAndDrop2D>();
        
        transformationUnlocked = false;
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;

        if (canMove)
        {
            transform.Translate(h, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.W) && isGrounded && canMove)
        {
            rb2D.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            if (!tutorial)
            {
                anim.SetTrigger("jump");
            }
        }    

        if (h < 0)
        {
            spriteRenderer.flipX = true;
            if (!tutorial)
            {
                anim.SetFloat("speed", 1);
            }
            fingers.pointSelector = 0;
        }
        else if (h > 0)
        {
            spriteRenderer.flipX = false;
            if (!tutorial)
            {
                anim.SetFloat("speed", 1);
            }
            fingers.pointSelector = 1;
        }
        else if (h == 0)
        {
            if (!tutorial)
            {
                anim.SetFloat("speed", 0);
            }
        }
        
        if (Input.GetKeyDown(KeyCode.S) && isGrounded && canMove && transformationUnlocked)
        {
            if (!tutorial)
            {
                anim.SetBool("transform", true);
            }
            else
            {
                anim.SetBool("transform", false);
            }
            
            if (transformed == false)
            {
                spriteRenderer.sprite = transformationSprite;
                transformationCollider.enabled = true;
                originalFormCollider.enabled = false;
                transformed = true;
                isGrounded = true;
            }
            else
            {
                spriteRenderer.sprite = originalFormSprite;
                originalFormCollider.enabled = true;
                transformationCollider.enabled = false;
                transformed = false;
                isGrounded = true;
            }
        }
    }

    void CheckHP()
    {
        if (hp <= 0) 
        { 
            hp = 0;
            
            if (!tutorial)
            {
                anim.SetTrigger("death");
                // iniciar fade in fade out
            }
            
            StartCoroutine(SaveSystem.current.BackToCheckpoint());
        }
        else if (hp == 3)
        {
            spriteRenderer.sprite = fullHpSprite;
            if (!tutorial)
            {
                anim.runtimeAnimatorController = fullHpAnimations;
            }
        }
        else if (hp == 2)
        {
            spriteRenderer.sprite = lightDamagedSprite;
            if (!tutorial)
            {
                anim.runtimeAnimatorController = lightDamagedAnimations;
            }
        }
        else if (hp == 1)
        {
            spriteRenderer.sprite = seriouslyDamagedSprite;
            if (!tutorial)
            {
                anim.runtimeAnimatorController = seriouslyDamagedAnimations;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground") || collision.collider.CompareTag("Box"))
        {
            isGrounded = true;
        }

        if (collision.collider.CompareTag("Enemy"))
        {
            hp--;
            CheckHP();
        }      
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}