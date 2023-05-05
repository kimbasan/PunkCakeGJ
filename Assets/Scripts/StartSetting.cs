using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSetting : MonoBehaviour
{
    private void Awake()
    {
        Screen.SetResolution(1920, 1080, true);
    }
    public void NewGame()
    {
        PlayerPrefs.SetInt("Health", 4);
        PlayerPrefs.SetInt("MaxHealth", 4);
        PlayerPrefs.SetInt("HealthNum", 100);
        PlayerPrefs.SetInt("MaxHealthNum", 100);
    }
}
