using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public const float defaultSpeed = 3;
    public const float runningSpeed = 5;
    public float Speed = defaultSpeed;
    private Rigidbody2D rig;
    public float jumpForce;

    public bool isJumping;

    public Animator anim;

    public int lifes = 2;

    public bool canMove = true;

    public Vector3 initialPosition;

    public int currentTrash = 0;
    public Text textTrash;

    //private bool isAttacking = false;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        initialPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            if (canMove)
            {
            Move();
            }
            Jump();
            //Recicle();

            textTrash.text = currentTrash.ToString();
        }

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
                anim.SetBool("jump", true);
                isJumping = true;
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





    public void LooseLife(int num)
    {
        if (lifes == 0) {
            return;
        }
        else {
            lifes = lifes - num;
            Debug.Log(lifes);
            anim.SetBool("walk", false);
            anim.SetBool("jump", false);
            anim.SetBool("hurt", true);
        }

    }

    public IEnumerator BlockMovement(float time)
    {
        canMove = false; // Desativa o movimento
        anim.SetBool("walk", false);
        anim.SetBool("jump", false);
        anim.SetBool("hurt", true);
        yield return new WaitForSeconds(time); // Espera o tempo definido
        canMove = true; // Ativa o movimento novamente    
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

