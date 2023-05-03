using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Move : MonoBehaviour
{
    private Rigidbody RbPlayer;
    private PlayerInputActions PlayerInputActions;//скрипт на управление
    private Vector2 MoveDirection;// 1)вектор направления, 2)вектор движения
    public float Speed;
    //вверхнее не нужно
    public LayerMask LayerMask;//поиски по маске
    public GameObject[] ItemsArray = new GameObject[2];//объект окружения
    float Distance = 1.5f;//дистанция луча
    Ray[] Ray = new Ray[4];//массив направления лучей

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
        //верхнее не нужно
        RayItems();
  
    }
    public void RayItems()//должен вызываться после перемещения
    {
        RaycastHit Hit;//луч попадания по объекту            
        Ray[0] = new Ray(transform.position, Vector3.forward);
        Ray[1] = new Ray(transform.position, Vector3.right);
        Ray[2] = new Ray(transform.position, Vector3.back);
        Ray[3] = new Ray(transform.position, Vector3.left);
        for (int i = 0; i < Ray.Length; i++)
        {
            if (Physics.Raycast(Ray[i], out Hit, Distance, LayerMask))//проверка, есть ли в направлении луча Collider со слоем
            {
                for(int j = 0; j < ItemsArray.Length; j++)
                {
                    if (ItemsArray[j] == null && !ItemsArray.Contains(Hit.transform.gameObject))//проверка на заполненность массива и если можно до заполняет
                    {
                        ItemsArray[j] = Hit.collider.gameObject;
                        TextActionOn(j);
                    }
                }
            }
            for(int j = 0; j < ItemsArray.Length; j++)//выпускает луч и проверяет объекты в массиве на столкновение, если нет столкновения с ним то удаляет из массива
            {
                if (ItemsArray[j] != null && !Physics.Raycast(transform.position, ItemsArray[j].transform.position, Distance, LayerMask))
                {
                    TextActionOFF(j);
                    ItemsArray[j] = null;
                }
            }
            if(ItemsArray[0] == null && ItemsArray[1] != null)//проверяет если 0 индекс пустой,а 1 нет, то перемещает его в первый
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
}