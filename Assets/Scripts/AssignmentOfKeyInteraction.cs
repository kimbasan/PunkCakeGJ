using UnityEngine;
using TMPro;

public class AssignmentOfKeyInteraction : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] TextAction;//текст с буквами для взаимодействия
    [SerializeField] private Outline Outline;//б=подсветка

    private void Start()
    {
        TurningOffText();
        //Outline = GetComponent<Outline>();
        Outline.enabled = false;//выключает подстветку
    }
    public void TurningOffText()//выключает текст
    {
        for (int i = 0; i < TextAction.Length; i++)
        {
            TextAction[i].enabled = false;
        }
    }
    public void OnText(int Index)//включает текст по индексу из Move
    {
        TextAction[Index].enabled = true;
        Outline.enabled = true;
    }
    public void OffText(int Index)//выключает текст по индексу из Move
    {
        TextAction[Index].enabled = false;
        Outline.enabled = false;
    }
}
