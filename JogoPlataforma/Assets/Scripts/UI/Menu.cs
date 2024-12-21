using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private string gameScene;
    [SerializeField] private GameObject targetObject;
    [SerializeField] private GameObject pauseMenu;
    // Start is called before the first frame update
    public void Jogar()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void PauseOrContinue() {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            if (targetObject != null)
            {
                pauseMenu.SetActive(false);
                targetObject.SetActive(true); // Desativa o GameObject
                Debug.Log("GameObject ativado!");
            }
        }
        else
        {
            Time.timeScale = 0;
            if (targetObject != null)
            {
                pauseMenu.SetActive(true);
                targetObject.SetActive(false); // Desativa o GameObject
                Debug.Log("GameObject desativado!");
            }
        }
    }

}
