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
        // ���� ��� �������� �����, ���� ��� ����� - ���� ���������
    }

    private void OnTriggerExit(Collider other)
    {
        // ���� ��� wire - ����� ������� ������� ����
    }

    
}
