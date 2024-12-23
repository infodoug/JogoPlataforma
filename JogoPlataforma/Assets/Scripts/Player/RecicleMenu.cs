using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecicleMenu : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject continueButton;
    
    void Start()
    {
        menu.SetActive(false);
    }

    void Update()
    {
        Recicle();
    }

    public void OpenOrCloseMenu()
    {
        if (menu.activeSelf)
        {
            pauseButton.SetActive(true);
            continueButton.SetActive(true);
            menu.SetActive(false);
            Time.timeScale = 1;
        }
        else if (!menu.activeSelf && Time.timeScale == 1)
        {
            pauseButton.SetActive(false);
            continueButton.SetActive(false);
            menu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    void Recicle()
    {
        // Verifica se a tecla 'F' foi pressionada (não importa se a tecla foi solta depois)
        if (Input.GetButtonDown("Fire1"))
        {
            OpenOrCloseMenu();
            //anim.SetBool("recicle", true);
        }
    }

    
}
