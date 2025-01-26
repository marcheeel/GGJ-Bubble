using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockMove : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DragAndDrop2D.current.moveObjectsUnlocked = true;
            Destroy(gameObject);
        }
    }
}
