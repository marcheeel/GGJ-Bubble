using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Dock : MonoBehaviour
{
    [SerializeField] BoxCollider2D doorCollider;
    [SerializeField] Animator doorAnimator;
    
    [Space(10)]
    
    [SerializeField] private bool unlockedWithCoins;
    public int coinsNeeded;
    public bool doorOpen;
    [SerializeField] private bool somethingOnDock;
    [SerializeField] private Animator dockAnimator;
    [SerializeField] private GameObject objectOnDock;

    private void Update()
    {
        dockAnimator.SetBool("onDock", somethingOnDock);
        
        switch (unlockedWithCoins)
        {
            // Con monedas
            case true:
                switch (somethingOnDock)
                {
                    case true when Controller.current.money >= coinsNeeded && unlockedWithCoins:
                        doorAnimator.SetTrigger("open");
                        Controller.current.money -= coinsNeeded;
                        doorCollider.enabled = false;
                        doorOpen = true;
                        break;
               
                    case true when Controller.current.money < coinsNeeded:
                        doorOpen = false;
                        break;
               
                    case false:
                        doorAnimator.SetTrigger("close");
                        doorCollider.enabled = true;
                        doorOpen = false;
                        break;
                }
                break;
            
            case false:
                switch (somethingOnDock)
                {
                    case true:
                        doorAnimator.SetTrigger("open");
                        Controller.current.money -= coinsNeeded;
                        doorCollider.enabled = false;
                        doorOpen = true;
                        break;
                    case false:
                        doorAnimator.SetTrigger("close");
                        doorCollider.enabled = true;
                        doorOpen = false;
                        break;
                }
                break;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("Box"))
        {
            objectOnDock = collision.gameObject;
            somethingOnDock = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") || collision.collider.CompareTag("Box"))
        {
            objectOnDock = null;
            somethingOnDock = false ;
        }
    }
}
