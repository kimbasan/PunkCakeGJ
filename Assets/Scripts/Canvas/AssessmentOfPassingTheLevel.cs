using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssessmentOfPassingTheLevel : MonoBehaviour
{
    [SerializeField] private Sprite StarEmpty, StarGood;
    [SerializeField] private Image[] LevelAssessment;
    [SerializeField] private Quests quests;
    public GameObject[] Panel;

    private void Start()
    {
        for(int i = 0; i < LevelAssessment.Length; i++)
        {
            LevelAssessment[i].sprite = StarEmpty;
        }
        for (int i = 0; i < Panel.Length; i++)
        {
            Panel[i].SetActive(false);
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
