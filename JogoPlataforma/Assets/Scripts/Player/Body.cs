using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    private Player player;
    private BoxCollider box;
    private bool hasLostLife = false;
    
    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider>();
        // Obtém o componente Player no mesmo GameObject
        player = GetComponentInParent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Baratinha") && !hasLostLife)
        {
            hasLostLife = true;
            player.anim.SetBool("hurt", true);

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
            float forceX = 3f;
            float forceY = 4f;
            Vector2 force = new Vector2(simpledirection * forceX, forceY);

            // Aplica a força ao Rigidbody2D do player
            player.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);

            player.LooseLife(1);
            StartCoroutine(player.BlockMovement(1f));
        }


    }
    
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Trash"))
        {
            player.currentTrash++;
        }
        if (collision.CompareTag("Baratinha"))
        {
            hasLostLife = false;
            player.anim.SetBool("hurt", false);
        }
    }

}
