using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreMoney : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Score, ScoreTextNum;
    [SerializeField] private AudioClip Clip;
    [SerializeField] private AudioSource Source;
    public int ScoreNum;
    public bool BoolMoney;
    public Quests quests;

    private void Start()
    {
        ScoreNum = 0;
        ScoreTextNum.text = ScoreNum.ToString();
        Score.text = $"{ScoreNum} / 6";
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
            ScoreNum = int.Parse(ScoreTextNum.text);
            ScoreNum++;
            ScoreTextNum.text = ScoreNum.ToString();
            Score.text = $"{ScoreNum} / 6";
            BoolMoney = false;
            Destroy(gameObject);
            if(ScoreNum == 1)
            {
                quests.AdditionalQuests[1].SetActive(true);
            }
            if (ScoreNum == 6)
            {
                quests.ProgressOfTheCompletedTask[1].SetActive(true);
            }
        }

    }
}
