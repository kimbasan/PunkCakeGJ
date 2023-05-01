using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    [SerializeField] private string tag = "Clone";
    public Action PlayerCollision;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Clone")
        {
            PlayerCollision?.Invoke();
        }
    }
}
