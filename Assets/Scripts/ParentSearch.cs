using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentSearch: MonoBehaviour
{
    public GameObject Parent;

    void Start()
    {
        Parent = GameObject.Find(transform.parent.name);
    }
}
