using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioSource coinAudioSource;
    
    public bool tutorial;

    private void Start()
    {
        if (!tutorial)
        {
            coinAudioSource = GetComponent<AudioSource>();  
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Controller>().money++;
            if (!tutorial)
            {
                coinAudioSource.Play();
            }
            Destroy(gameObject);
        }
    }
}
