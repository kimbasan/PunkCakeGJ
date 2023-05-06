using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vizor : MonoBehaviour
{
    [SerializeField] public bool DetectedPlayer = false;
    [SerializeField] public GameObject Vision;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Clone")
        {
            StartCoroutine(TimerToDetect());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Clone")
        {
            DetectedPlayer = false;
        }
    }

    private IEnumerator TimerToDetect()
    {
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForEndOfFrame();
        DetectedPlayer = true;
    }
}
