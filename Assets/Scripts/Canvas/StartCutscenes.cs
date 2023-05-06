using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCutscenes : MonoBehaviour
{
    [SerializeField] private Canvas Cuscene;
    [SerializeField] private GameObject Button;
    [SerializeField] private Image[] Slider;
    public float Timer, time = 0;
    int Index = 0;
    private void Awake()
    {
        Button.SetActive(false);
        for (int i = 0; i < Slider.Length; i++)
        {
            Slider[i].color = new Color(1, 1, 1, 0);
        }
    }
    void Start()
    {
       // StartCoroutine(CutsceneCorotine(Timer));
    }
    private void Update()
    {
        if (time < Timer && Index < Slider.Length)
        {
            time += Time.deltaTime/1.2f;
            Slider[Index].color = new Color(1, 1, 1, time);
        }
        else if(Index < Slider.Length)
        {           
            time = 0;
            Index++;
        }
        if (Index == Slider.Length - 1)
        {
            Button.SetActive(true);
        }
    }
    public void StartButton()
    {
        Cuscene.enabled = false;
    }
    //IEnumerator CutsceneCorotine(float Timer)
    //{
    //for (int i = 0; i < Slider.Length; i++)
    //{
    //    Slider[i].SetActive(true);
    //    Slider[i].enabled = true;

    //    yield return new WaitForSeconds(Timer);
    //}


    //}
}
