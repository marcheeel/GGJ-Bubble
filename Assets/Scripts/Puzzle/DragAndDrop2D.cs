using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop2D : MonoBehaviour
{
    public static DragAndDrop2D current;
    
    public bool moveObjectsUnlocked = false;

    [SerializeField] Transform grabbedObject;
    [SerializeField] bool dragging = false;
    [SerializeField] LayerMask layer;

    Vector3 mousePosition;

    private void Awake()
    {
        if (current != null)
        {
            Destroy(gameObject);
        }
        else
        {
            current = this;
        }
    }

    private void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D raycastHit2D = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, layer);

        if (Input.GetKeyDown(KeyCode.Mouse0) && moveObjectsUnlocked)
        {
            if (raycastHit2D != false)
            {
                if (raycastHit2D.collider.gameObject.GetComponent<GrabbableObject>().canBeGrabbed == true)
                {
                    grabbedObject = raycastHit2D.transform;
                    dragging = true;
                }               
            }
        }
        else if (Input.GetKey(KeyCode.Mouse0) && moveObjectsUnlocked)
        {
            if(dragging) 
            {
                grabbedObject.position = new Vector3(mousePosition.x, mousePosition.y, 0);
            }
        }
        else if(Input.GetKeyUp(KeyCode.Mouse0) && moveObjectsUnlocked)
        {
            dragging = false;
            grabbedObject = null;
        }
    }
}
