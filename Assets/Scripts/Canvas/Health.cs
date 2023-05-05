using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    public Image Hp;
    public float NumberOfLivesNum, IndexHealthNum;
    public TextMeshProUGUI HpText;
    private PlayerInputActions PlayerInputActions;

    private void Awake()
    {
        PlayerInputActions = new PlayerInputActions();
        PlayerInputActions.Player.Move.performed += context => CheckBattery();
    }
    private void OnEnable()
    {
        PlayerInputActions.Enable();
    }
    private void OnDisable()
    {
        PlayerInputActions.Disable();
    }
    void CheckBattery()// уменьшение заряда батареи
    {
        NumberOfLivesNum--;
        Hp.fillAmount = NumberOfLivesNum / IndexHealthNum;
        HpText.text = $"{ NumberOfLivesNum} / {IndexHealthNum}";
    }
    void Start()
    {
        NumberOfLivesNum = PlayerPrefs.GetInt("HealthNum");
        IndexHealthNum = PlayerPrefs.GetInt("MaxHealthNum");
        Hp.fillAmount = NumberOfLivesNum / IndexHealthNum;
        HpText.text = $"{ NumberOfLivesNum} / {IndexHealthNum.ToString()}";
    }
    public void TakeDamageNum(int Damage)
    {
        if (NumberOfLivesNum > 0)
        {
            NumberOfLivesNum -= Damage;
            Hp.fillAmount = NumberOfLivesNum / IndexHealthNum;
        }
        HpText.text = $"{ NumberOfLivesNum} / {IndexHealthNum.ToString()}";
    }
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
