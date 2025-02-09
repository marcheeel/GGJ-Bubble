using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public bool tutorial;
    [SerializeField] float spellDuration;
    
    [SerializeField] AudioSource spellAudioSource;

    private void Start()
    {
        Destroy(gameObject, spellDuration);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Box"))
        {
            if (!tutorial)
            {
                spellAudioSource.Play();
            }
            Destroy(gameObject);
        }

        if (collision.gameObject.GetComponent<Enemy_Goblin>() != null)
        {
            if (!tutorial)
            {
                spellAudioSource.Play();
            }
            collision.gameObject.GetComponent<Enemy_Goblin>().hp--;
            collision.gameObject.GetComponent<Enemy_Goblin>().CheckHP();
        }
        else if (collision.gameObject.GetComponent<Enemy_Slime>() != null)
        {
            if (!tutorial)
            {
                spellAudioSource.Play();
            }
            collision.gameObject.GetComponent<Enemy_Slime>().hp--;
            collision.gameObject.GetComponent<Enemy_Slime>().CheckHP();
        }
        else if (collision.gameObject.GetComponent<Enemy_Goblin>() == null &&
                 collision.gameObject.GetComponent<Enemy_Slime>() == null &&
                 collision.gameObject.CompareTag("Enemy"))
        {
            if (!tutorial)
            {
                spellAudioSource.Play();
            }
            Destroy(gameObject); 
        }
    }
}
