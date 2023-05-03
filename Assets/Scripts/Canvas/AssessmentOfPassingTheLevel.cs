using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssessmentOfPassingTheLevel : MonoBehaviour
{
    [SerializeField] private Sprite StarEmpty, StarGood;
    [SerializeField] private Image[] LevelAssessment;
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
        LevelAssessment[o].sprite = StarGood;
        o++;
    }
}
