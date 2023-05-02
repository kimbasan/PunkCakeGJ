using System;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent<Vector3> interaction;
    public void Interact(Vector3 actorPosition)
    {
        Debug.Log("interaction");
        interaction?.Invoke(actorPosition);
    }
}
