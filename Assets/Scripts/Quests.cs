using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Quests : MonoBehaviour
{
    //[SerializeField] private TextMeshProUGUI[] MainQuest;
    [SerializeField] private GameObject MainQuest;// FindTheKeycardQuest, CollectGlitterQuest, NeutralizeGuardQuest;
    [SerializeField] private GameObject[] AdditionalQuests, EducationalQuests, ProgressOfTheCompletedTask;
    [SerializeField] private bool[] AdditionalTasksReceivedBool, CompletingTheQuest;

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
        for(int i = 0; i < EducationalQuests.Length; i++)
        {
            EducationalQuests[i].SetActive(false);
        }

        //for (int i = 0; i < NeutralizeGuardQuest.Length; i++)
        //{
        //    NeutralizeGuardQuest[i].SetActive(false);
        //}
        //MainQuest[0].SetActive(true);
    }
    public void CheckProgressAdditionalQuest(int IndexQuest)
    {
        if (CompletingTheQuest[IndexQuest])
        {
            ProgressOfTheCompletedTask[IndexQuest].SetActive(true);
        }
    }
}
