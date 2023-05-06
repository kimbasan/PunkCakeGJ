using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGame : MonoBehaviour
{
    [SerializeField] private AudioSource Source;
    [SerializeField] private AudioClip[] Clip;
    //[SerializeField] private AssessmentOfPassingTheLevel AssessmentOfPassingTheLevel;
    [SerializeField] GameObject EndCanvas;
    public KeyInteractionCard keyInteractionCard;

    public void CheckExit()
    {
        if (keyInteractionCard.AvailabilityKeyCard)
        {
            Source.PlayOneShot(Clip[0]);
            //AssessmentOfPassingTheLevel.StarUp();
            EndCanvas.GetComponent<AssessmentOfPassingTheLevel>().Panel[1].SetActive(true);
            EndCanvas.GetComponent<AssessmentOfPassingTheLevel>().StarUp();
            Debug.Log("Дверь открыта");
        }
        else
        {
            Source.PlayOneShot(Clip[1]);
            Debug.Log("Где карта");
        }
    }
}
