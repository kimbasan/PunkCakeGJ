using System;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public UnityEvent<Vector3, Boolean> interaction;

    public void Interact(Vector3 actorPosition, bool isClone = false)
    {
        Debug.Log("interaction");
        interaction?.Invoke(actorPosition, isClone);
    }
}
