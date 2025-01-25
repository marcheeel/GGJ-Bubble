using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop2D : MonoBehaviour
{
    [Header("Animations")]
    [Space(10)]
    [SerializeField] private Animator anim;
    [Space(15)]
    
    [Header("Tutorial Mode")]
    [Space(10)]
    [SerializeField] private bool tutorial;
    [Space(15)]
    
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
                    anim = raycastHit2D.collider.gameObject.GetComponent<Animator>();
                    grabbedObject = raycastHit2D.transform;
                    if (!tutorial)
                    {
                        anim.SetBool("grabbing", true);
                    }
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
            
            if (!tutorial)
            {
                anim.SetBool("grabbing", false);
            }
        }
    }
}
