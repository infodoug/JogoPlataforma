using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public const float defaultSpeed = 3;
    public const float runningSpeed = 5;
    public float Speed = defaultSpeed;
    private Rigidbody2D rig;
    public float jumpForce;

    public bool isJumping;

    public Animator anim;

    //private bool isAttacking = false;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        Befriend();
    }

    void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        if (Input.GetKey("left shift") || Input.GetKey("right shift")){Speed = runningSpeed;} else {Speed = defaultSpeed;}
        transform.position += movement * Time.deltaTime * Speed;
        if (Input.GetAxis("Horizontal") != 0)
        {
            anim.SetBool("walk", true);
            if (Input.GetAxis("Horizontal") > 0f)
            {
                transform.eulerAngles = new Vector3(0f,0f,0f);
            }
            else
            {
                transform.eulerAngles = new Vector3(0f,180f,0f);
            }
        }
        else
        {
            anim.SetBool("walk", false);
        }
    }
    
    void Jump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if (!isJumping)
            {
                rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                
            }
            
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            anim.SetBool("jump", false);
            isJumping = false;
        }
    }
    
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            anim.SetBool("jump", true);
            isJumping = true;
        }
    }



     void Befriend()
    {
        // Verifica se a tecla 'F' foi pressionada (não importa se a tecla foi solta depois)
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetBool("befriend", true);
        }

        else
        {
            // Quando a animação "Fire1" terminar, desmarque a flag
            anim.SetBool("befriend", false);
        }
    }
/*
    void Shield()
    {
        // Verifica se a tecla 'B' foi pressionada (não importa se a tecla foi solta depois)
        if (Input.GetKey("n"))
        {
            anim.SetBool("shield", true);
        }
        // Verifica se a animação terminou
        //if (isAttacking && !anim.GetCurrentAnimatorStateInfo(0).IsName("Fire1"))
        else
        {
            // Quando a animação "Fire1" terminar, desmarque a flag
            anim.SetBool("shield", false);
        }
    } */
}
