using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioSource coinAudioSource;

    private void Start()
    {
        coinAudioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Controller>().money++;
            coinAudioSource.Play();
            Destroy(gameObject);
        }
    }
}
