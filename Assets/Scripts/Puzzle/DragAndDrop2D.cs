using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop2D : MonoBehaviour
{
    [SerializeField] Transform grabbedObject;
    [SerializeField] bool dragging = false;

    private void Update()
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (Input.GetMouseButton(0))
        {
            dragging = true;
        }
    }
}
