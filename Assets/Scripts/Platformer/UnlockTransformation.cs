using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UnlockTransformation : MonoBehaviour
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
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Controller.current.transformationUnlocked = true;
            if (!tutorial)
            {
                coinAudioSource.Play();
            }
            Destroy(gameObject);
        }
    }
}
