using System;
using UnityEngine;
using UnityEngine.Events;

public class Wire : MonoBehaviour
{
    public int interactableLayer;
    public int electricityOnLayer;
    public int electricityOffLayer;

    public Material normalMaterial;
    public Material tearedMaterial;

    [Header("Debug")]    
    [SerializeField] private bool electricityOn;
    [SerializeField] private bool isTeared;
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer= GetComponent<MeshRenderer>();
        electricityOn = true;
        isTeared = false;
    }

    public void SetElectricity(bool electricityOn)
    {
        this.electricityOn = electricityOn;
        if (isTeared)
        {
            if (electricityOn)
            {
                Debug.Log("Wire become dangerous");
                this.gameObject.layer = electricityOnLayer;

            } else
            {
                Debug.Log("Wire become safe");
                this.gameObject.layer = electricityOffLayer;
            }
        }
    }

    public void TearWire()
    {
        if (electricityOn)
        {
            // убить игрока
            Debug.Log("Player killed");
            var levelController = FindAnyObjectByType<LevelController>();
            levelController?.Dead();
            this.gameObject.layer = electricityOnLayer;
        } else
        {
            // порвать кабель
            isTeared = true;
            meshRenderer.material = tearedMaterial;
            this.gameObject.layer = electricityOffLayer;
            Debug.Log("Wire teared");
        }
    }

    public void ResetWire(bool electricity)
    {
        electricityOn = electricity;
        isTeared = false;
        meshRenderer.material = normalMaterial;
        gameObject.layer = interactableLayer;
    }
}
