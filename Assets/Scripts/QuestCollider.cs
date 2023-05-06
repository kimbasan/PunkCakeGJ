using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCollider : MonoBehaviour
{
    [SerializeField] private LayerMask LayerMask;
    public QuestTrigger QuestTrigger;
    public int IndexTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Clone") && IndexTrigger==0)//.gameObject.layer == LayerMask)
        {
            QuestTrigger.quests.AdditionalQuests[0].SetActive(true);
        }
        else if (other.CompareTag("Clone") && IndexTrigger == 1)//.gameObject.layer == LayerMask)
        {
            QuestTrigger.quests.AdditionalQuests[2].SetActive(true);
        }
    }
}
