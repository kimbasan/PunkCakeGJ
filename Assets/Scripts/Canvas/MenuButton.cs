using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    [SerializeField] private GameObject SettingPanel, AuthorsPanel;
    private bool ActiveSettingPanel, ActiveAuthorsPanel;
    private PlayerInputActions PlayerInputActions;

    private void Awake()
    {
        PlayerInputActions = new PlayerInputActions();
        PlayerInputActions.Player.ClosePanel.performed += context => CheckPanel();
    }
    private void OnEnable()
    {
        PlayerInputActions.Enable();
    }
    private void OnDisable()
    {
        PlayerInputActions.Disable();
    }
    private void Start()
    {
        SettingPanel.SetActive(ActiveSettingPanel);
        AuthorsPanel.SetActive(ActiveAuthorsPanel);
    }
    public void CheckPanel()
    {
        Debug.Log(0);
        if (SettingPanel.activeSelf)
        {
            CheckSetting();
        }
        else if (AuthorsPanel.activeSelf)
        {
            CheckAuthors();
        }
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
