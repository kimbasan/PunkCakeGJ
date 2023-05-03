using System.Collections.Generic;
using UnityEngine;

public class ElectricalPanel : MonoBehaviour
{
    [SerializeField] private List<Wire> connectedWires;

    [SerializeField] private bool startingElectricityOn;
    private bool electricityOn;

    [SerializeField] private Material electricityOnMaterial;
    [SerializeField] private Material electricityOffMaterial;
    private MeshRenderer meshRenderer;
    private void Start()
    {
        var levelController = FindAnyObjectByType<LevelController>();
        if (levelController != null)
        {
            levelController._cloneEvent += ResetPanel;
        }
        meshRenderer = GetComponent<MeshRenderer>();
        ResetPanel();
    }
    public void SwitchElectricity()
    {
        electricityOn= !electricityOn;
        if (!electricityOn)
        {
            meshRenderer.material = electricityOffMaterial;
        } else
        {
            meshRenderer.material = electricityOnMaterial;
        }
        Debug.Log("Electricity On=" + electricityOn);
        foreach (Wire wire in connectedWires)
        {
            wire.SetElectricity(electricityOn);
        }
    }

    private void ResetPanel()
    {
        electricityOn = startingElectricityOn;
        foreach(Wire wire in connectedWires)
        {
            wire.ResetWire(startingElectricityOn);
        }
        meshRenderer.material= electricityOnMaterial;
    }

}
