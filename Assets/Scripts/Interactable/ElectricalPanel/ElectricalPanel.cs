using System.Collections.Generic;
using UnityEngine;

public class ElectricalPanel : MonoBehaviour
{
    [SerializeField] private List<Wire> connectedWires;

    [SerializeField] private bool startingElectricityOn;
    private bool electricityOn;


    private void Start()
    {
        var levelController = FindAnyObjectByType<LevelController>();
        if (levelController != null)
        {
            levelController._cloneEvent += ResetPanel;
        }
        ResetPanel();
    }
    public void SwitchElectricity()
    {
        electricityOn= !electricityOn;
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
    }

}
