using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Baratinha : MonoBehaviour
{
    public bool justHurt;
    private Rigidbody2D rig;
    public Animator anim;
    private Spawner spawner;

    public float speed;
    public bool ground = true;
    public Transform groundCheck;
    public LayerMask groundLayer;


    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spawner = GetComponentInParent<Spawner>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        ground = Physics2D.Linecast(groundCheck.position, transform.position, groundLayer);

        if (ground == false)
        {
            speed *= -1;
        }
    }

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
                Destroy(spawner.instancia);
                spawner.quantidade -= 1;
                spawner.Spawn();
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

    // Função chamada quando o GameObject é reativado
    void OnEnable()
    {
        // Aqui verificamos se o inimigo já estava ferido antes de ser desativado.
        if (justHurt)
        {
            // Se ele já estava ferido, ainda deve ter a animação "hurt" ativa ao ser reativado.
            anim.SetBool("hurt", false);
        }
    }
}
