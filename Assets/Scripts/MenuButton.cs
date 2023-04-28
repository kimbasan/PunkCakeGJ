using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public void StartGame(int Level)
    {
        GetComponent<StartSetting>().NewGame();
        SceneManager.LoadScene(Level);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
