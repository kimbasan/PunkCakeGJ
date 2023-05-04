using UnityEngine;
using TMPro;

public class AssignmentOfKeyInteraction : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] TextAction;//����� � ������� ��� ��������������
    [SerializeField] private Outline Outline;//�=���������

    private void Start()
    {
        TurningOffText();
        //Outline = GetComponent<Outline>();
        Outline.enabled = false;//��������� ����������
    }
    public void TurningOffText()//��������� �����
    {
        for (int i = 0; i < TextAction.Length; i++)
        {
            TextAction[i].enabled = false;
        }
    }
    public void OnText(int Index)//�������� ����� �� ������� �� Move
    {
        TextAction[Index].enabled = true;
        Outline.enabled = true;
    }
    public void OffText(int Index)//��������� ����� �� ������� �� Move
    {
        TextAction[Index].enabled = false;
        Outline.enabled = false;
    }
}
