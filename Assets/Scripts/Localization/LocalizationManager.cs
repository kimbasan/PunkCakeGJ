using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using TMPro;

public class LocalizationManager : MonoBehaviour
{
    public static int SelectedLanguage { get; private set; }
    public static event LanguageChangeHandler OnLanguageChange;
    public delegate void LanguageChangeHandler();

    private static Dictionary<string, List<string>> localization;
    [SerializeField]private TMP_Dropdown dropdown;
    [SerializeField] private TextAsset textFile;

    void Awake()
    {
        if (localization == null)
        {
            LoadLocalization();
        }
        //if (PlayerPrefs.HasKey("Lang"))
        //{
        //    dropdown.value = PlayerPrefs.GetInt("Lang");
        //    SetLanguageDrop();
        //}
        //else
        //{
        //    dropdown.value = 0;
        //    SetLanguageDrop();
        //}
    }

    public void SetLanguageDrop()
    {
        if(dropdown.value == 0)
        {
            SelectedLanguage = 0;
            OnLanguageChange?.Invoke();
        }
        else if(dropdown.value == 1)
        {
            SelectedLanguage = 1;
            OnLanguageChange?.Invoke();
        }
        PlayerPrefs.SetInt("Lang", dropdown.value);
    }

    public void SetLanguage(int id)
    {
        SelectedLanguage = id;
        OnLanguageChange?.Invoke();
    }

    private void LoadLocalization()
    {
        localization = new Dictionary<string, List<string>>();
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(textFile.text);
        foreach(XmlNode key in xmlDocument["Keys"].ChildNodes)
        {
            string keyStr = key.Attributes["Name"].Value;
            var values = new List<string>();
            foreach(XmlNode translate in key["Translates"].ChildNodes)
                values.Add(translate.InnerText);
            localization[keyStr] = values;
        }
    }

    public static string GetTranslate(string key, int languageId = -1)
    {
        if(languageId == -1)
            languageId = SelectedLanguage;
        if (localization.ContainsKey(key))
            return localization[key][languageId];
        return key;
    }
}
