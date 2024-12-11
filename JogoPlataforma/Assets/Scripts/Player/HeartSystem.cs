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
        StartCoroutine(ResetAfterDelay(0.5f));
    }

    private IEnumerator ResetAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void HealthLogic()
    {
        if (life == 0)
        {
            ResetGame();
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
