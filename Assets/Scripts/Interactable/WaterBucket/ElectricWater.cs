using Unity.VisualScripting;
using UnityEngine;

public class ElectricWater : MonoBehaviour
{
    [SerializeField] private GameObject regularWater;
    [SerializeField] private MeshRenderer waterRender;
    [SerializeField] private Material waterMaterial;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger on electic water");
        // если это охранник убить, если это игрок - игра проиграна
    }

    private void OnTriggerExit(Collider other)
    {
        // если это wire - нужно вернуть обычную воду
    }

    
}
