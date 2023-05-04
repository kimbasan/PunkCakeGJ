using System.Collections.Generic;
using UnityEngine;

public class ElectricalPanel : MonoBehaviour
{
    [SerializeField] private List<Wire> connectedWires;

    [SerializeField] private bool startingElectricityOn;
    [SerializeField] private Animator boxAnimator;
    [SerializeField] private LightController lightController;

    private bool electricityOn;

    private readonly string animParam = "electricity";
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
        lightController.SetLight(electricityOn);
        Debug.Log("Electricity On=" + electricityOn);
        boxAnimator.SetBool(animParam, electricityOn);
        foreach (Wire wire in connectedWires)
        {
            wire?.SetElectricity(electricityOn);
        }
    }

    private void ResetPanel()
    {
        electricityOn = startingElectricityOn;
        foreach(Wire wire in connectedWires)
        {
            wire?.ResetWire(startingElectricityOn);
        }
        lightController.SetLight(electricityOn);
        boxAnimator.SetBool(animParam, electricityOn);
    }

}
