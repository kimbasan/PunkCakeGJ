using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask LayerMask;
    public Quests quests;

    public void CheckQuests(int IndexQuest)
    {
        if (quests.IndexEducation == IndexQuest)
        {
            quests.CheckEducationQuest();
        }
    }
}
