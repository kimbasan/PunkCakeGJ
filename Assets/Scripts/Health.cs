using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    //Сердечки
    public GameObject[] HealthImage;
    public int NumberOfLives, IndexHealth;
    //Цыфры
    public Image Hp;
    public float NumberOfLivesNum, IndexHealthNum;
    public TextMeshProUGUI HpText;
    void Start()
    {
        //Сердечки
        for (int i = HealthImage.Length - 1; i >= 0; i--)
        {
            HealthImage[i].SetActive(false);
        }
        NumberOfLives = PlayerPrefs.GetInt("Health");
        for (int i = 0; i <= NumberOfLives; i++)
        {
            HealthImage[i].SetActive(true);
        }
        IndexHealth = PlayerPrefs.GetInt("MaxHealth");
        //Цыфры
        NumberOfLivesNum = PlayerPrefs.GetInt("HealthNum");
        IndexHealthNum = PlayerPrefs.GetInt("MaxHealthNum");
        Hp.fillAmount = NumberOfLivesNum / IndexHealthNum;
        HpText.text = $"{ NumberOfLivesNum} / {IndexHealthNum.ToString()}";
    }
    //Сердечки
    public void TakeDamage(int Damage)
    {
        if(NumberOfLives > 0)
        {
            for (int i = NumberOfLives; i > NumberOfLives - Damage; i--)
            {
                HealthImage[i].SetActive(false);
            }
            NumberOfLives -= Damage;
        }
    }
    //Цыфры
    public void TakeDamageNum(int Damage)
    {
        if (NumberOfLivesNum > 0)
        {
            NumberOfLivesNum -= Damage;
            Hp.fillAmount = NumberOfLivesNum / IndexHealthNum;
        }
        HpText.text = $"{ NumberOfLivesNum} / {IndexHealthNum.ToString()}";
    }
    //Сердечки
    public void Healing(int TakeHealing)
    {
        if (NumberOfLives < IndexHealth - 1)
        {
            for (int i = NumberOfLives; i <= NumberOfLives + TakeHealing; i++)
            {
                HealthImage[i].SetActive(true);
            }
            NumberOfLives += TakeHealing;
        }
    }
    //Цыфры
    public void HealingNum(int TakeHealing)
    {
        if (NumberOfLivesNum < IndexHealthNum)
        {
            NumberOfLivesNum += TakeHealing;
            Hp.fillAmount = NumberOfLivesNum / IndexHealthNum;
        }
        HpText.text = $"{ NumberOfLivesNum} / {IndexHealthNum.ToString()}";
    }
}
