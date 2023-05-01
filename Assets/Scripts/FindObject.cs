using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FindObject : MonoBehaviour
{
    public TextMeshProUGUI ActionText;
    public GameObject ObjectFind;
    private void Start()
    {
        ActionText.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ObjectDefinition>() != null)
        {
            if(other.GetComponent<ObjectDefinition>().IndexObject == 0)
            {
                GetComponent<ScoreMoney>().AccountIncrease();
                Destroy(other.gameObject);
            }
            else if(other.GetComponent<ObjectDefinition>() != null)
            {
                ActionText.enabled = true;
                if (other.GetComponent<ObjectDefinition>().IndexObject == 1)
                {
                    GetComponent<KeyInteractionCard>().CheckKeyCard = true;                   
                }
                else if (other.GetComponent<ObjectDefinition>().IndexObject == 2)
                {
                    GetComponent<KeyInteractionCard>().CheckDoor = true;
                }
                ObjectFind = other.gameObject;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ObjectDefinition>() != null)
        {
            ActionText.enabled = false;
            if (other.GetComponent<ObjectDefinition>().IndexObject == 1)
            {
                GetComponent<KeyInteractionCard>().CheckKeyCard = false;
            }
            else if (other.GetComponent<ObjectDefinition>().IndexObject == 2)
            {
                GetComponent<KeyInteractionCard>().CheckDoor = false;
            }
            ObjectFind = null;
        }
    }
}
