using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Vector3 checkpointPosition;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            checkpointPosition = gameObject.transform.position;
            PlayerPrefs.SetFloat("Checkpoint.X", checkpointPosition.x);
            PlayerPrefs.SetFloat("Checkpoint.Y", checkpointPosition.y);
            PlayerPrefs.SetFloat("Checkpoint.Z", checkpointPosition.z);
            
            PlayerPrefs.SetInt("Money", Controller.current.money);
        }
    }
}
