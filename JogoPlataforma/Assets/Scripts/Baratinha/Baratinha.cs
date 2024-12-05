using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            justHurt = true;
            anim.SetBool("hurt", true);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            justHurt = false;
            anim.SetBool("hurt", false);
        }
    }
}
