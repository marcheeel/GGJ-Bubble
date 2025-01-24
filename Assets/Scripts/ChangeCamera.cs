using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    [SerializeField] GameObject camera1;
    [SerializeField] GameObject camera2;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (camera1.activeSelf)
            {
                camera1.SetActive(false);
                camera2.SetActive(true);
            }
            else
            {
                camera1.SetActive(true);
                camera2.SetActive(false);
            }
        }
    }
}