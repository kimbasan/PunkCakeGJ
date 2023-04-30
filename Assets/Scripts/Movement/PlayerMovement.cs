using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public event EventHandler PlayerMoved;
    private PlayerInputActions PlayerInputActions;//скрипт на управление
    private Vector2 MoveDirection;// 1)вектор направления
    private Vector3 TransformVector;//вектор направления луча
    private Queue<Movements> movementsQueue;
    public LayerMask LayerMask;//маска, которую ищет луч
    public int NumberOfSteps, NumberOfStepsLeft;//1)общее количесвто ходов, 2)оставшееся количество ходов

    public enum Movements
    {
        Up, 
        Down, 
        Left,
        Right,
        Wait, Interact
    }

    private void Awake()
    {
        PlayerInputActions = new PlayerInputActions();
        PlayerInputActions.Player.Move.performed += context => MovePlane();
        NumberOfStepsLeft = NumberOfSteps;
        movementsQueue = new Queue<Movements>();
    }
    private void OnEnable()
    {
        PlayerInputActions.Enable();
    }
    private void OnDisable()
    {
        PlayerInputActions.Disable();
    }
    private void MovePlane()
    {//выбор направления
        if (NumberOfStepsLeft > 0)
        {
            MoveDirection = PlayerInputActions.Player.Move.ReadValue<Vector2>();
            Vector3 Move = new Vector3(transform.position.x + MoveDirection.x, transform.position.y, transform.position.z + MoveDirection.y);
            TransformVector = new Vector3(MoveDirection.x, 0, MoveDirection.y);

            // если можно перейти
            if (CheckPlane())
            {
                // повернутся
                transform.rotation = Quaternion.LookRotation(TransformVector);

                transform.position = new Vector3(Move.x, Move.y, Move.z);//перемещение
                NumberOfStepsLeft--;
                RecordMove(MoveDirection);

                // событие чтобы сдвинулись другие клоны
                if (PlayerMoved!= null)
                {
                    PlayerMoved.Invoke(this, EventArgs.Empty);
                }
            }
        }
    }
    private bool CheckPlane()
    {
        bool movable = false;
        RaycastHit Hit;
        Ray Ray = new Ray(transform.position, TransformVector);//направляет луч
        Debug.DrawRay(Ray.origin, Ray.direction * 1.5f);//рисует луч (короткий промежуток)
        if(Physics.Raycast(transform.position, TransformVector, out Hit, 1.5f, LayerMask))//проверка, есть ли в направлении Collider со слоем
        {
            movable = true;
        }
        return movable;
    }
    
    private void RecordMove(Vector2 move)
    {
        if (move == Vector2.up)
        {
            movementsQueue.Enqueue(Movements.Up);
        } else if (move == Vector2.down)
        {
            movementsQueue.Enqueue(Movements.Down);
        } else if (move == Vector2.left)
        {
            movementsQueue.Enqueue(Movements.Left);
        } else if (move == Vector2.right)
        {
            movementsQueue.Enqueue(Movements.Right);
        }
    }

    public Queue<Movements> GetMovements()
    {
        return movementsQueue;
    }
}
