using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartSystem : MonoBehaviour
{
    public int life;
    public int maxLife;

    private Player player;
    public Image[] heart;
    public Sprite full;
    public Sprite empty;

    private Control cameraControl;
    // Start is called before the first frame update
    void Start()
    {
        cameraControl = GetComponentInParent<Control>();
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

    void HealthLogic()
    {
        if (life == 0)
        {
            cameraControl.ResetGame();
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
