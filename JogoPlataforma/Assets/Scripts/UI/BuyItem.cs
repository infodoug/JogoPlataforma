using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BuyItem : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject textNoTrash;
    [SerializeField] private GameObject itemComprado;
    public int price;
    public Text priceOnMenu;
    

    void Update()
    {
        // Reduz o count no tempo

    }

    public void Buy()
    {
        if (player.currentTrash >= price)
        {
            player.currentTrash -= price;
            itemComprado.SetActive(true);
        }
        else
        {
            //textNoTrash.SetActive(true);
            //StartCoroutine(FalseNoTrash(1f));
        }
    }

    public IEnumerator FalseNoTrash(float time)
    {
        yield return new WaitForSeconds(time); // Espera o tempo definido   
        textNoTrash.SetActive(false); // Desativa a mensagem
    }
}
