using System;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalPanel : MonoBehaviour
{
    [SerializeField] private List<Wire> connectedWires;

    [SerializeField] private bool electricityOn = true;


    public void SwitchElectricity()
    {
        electricityOn= !electricityOn;
        Debug.Log("Electricity On=" + electricityOn);
        foreach (Wire wire in connectedWires)
        {
            wire.SetElectricity(electricityOn);
        }
    }

}
