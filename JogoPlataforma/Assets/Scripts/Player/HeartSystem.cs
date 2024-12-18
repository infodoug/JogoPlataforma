using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HeartSystem : MonoBehaviour
{
    public int life;
    public int maxLife;

    private Player player;
    public Image[] heart;
    public Sprite full;
    public Sprite empty;

    [SerializeField] private GameObject textGameOver;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<Player>();
        life = player.lifes;
        maxLife = 2;
    }

    // Update is called once per frame
    void Update()
    {
        life = player.lifes;
        maxLife = 2;
        HealthLogic();
    }

    public void ResetGame()
    {

    }

    private IEnumerator ResetAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay-0.2f);
        player.anim.SetBool("hurt", false);
        yield return new WaitForSeconds(delay);
        textGameOver.SetActive(false);
        player.transform.position = player.initialPosition;
        player.isJumping = true;
        player.anim.SetBool("jump", true);
        player.lifes = maxLife;
        
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void HealthLogic()
    {
        if (life == 0)
        {
            textGameOver.SetActive(true);
            StartCoroutine(ResetAfterDelay(0.8f));
            


            
        }
        if (life > maxLife)
        {
            maxLife = life;
        }

        for (int i = 0; i < heart.Length; i++)
        {
            if (i < life)
            {
                heart[i].sprite = full;
            }
            else
            {
                heart[i].sprite = empty;
            }
        }
    }
}
