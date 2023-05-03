using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreMoney : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Score;
    [SerializeField] private AudioClip Clip;
    [SerializeField] private AudioSource Source;
    private PlayerInputActions PlayerInputActions;
    private int ScoreNum;
    bool BoolMoney;

    private void Awake()
    {
        PlayerInputActions = new PlayerInputActions();
        PlayerInputActions.Player.Action.performed += context => AccountIncrease();
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
        ScoreNum = 0;
        Score.text = ScoreNum.ToString();
    }
    public void CheckMoney()
    {
        BoolMoney = true;
    }
    public void AccountIncrease()
    {
        if (BoolMoney)
        {
            Source.PlayOneShot(Clip);
            ScoreNum++;
            Score.text = ScoreNum.ToString();
            BoolMoney = false;
            Destroy(gameObject);
        }
    }
}
