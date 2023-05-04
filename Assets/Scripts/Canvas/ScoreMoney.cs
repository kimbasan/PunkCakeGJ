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
    public int ScoreNum;
    public bool BoolMoney;
    public Quests quests;

    //private void Awake()
    //{
    //    PlayerInputActions = new PlayerInputActions();
    //    PlayerInputActions.Player.Action.performed += context => AccountIncrease();
    //}
    //private void OnEnable()
    //{
    //    PlayerInputActions.Enable();
    //}
    //private void OnDisable()
    //{
    //    PlayerInputActions.Disable();
    //}
    private void Start()
    {
        ScoreNum = 0;
        Score.text = ScoreNum.ToString();
    }
    public void CheckMoney()
    {
        BoolMoney = !BoolMoney;
    }
    public void AccountIncrease()
    {
        if (BoolMoney)
        {
            Source.PlayOneShot(Clip);
            ScoreNum = int.Parse(Score.text);
            ScoreNum++;
            Score.text = ScoreNum.ToString();
            BoolMoney = false;
            Destroy(gameObject);
            if(ScoreNum == 1)
            {
                quests.AdditionalQuests[1].SetActive(true);
            }
            if (ScoreNum == 3)
            {
                quests.ProgressOfTheCompletedTask[1].SetActive(true);
            }
        }

    }
}
