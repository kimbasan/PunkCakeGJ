using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    //��������
    public GameObject[] HealthImage;
    public int NumberOfLives, IndexHealth;
    //�����
    public Image Hp;
    public float NumberOfLivesNum, IndexHealthNum;
    public TextMeshProUGUI HpText;
    void Start()
    {
        //��������
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
        //�����
        NumberOfLivesNum = PlayerPrefs.GetInt("HealthNum");
        IndexHealthNum = PlayerPrefs.GetInt("MaxHealthNum");
        Hp.fillAmount = NumberOfLivesNum / IndexHealthNum;
        HpText.text = $"{ NumberOfLivesNum} / {IndexHealthNum.ToString()}";
    }
    //��������
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
    //�����
    public void TakeDamageNum(int Damage)
    {
        if (NumberOfLivesNum > 0)
        {
            NumberOfLivesNum -= Damage;
            Hp.fillAmount = NumberOfLivesNum / IndexHealthNum;
        }
        HpText.text = $"{ NumberOfLivesNum} / {IndexHealthNum.ToString()}";
    }
    //��������
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
    //�����
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
