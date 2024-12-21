using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyItem : MonoBehaviour
{
    [SerializeField] private Player player;
    public int price;
    public Text priceOnMenu;
    // Start is called before the first frame update
    public void Buy() {
        if (player.currentTrash >= price)
        {
            player.currentTrash -= price;
            priceOnMenu.text = player.currentTrash.ToString();
        }
    }
}
