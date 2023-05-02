using System.Collections.Generic;
using UnityEngine;

public class Water: MonoBehaviour
{
    [SerializeField] private MeshRenderer waterRender;
    [SerializeField] private Material waterMaterial;
    [SerializeField] private Material electricMaterial;
    [SerializeField] private int waterDefaultLayer;
    [SerializeField] private int electricWaterLayer;

    [Header("Debug")]
    [SerializeField] private List<GameObject> waterList;
    [SerializeField] private bool isElectric = false;

    public void SetWaterList(ref List<GameObject> waterList)
    {
        this.waterList = waterList;
    }

    // Триггер на провод
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Water trigger");
        Debug.Log(other.gameObject.name);
        // включить электричество

        if (other.CompareTag("Wire"))
        {
            foreach (GameObject obj in waterList)
            {
                var water = obj.GetComponent<Water>();
                water.Electricity(!isElectric); // Переключить воду в электрическое/неэлектрическое состояние
            }
        } else
        {
            if (isElectric)
            {
                // если охранник, вырубить, если игрок - проиграть
                Debug.Log("Someone died");
            }
        }
    }

    public void Electricity(bool on)
    {
        if (on)
        {
            isElectric= true;
            gameObject.layer = electricWaterLayer;
            waterRender.material= electricMaterial;
        }
        else
        {
            isElectric= false;
            gameObject.layer = waterDefaultLayer;
            waterRender.material = waterMaterial;
        }
    }
}
