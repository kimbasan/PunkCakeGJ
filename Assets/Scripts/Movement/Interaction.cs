using System.Linq;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public LayerMask interactableLayer; //������ �� �����
    [SerializeField] private float tileSize; // ������� ��������� ��� ����

    public GameObject[] ItemsArray = new GameObject[2];//������ ���������
    private float RayDistance = Constants.TILE_SIZE;//��������� ����
    Ray[] Ray = new Ray[4];//������ ����������� �����

    public void RayItems()//������ ���������� ����� �����������
    {
        RaycastHit Hit;//��� ��������� �� �������            
        Ray[0] = new Ray(transform.position, Vector3.forward);
        Ray[1] = new Ray(transform.position, Vector3.right);
        Ray[2] = new Ray(transform.position, Vector3.back);
        Ray[3] = new Ray(transform.position, Vector3.left);
        Debug.DrawRay(transform.position, Vector3.forward * RayDistance, Color.red, 1, true);
        Debug.DrawRay(transform.position, Vector3.right * RayDistance, Color.red, 1, true);
        Debug.DrawRay(transform.position, Vector3.back * RayDistance, Color.red, 1, true);
        Debug.DrawRay(transform.position, Vector3.left * RayDistance, Color.red, 1, true);

        for (int j = 0; j < ItemsArray.Length; j++) //��������� ��������� � ������� ������
        { 
            if (ItemsArray[j] != null)
            {
                TextActionOFF(j);
                ItemsArray[j] = null;
            }
        }
        
        // ���� ������� � �������� �� ��� ���������
        for (int i = 0; i < Ray.Length; i++)
        {
            if (Physics.Raycast(Ray[i], out Hit, RayDistance, interactableLayer))//��������, ���� �� � ����������� ���� Collider �� �����
            {
               
                for (int j = 0; j < ItemsArray.Length; j++)
                {
                    if (ItemsArray[j] == null && !ItemsArray.Contains(Hit.collider.gameObject))//�������� �� ������������� ������� � ���� ����� �� ���������
                    {
                        ItemsArray[j] = Hit.collider.gameObject;
                        TextActionOn(j);
                    }
                }
            }
        }
        
        if (ItemsArray[0] == null && ItemsArray[1] != null)//��������� ���� 0 ������ ������,� 1 ���, �� ���������� ��� � ������
        {
            TextActionOFF(0);
            TextActionOFF(1);
            ItemsArray[0] = ItemsArray[1];
            TextActionOn(0);
            ItemsArray[1] = null;
        }
        CheckAction();
    }
    public void CheckAction()
    {
        for (int i = 0; i < ItemsArray.Length; i++)
        {
            if (ItemsArray[i] != null && ItemsArray[i].GetComponent<KeyInteractionCard>() != null)
            {
                ItemsArray[i].GetComponent<KeyInteractionCard>().KeyCard();
            }
            if(ItemsArray[i] != null && ItemsArray[i].GetComponent<ScoreMoney>() != null)
            {
                ItemsArray[i].GetComponent<ScoreMoney>().CheckMoney();
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

    public void DoAction()
    {
        if (ItemsArray[0] != null)
        {
            var interact = ItemsArray[0].GetComponent<Interactable>();
            interact?.Interact(transform.position);
        }
    }

    public void DoSecondaryAction()
    {
        if (ItemsArray[1] != null)
        {
            var interact = ItemsArray[1].GetComponent<Interactable>();
            interact?.Interact(transform.position);
        }
    }
}
