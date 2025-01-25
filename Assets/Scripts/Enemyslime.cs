using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyslime : MonoBehaviour
{
    public bool JugIn;

    public Rigidbody2D rb2D;

    public int Vida = 5;
    public int jumpForce = 3;
    public int Movement = 3;

    public Transform player;
    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
       StartCoroutine(movimiento());
    }

    private void Update()
    {
        if (Vida <= 0)
        {
            gameObject.SetActive(false);
        }
        if (JugIn == true)
        {
            MoverHaciaJugador();
        }
    }
    private IEnumerator movimiento()
    {
        while(true)
        {
            rb2D.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);yield return new WaitForSeconds(1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.GetComponent<Controller>().hp--;
        }
        if (collision.collider.name == "Spell(Clone)")
        {
            Vida--;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            JugIn = true;
        }
        else
        {
            JugIn = false;
        }
    }
    public void MoverHaciaJugador()
    {
        Vector2 targetPosition = new Vector2(player.position.x, rb2D.position.y);
        rb2D.MovePosition(Vector2.MoveTowards(rb2D.position, targetPosition, Movement * Time.deltaTime));
    }
}
