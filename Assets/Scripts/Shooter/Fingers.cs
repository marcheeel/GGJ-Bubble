using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fingers : MonoBehaviour
{
    [Header("Animations")]
    [Space(10)]
    [SerializeField] private Animator anim;
    [Space(15)]
    
    [Header("Tutorial Mode")]
    [Space(10)]
    [SerializeField] private bool tutorial;    
    public bool fingersUnlocked = false;
    [Space(15)]
    
    [SerializeField] GameObject spell;

    [SerializeField] private float fireRate;
    [SerializeField] private bool canFire;
    [Space(5)]
    [SerializeField] private float spellSpeed;

    [Space(10)]
    public int pointSelector;
    [Space(5)]
    [SerializeField] private Transform firePoint;
    [Space(5)]
    [SerializeField] private Transform leftPoint;
    [SerializeField] private Transform rightPoint;

    private void Start()
    {
        canFire = true;
        pointSelector = 1;
    }

    private void Update()
    {      
        if (pointSelector == 0)
        {
            firePoint = leftPoint;
        }
        else if (pointSelector == 1)
        {
            firePoint = rightPoint;
        }

        if (Input.GetKeyDown(KeyCode.Space) && canFire)
        {
            switch (tutorial)
            {
                case false when pointSelector == 0:
                    anim.SetTrigger("attackleft");
                    break;
                case false when pointSelector == 1:
                    anim.SetTrigger("attackright");
                    break;
            }

            StartCoroutine(FingersCooldown(fireRate));
        }
    }

    private IEnumerator FingersCooldown(float time)
    {         
        
        GameObject InstanciatedSpell = Instantiate(spell, firePoint.position, firePoint.rotation);
        Rigidbody2D rbSpell = InstanciatedSpell.GetComponent<Rigidbody2D>();
        Vector3 direction = (firePoint.position - (Vector3)transform.position).normalized;
        rbSpell.AddForce(direction * spellSpeed, ForceMode2D.Impulse);
        canFire = false;
        yield return new WaitForSeconds(time);
        canFire = true;
        yield return null;
    }
}