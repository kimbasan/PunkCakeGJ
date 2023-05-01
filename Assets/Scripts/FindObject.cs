using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindObject : MonoBehaviour
{
    public GameObject ObjectFind;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ObjectDefinition>() != null)
        {
            if(other.GetComponent<ObjectDefinition>().IndexObject == 0)
            {
                GetComponent<ScoreMoney>().AccountIncrease();
                Destroy(other.gameObject);
            }
            else if(other.GetComponent<ObjectDefinition>().IndexObject == 1)
            {
                GetComponent<KeyInteractionCard>().CheckKeyCard = true;
                ObjectFind = other.gameObject;
            }
            else if (other.GetComponent<ObjectDefinition>().IndexObject == 2)
            {
                GetComponent<KeyInteractionCard>().CheckDoor = true;
                ObjectFind = other.gameObject;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ObjectDefinition>().IndexObject == 1)
        {
            GetComponent<KeyInteractionCard>().CheckKeyCard = false;
            ObjectFind = null;
        }
        else if (other.GetComponent<ObjectDefinition>().IndexObject == 2)
        {
            GetComponent<KeyInteractionCard>().CheckDoor = false;
            ObjectFind = null;
        }
    }
}
