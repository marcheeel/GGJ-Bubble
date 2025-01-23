using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] float spellDuration;

    private void Start()
    {
        Destroy(gameObject, spellDuration);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Box"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.GetComponent<Enemy>() != null)
        {
            collision.gameObject.GetComponent<Enemy>().hp--;
            collision.gameObject.GetComponent<Enemy>().CheckHP();
            Destroy(gameObject);
        }
    }
}
