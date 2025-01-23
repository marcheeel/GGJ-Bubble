using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 5f;

    [SerializeField] float jumpForce = 5f;
    Rigidbody2D rb2D;
    [SerializeField] bool isGrounded;

    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite originalFormSprite;
    [SerializeField] BoxCollider2D originalFormCollider;
    [SerializeField] bool transformed;
    [SerializeField] Sprite transformationSprite;
    [SerializeField] BoxCollider2D transformationCollider;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalFormSprite = spriteRenderer.sprite;
        transformed = false;
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;

        transform.Translate(h, 0, 0);

        if (Input.GetKeyDown(KeyCode.W) && isGrounded == true)
        {
            rb2D.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
        }    

        if (h < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (h > 0)
        {
            spriteRenderer.flipX = false;
        }

        if (Input.GetKeyDown(KeyCode.S) && isGrounded)
        {
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
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