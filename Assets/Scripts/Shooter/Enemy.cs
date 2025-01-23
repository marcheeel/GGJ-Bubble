using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp = 2;
    [SerializeField] float arrowSpeed = 10;
    [SerializeField] Animator anim;
    [SerializeField] GameObject arrow;

    [SerializeField] Transform firePoint;
    [SerializeField] Transform leftPoint;
    [SerializeField] Transform rightPoint;

    [SerializeField] Vector3 playerPos;
    [SerializeField] bool playerInRange;

    public void CheckHP()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {      
        if (collision.CompareTag("Player"))
        {
            playerPos = collision.transform.position;
            playerInRange = true;
            anim.SetTrigger("attack");
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
    }
}