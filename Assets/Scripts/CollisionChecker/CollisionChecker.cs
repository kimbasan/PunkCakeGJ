using System;
using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    public event Action PlayerCollision;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag(Constants.CLONE_TAG) || collision.gameObject.CompareTag("Security") || collision.gameObject.CompareTag("ClosedDoor"))
        {
            PlayerCollision?.Invoke();
        }
    }
}