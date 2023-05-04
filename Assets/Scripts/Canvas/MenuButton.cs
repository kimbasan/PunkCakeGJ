using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    [SerializeField] private GameObject SettingPanel, AuthorsPanel;
    private bool ActiveSettingPanel, ActiveAuthorsPanel;
    private void Start()
    {
        SettingPanel.SetActive(ActiveSettingPanel);
        AuthorsPanel.SetActive(ActiveAuthorsPanel);
    }
    public void StartGame(int Level)
    {
        GetComponent<StartSetting>().NewGame();
        SceneManager.LoadScene(Level);
    }
    public void Setting()
    {
        CheckSetting();
        CheckActivePanel(1);
    }
    public void Authors()
    {
        CheckAuthors();
        CheckActivePanel(0);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    void CheckActivePanel(int IndexVoid)
    {
        if (SettingPanel.activeSelf && IndexVoid==0)
        {
            CheckSetting();
        }
        if(AuthorsPanel.activeSelf && IndexVoid == 1)
        {
            CheckAuthors();
        }
    }
    void CheckSetting()
    {
        ActiveSettingPanel = !ActiveSettingPanel;
        SettingPanel.SetActive(ActiveSettingPanel);
    }
    void CheckAuthors()
    {
        ActiveAuthorsPanel = !ActiveAuthorsPanel;
        AuthorsPanel.SetActive(ActiveAuthorsPanel);
    }
}
