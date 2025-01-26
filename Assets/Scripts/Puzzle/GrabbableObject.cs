using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : MonoBehaviour
{
    [SerializeField] AudioSource stoneAudioSource;
    
    [Header("Animations")]
    [Space(10)]
    [SerializeField] private Animator anim;
    [Space(15)]
    
    [Header("Tutorial Mode")]
    [Space(10)]
    [SerializeField] private bool tutorial;
    [Space(15)]
    
    public bool canBeGrabbed;

    private void Start()
    {
        if (!tutorial)
        {
            stoneAudioSource = GetComponent<AudioSource>();
            stoneAudioSource.loop = true;
        }
    }

    private void Update()
    {
        if (DragAndDrop2D.current.dragging == true && !tutorial)
        {
            stoneAudioSource.Play();
        }
        else
        {
            stoneAudioSource.Stop();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canBeGrabbed = true;
            
            if (!tutorial)
            {
                anim.SetTrigger("color");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canBeGrabbed = false;
            
            if (!tutorial)
            {
                anim.SetTrigger("noncolor");
            }
        }           
    }
}