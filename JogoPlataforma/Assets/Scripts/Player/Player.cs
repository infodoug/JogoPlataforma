using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public bool balloon = false;
    [SerializeField] private GameObject balloonItem;
    public float count;

    [SerializeField] private string menuInicial;

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

        if (balloonItem.activeSelf)
        {
            balloon = true;
        }
        else
        {
            balloon = false;
        }
        if (balloon)
        {
            rig.gravityScale = 2f;
        // Detecta se a tecla Espaço está sendo pressionada
        if (Input.GetKey(KeyCode.Space))
        {
            
            // Aplica força para cima enquanto o botão está pressionado
            if (transform.position.y < 5f) // Limita a altura máxima
            {
                rig.AddForce(new Vector3(0, 9f, 0), ForceMode2D.Force);
                //rig.AddForce(Vector3.up * 10f, ForceMode2D.Force);
            }
            if (rig.velocity.y < 10f) // Limita a velocidade de subida
            {
                rig.AddForce(Vector3.up * 9f, ForceMode2D.Force);
            }
        }
            //isJumping = false;
            count += Time.deltaTime; // Subtrai o tempo que passou desde o último frame
            if (count >= 10f) // Quando o tempo acabar, desativa o item
            {
                balloonItem.SetActive(false);
                count = 0f; // Garante que o valor não fique negativo
                
            }
        }
        else
        {
            rig.gravityScale = 4f;
        }

    }

    public void Resetar()
    {
        SceneManager.LoadScene(menuInicial);
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

