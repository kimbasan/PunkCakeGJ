using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreMoney : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Score;
    private int ScoreNum;

    private void Start()
    {
        ScoreNum = 0;
        Score.text = ScoreNum.ToString();
    }
    public void AccountIncrease()
    {
        ScoreNum++;
        Score.text = ScoreNum.ToString();
    }
}
