using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseObject;
    bool CheckPause;
    private void Start()
    {
        CheckPause = false;
        PauseObject.SetActive(CheckPause);       
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Continue();
        }
    }
    public void Continue()
    {
        CheckPause = !CheckPause;
        PauseObject.SetActive(CheckPause);
        if (CheckPause)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
    public void Menu(int Level)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(Level);
    }
}
