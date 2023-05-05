using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssessmentOfPassingTheLevel : MonoBehaviour
{
    [SerializeField] private Sprite StarEmpty, StarGood;
    [SerializeField] private Image[] LevelAssessment;
    [SerializeField] private Quests quests;
    int o = 0;

    private void Start()
    {
        for(int i = 0; i < LevelAssessment.Length; i++)
        {
            LevelAssessment[i].sprite = StarEmpty;
        }
    }
    public void StarUp()
    {
        for(int i = 0; i < quests.NumQuest; i++)
        {
            LevelAssessment[i].sprite = StarGood;
        }
        //LevelAssessment[o].sprite = StarGood;
        //o++;
    }
}
