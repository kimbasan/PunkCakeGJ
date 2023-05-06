using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCollisionCheker : MonoBehaviour
{
    public event Action Stan;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Security"))
        {
            Stan?.Invoke();
            Debug.Log("Стан");
        }
    }
}
