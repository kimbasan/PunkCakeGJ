using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSetting : MonoBehaviour
{
    public void NewGame()
    {
        PlayerPrefs.SetInt("Health", 4);
        PlayerPrefs.SetInt("MaxHealth", 4);
        PlayerPrefs.SetInt("HealthNum", 100);
        PlayerPrefs.SetInt("MaxHealthNum", 100);
    }
}
