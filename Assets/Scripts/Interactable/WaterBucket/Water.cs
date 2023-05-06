using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Water: MonoBehaviour
{
    [SerializeField] private MeshRenderer waterRender;
    
    [SerializeField] private int waterDefaultLayer;
    [SerializeField] private int electricWaterLayer;
    [Header("слои провода")]
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

    // Триггер на провод
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Water trigger");
        Debug.Log(other.gameObject.name);
        // включить электричество

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
                water.Electricity(electricity); // Переключить воду в электрическое/неэлектрическое состояние
            }
        }
    }

    // Коллиззия с игроком
    private void OnCollisionEnter(Collision collision)
    {
        if (isElectric)
        {
            // если охранник, вырубить, если игрок - проиграть
            if (collision.gameObject.CompareTag(Constants.CLONE_TAG))
            {
                var levelController = FindAnyObjectByType<LevelController>();
                levelController?.Dead();
                Debug.Log("Player died");
            }

            else if (collision.gameObject.CompareTag("Security"))
            {
                var levelController = FindAnyObjectByType<LevelController>();
                Instantiate(levelController.DeathSecurity, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
                levelController.RemoveSecurity(collision.gameObject.GetComponent<SecurityController>());
                Destroy(collision.gameObject);
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
