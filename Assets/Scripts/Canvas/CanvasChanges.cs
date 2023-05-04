using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasChanges : MonoBehaviour
{
    [SerializeField] Camera Camera;

    void Awake()
    {
        Camera = Camera.main;
    }

    void LateUpdate()
    {
        transform.LookAt(new Vector3(Camera.transform.position.x, Camera.transform.position.y, Camera.transform.position.z));
        transform.Rotate(0, 180, 0);
    }
}
