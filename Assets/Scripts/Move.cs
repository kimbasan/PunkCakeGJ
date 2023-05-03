using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Move : MonoBehaviour
{
    private Rigidbody RbPlayer;
    private PlayerInputActions PlayerInputActions;//������ �� ����������
    private Vector2 MoveDirection;// 1)������ �����������, 2)������ ��������
    public float Speed;
    //�������� �� �����
    public LayerMask LayerMask;//������ �� �����
    public GameObject[] ItemsArray = new GameObject[2];//������ ���������
    float Distance = 1.5f;//��������� ����
    Ray[] Ray = new Ray[4];//������ ����������� �����

    private void Awake()
    {
        PlayerInputActions = new PlayerInputActions();
    }
    private void OnEnable()
    {
        PlayerInputActions.Enable();
    }
    private void OnDisable()
    {
        PlayerInputActions.Disable();
    }
    private void Start()
    {
        RbPlayer = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        MoveDirection = PlayerInputActions.Player.Move.ReadValue<Vector2>();
        Moved();
    }
    public void Moved()
    {
        Vector3 Move = new Vector3(MoveDirection.x, 0, MoveDirection.y);
        RbPlayer.velocity = new Vector3(Move.x * Speed, Move.y, Move.z * Speed);
        //������� �� �����
        RayItems();
  
    }
    public void RayItems()//������ ���������� ����� �����������
    {
        RaycastHit Hit;//��� ��������� �� �������            
        Ray[0] = new Ray(transform.position, Vector3.forward);
        Ray[1] = new Ray(transform.position, Vector3.right);
        Ray[2] = new Ray(transform.position, Vector3.back);
        Ray[3] = new Ray(transform.position, Vector3.left);
        for (int i = 0; i < Ray.Length; i++)
        {
            if (Physics.Raycast(Ray[i], out Hit, Distance, LayerMask))//��������, ���� �� � ����������� ���� Collider �� �����
            {
                for(int j = 0; j < ItemsArray.Length; j++)
                {
                    if (ItemsArray[j] == null && !ItemsArray.Contains(Hit.transform.gameObject))//�������� �� ������������� ������� � ���� ����� �� ���������
                    {
                        ItemsArray[j] = Hit.collider.gameObject;
                        TextActionOn(j);
                    }
                }
            }
            for(int j = 0; j < ItemsArray.Length; j++)//��������� ��� � ��������� ������� � ������� �� ������������, ���� ��� ������������ � ��� �� ������� �� �������
            {
                if (ItemsArray[j] != null && !Physics.Raycast(transform.position, ItemsArray[j].transform.position, Distance, LayerMask))
                {
                    TextActionOFF(j);
                    ItemsArray[j] = null;
                }
            }
            if(ItemsArray[0] == null && ItemsArray[1] != null)//��������� ���� 0 ������ ������,� 1 ���, �� ���������� ��� � ������
            {
                TextActionOFF(0);
                TextActionOFF(1);
                ItemsArray[0] = ItemsArray[1];
                TextActionOn(0);
                ItemsArray[1] = null;
            }
        }
    }
    public void TextActionOn(int Index)
    {
        if (ItemsArray[Index] != null)
        {
            //��������� ������ � ���������
            if (Index == 0)
            {
                ItemsArray[0].GetComponent<AssignmentOfKeyInteraction>().TurningOffText();
                ItemsArray[0].GetComponent<AssignmentOfKeyInteraction>().OnText(Index);
            }
            if (Index == 1)
            {
                ItemsArray[1].GetComponent<AssignmentOfKeyInteraction>().TurningOffText();
                ItemsArray[1].GetComponent<AssignmentOfKeyInteraction>().OnText(Index);
            }
        }
    }
    public void TextActionOFF(int Index)
    {
        if (ItemsArray[Index] != null)//�������� ������� �������
        {
           //���������� ������ � ���������
            if (Index == 0)
            {
                ItemsArray[0].GetComponent<AssignmentOfKeyInteraction>().OffText(Index);
            }
            if (Index == 1)
            {
                ItemsArray[1].GetComponent<AssignmentOfKeyInteraction>().OffText(Index);
            }
        }
    }
}