using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vizor : MonoBehaviour
{
    [SerializeField] public bool DetectedPlayer = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Clone")
        {
            DetectedPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Clone")
        {
            DetectedPlayer = false;
        }
    }
}
