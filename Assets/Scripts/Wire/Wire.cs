using UnityEngine;

public class Wire : MonoBehaviour
{
    public int interactableLayer;
    public int electricityOnLayer;
    public int electricityOffLayer;

    public MeshRenderer normalMesh;
    public GameObject tearedVisuals;

    [Header("Debug")]    
    [SerializeField] private bool electricityOn;
    [SerializeField] private bool isTeared;

    private void Awake()
    {
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

            normalMesh.enabled = false;
            tearedVisuals.SetActive(true);

            this.gameObject.layer = electricityOffLayer;
            Debug.Log("Wire teared");
        }
    }

    public void ResetWire(bool electricity)
    {
        electricityOn = electricity;
        isTeared = false;

        normalMesh.enabled = true;
        tearedVisuals.SetActive(false);

        gameObject.layer = interactableLayer;
    }
}
