using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject SettingPanel;
    public bool ActiveSettingPanel;
    public GameObject PauseObject;
    bool CheckPause;
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
    public void CheckPanel()
    {
        if(ActiveSettingPanel==false)
        {
            Continue();
        }
        else if((SettingPanel.activeSelf))
        {
            Setting();
        }
    }
    private void Start()
    {
        CheckPause = false;
        PauseObject.SetActive(CheckPause);       
    }
    public void Continue()
    {
        CheckPause = !CheckPause;
        PauseObject.SetActive(CheckPause);
        if (CheckPause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
    public void Setting()
    {
        ActiveSettingPanel = !ActiveSettingPanel;
        SettingPanel.SetActive(ActiveSettingPanel);
    }
    public void Menu(int Level)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(Level);
    }
}
