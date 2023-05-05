using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Water: MonoBehaviour
{
    [SerializeField] private MeshRenderer waterRender;
    
    [SerializeField] private int waterDefaultLayer;
    [SerializeField] private int electricWaterLayer;
    [Header("���� �������")]
    [SerializeField] private int wireOn;
    [SerializeField] private int wireOff;

    [Header("Debug")]
    [SerializeField] private List<GameObject> waterList;
    [SerializeField] private bool isElectric = false;
    [SerializeField] private GameObject electricityParticles;
    public void SetWaterList(ref List<GameObject> waterList)
    {
        this.waterList = waterList;
    }

    // ������� �� ������
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Water trigger");
        Debug.Log(other.gameObject.name);
        // �������� �������������

        if (other.CompareTag("Wire"))
        {
            bool electricity = false;
            if (other.gameObject.layer == wireOn)
            {
                electricity= true;
            }
            foreach (GameObject obj in waterList)
            {
                var water = obj.GetComponent<Water>();
                water.Electricity(electricity); // ����������� ���� � �������������/��������������� ���������
            }
        }
    }

    // ��������� � �������
    private void OnCollisionEnter(Collision collision)
    {
        if (isElectric)
        {
            // ���� ��������, ��������, ���� ����� - ���������
            if (collision.gameObject.CompareTag(Constants.CLONE_TAG))
            {
                var levelController = FindAnyObjectByType<LevelController>();
                levelController?.Dead();
                Debug.Log("Player died");
            }
        }
    }

    public void Electricity(bool on)
    {
        if (on)
        {
            isElectric= true;
            gameObject.layer = electricWaterLayer;
            electricityParticles.SetActive(true);
        }
        else
        {
            isElectric= false;
            gameObject.layer = waterDefaultLayer;
            electricityParticles.SetActive(false);
        }
    }
}
