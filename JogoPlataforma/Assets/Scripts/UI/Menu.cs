using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] private string gameScene;
    // Start is called before the first frame update
    public void Jogar()
    {
        SceneManager.LoadScene(gameScene);
    }


}
