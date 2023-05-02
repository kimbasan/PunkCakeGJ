using UnityEngine;

public class Wire : MonoBehaviour
{
    public int interactableLayer;
    public int electricityOnLayer;
    public int electricityOffLayer;

    private bool electricityOn;
    private bool isTeared;
    private void Awake()
    {
        electricityOn= true;
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
            this.gameObject.layer = electricityOnLayer;
        } else
        {
            // порвать кабель
            isTeared = true;
            this.gameObject.layer = electricityOffLayer;
            Debug.Log("Wire teared");
        }
    }
}
