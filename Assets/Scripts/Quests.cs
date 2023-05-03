using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Quests : MonoBehaviour
{
    [SerializeField] private GameObject MainQuest;
    [SerializeField] private GameObject[] AdditionalQuests, EducationalQuests, ProgressOfTheCompletedTask;
    [SerializeField] private bool[] AdditionalTasksReceivedBool, CompletingTheQuest;
    private int IndexEducation = 0;

    private void Start()
    {
        for (int i = 0; i < AdditionalQuests.Length; i++)
        {
            AdditionalQuests[i].SetActive(false);
            ProgressOfTheCompletedTask[i].SetActive(false);
            if (AdditionalTasksReceivedBool[i])
            {
                AdditionalQuests[i].SetActive(true);
            }
        }
        for(int i = 1; i < EducationalQuests.Length; i++)
        {
            EducationalQuests[i].SetActive(false);
        }
    }
    public void CheckAdditionalQuests(int IndexQuest)
    {
        if (AdditionalTasksReceivedBool[IndexQuest])
        {
            AdditionalQuests[IndexQuest].SetActive(true);
        }
    }
    public void CheckProgressAdditionalQuest(int IndexQuest)
    {
        if (CompletingTheQuest[IndexQuest])
        {
            ProgressOfTheCompletedTask[IndexQuest].SetActive(true);
        }
    }
    public void CheckEducationQuest()
    {
        if(IndexEducation < EducationalQuests.Length - 1)
        {
            EducationalQuests[IndexEducation].SetActive(false);
            IndexEducation++;
            EducationalQuests[IndexEducation].SetActive(true);
        }
        else if (IndexEducation < EducationalQuests.Length)
        {
            EducationalQuests[IndexEducation].SetActive(false);
        }
    }
}
