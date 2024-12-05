using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feet : MonoBehaviour
{
    private Player player;
    private BoxCollider rig;
    //private Baratinha baratinha;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<BoxCollider>();
        // Obtém o componente Player no mesmo GameObject
        player = GetComponentInParent<Player>();
        //baratinha = GetComponent<Baratinha>();
        
        // Verifique se player foi atribuído corretamente
        if (player == null)
        {
            Debug.LogError("Componente Player não encontrado no GameObject.");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Baratinha"))
        {
            //Destroy(collision.gameObject);
            //Debug.LogWarning(collision);
            // Verifica se player foi encontrado
            if (player != null)
            {
                
                //baratinha.anim.SetBool("hurt", true);
                // Usa a força de pulo do Player
                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, player.jumpForce*2), ForceMode2D.Impulse);
            }
        }
    }
}
