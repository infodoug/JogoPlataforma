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

    public bool facingRight = true;

    public Transform alvo;
    public float distanciaPara = 0.2f;
    public bool isChasing = false;

    public string alvoDirection;


    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spawner = GetComponentInParent<Spawner>();

        alvo = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (speed != 0)
        {
            anim.SetBool("walk", true);
        }
        float distanceToPlayer = alvo.position.x - transform.position.x;
        if (!ground)
        {
            isChasing = false;
        }
        if (!isChasing)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            ground = Physics2D.Linecast(groundCheck.position, transform.position, groundLayer);

            if (ground == false)
            {
                //Debug.Log("fora");
                speed *= -1;
            }

        if (speed > 0 && !facingRight)
        {
            Flip();
        }
        else if (speed < 0 && facingRight)
        {
            Flip();
        }
        }
        if (isChasing)
        {
            ground = Physics2D.Linecast(groundCheck.position, transform.position, groundLayer);
            if (ground == false)
            {
                //Debug.Log("fora!!");
                speed *= -1;
            }
            if (Vector2.Distance(transform.position, alvo.position) > distanciaPara)
            {
                if (speed < 0)
                {
                    speed *= -1;
                }
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(alvo.position.x, transform.position.y), speed * Time.deltaTime);

            }
            if (distanceToPlayer > 0)
            {
                facingRight = true;
                Vector3 Scale = transform.localScale;
                if (Scale.x < 0)
                {
                    Scale.x *= -1;
                }
                
                transform.localScale = Scale;
            }
            else if (distanceToPlayer < 0)
            {
                facingRight = false;
                Vector3 Scale = transform.localScale;
                if (Scale.x > 0)
                {
                Scale.x *= -1;
                }
                transform.localScale = Scale;
            }
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
        isChasing = true;
        speed = 1;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            speed = 0;
            StartCoroutine(
                Wait(1f, StopHurt)
 
                );
            

        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            Debug.Log("bateu!");
            // Calcula a direção oposta com base na posição relativa
            Vector2 direction = (transform.position - collision.transform.position).normalized;
            int simpledirection;
            if (direction.x > 0)
            {
                simpledirection = 1;
            }
            else
            {
                simpledirection = -1;
            }

            // Define a força do impulso, ajustando a intensidade
            float forceX = 1f;
            float forceY = 2f;
            Vector2 force = new Vector2(simpledirection * forceX, forceY);

            // Aplica a força ao Rigidbody2D do player
            rig.AddForce(force, ForceMode2D.Impulse);
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

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }
}
