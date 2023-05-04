using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGame : MonoBehaviour
{
    [SerializeField] private AudioSource Source;
    [SerializeField] private AudioClip[] Clip;
    public KeyInteractionCard keyInteractionCard;

    public void CheckExit()
    {
        if (keyInteractionCard.AvailabilityKeyCard)
        {
            Source.PlayOneShot(Clip[0]);
            Debug.Log("Дверь открыта");
        }
        else
        {
            Source.PlayOneShot(Clip[1]);
            Debug.Log("Где карта");
        }
    }
}
