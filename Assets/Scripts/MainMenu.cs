using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadSceneAsync("Tomb1_Cave");
    }
    public void Options()
    {
        SceneManager.LoadSceneAsync("Options");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
