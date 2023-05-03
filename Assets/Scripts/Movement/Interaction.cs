using System.Linq;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public LayerMask interactableLayer; //поиски по маске
    [SerializeField] private float tileSize; // сделать константу для всех

    public GameObject[] ItemsArray = new GameObject[2];//объект окружения
    private float RayDistance = Constants.TILE_SIZE;//дистанция луча
    Ray[] Ray = new Ray[4];//массив направления лучей

    public void RayItems()//должен вызываться после перемещения
    {
        RaycastHit Hit;//луч попадания по объекту            
        Ray[0] = new Ray(transform.position, Vector3.forward);
        Ray[1] = new Ray(transform.position, Vector3.right);
        Ray[2] = new Ray(transform.position, Vector3.back);
        Ray[3] = new Ray(transform.position, Vector3.left);
        Debug.DrawRay(transform.position, Vector3.forward * RayDistance, Color.red, 1, true);
        Debug.DrawRay(transform.position, Vector3.right * RayDistance, Color.red, 1, true);
        Debug.DrawRay(transform.position, Vector3.back * RayDistance, Color.red, 1, true);
        Debug.DrawRay(transform.position, Vector3.left * RayDistance, Color.red, 1, true);

        for (int j = 0; j < ItemsArray.Length; j++) //выключаем подсказки и удаляем объект
        { 
            if (ItemsArray[j] != null)
            {
                TextActionOFF(j);
                ItemsArray[j] = null;
            }
        }
        
        // ищем объекты и включаем на них подсказки
        for (int i = 0; i < Ray.Length; i++)
        {
            if (Physics.Raycast(Ray[i], out Hit, RayDistance, interactableLayer))//проверка, есть ли в направлении луча Collider со слоем
            {
               
                for (int j = 0; j < ItemsArray.Length; j++)
                {
                    if (ItemsArray[j] == null && !ItemsArray.Contains(Hit.collider.gameObject))//проверка на заполненность массива и если можно до заполняет
                    {
                        ItemsArray[j] = Hit.collider.gameObject;
                        TextActionOn(j);
                    }
                }
            }
        }
        
        if (ItemsArray[0] == null && ItemsArray[1] != null)//проверяет если 0 индекс пустой,а 1 нет, то перемещает его в первый
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
            //включение текста и подсветки
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
        if (ItemsArray[Index] != null)//проверка наличия объекта
        {
            //выключение текста и подсветки
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
