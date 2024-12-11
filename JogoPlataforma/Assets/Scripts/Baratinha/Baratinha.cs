using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Baratinha : MonoBehaviour
{
    public bool justHurt;
    private Rigidbody2D rig;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //getHurt();
    }

/*     public void getHurt()
    {
        
        anim.SetBool("hurt", true);
        anim.SetBool("hurt", false);
    } */

    public IEnumerator Wait(float time, Action function)
    {
        yield return new WaitForSeconds(time); // Espera o tempo definido   

        function();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (justHurt)
            {
                Destroy(gameObject);
            }
            else
            {
                justHurt = true;
                anim.SetBool("hurt", true);
            }
        }
    }

    public void StopHurt()
    {
        anim.SetBool("hurt", false);
        justHurt = false;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(
                Wait(1f, StopHurt)
 
                );

        }
    }
}
