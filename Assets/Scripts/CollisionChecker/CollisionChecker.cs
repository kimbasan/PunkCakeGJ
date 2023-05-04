using System;
using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    public Action PlayerCollision;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Constants.CLONE_TAG))
        {
            PlayerCollision?.Invoke();
        }
    }
}
