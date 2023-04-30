using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public event EventHandler PlayerMoved;
    private PlayerInputActions PlayerInputActions;//������ �� ����������
    private Vector2 MoveDirection;// 1)������ �����������
    private Vector3 TransformVector;//������ ����������� ����
    private Queue<Movements> movementsQueue;
    public LayerMask LayerMask;//�����, ������� ���� ���
    public int NumberOfSteps, NumberOfStepsLeft;//1)����� ���������� �����, 2)���������� ���������� �����

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
    {//����� �����������
        if (NumberOfStepsLeft > 0)
        {
            MoveDirection = PlayerInputActions.Player.Move.ReadValue<Vector2>();
            Vector3 Move = new Vector3(transform.position.x + MoveDirection.x, transform.position.y, transform.position.z + MoveDirection.y);
            TransformVector = new Vector3(MoveDirection.x, 0, MoveDirection.y);

            // ���� ����� �������
            if (CheckPlane())
            {
                // ����������
                transform.rotation = Quaternion.LookRotation(TransformVector);

                transform.position = new Vector3(Move.x, Move.y, Move.z);//�����������
                NumberOfStepsLeft--;
                RecordMove(MoveDirection);

                // ������� ����� ���������� ������ �����
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
        Ray Ray = new Ray(transform.position, TransformVector);//���������� ���
        Debug.DrawRay(Ray.origin, Ray.direction * 1.5f);//������ ��� (�������� ����������)
        if(Physics.Raycast(transform.position, TransformVector, out Hit, 1.5f, LayerMask))//��������, ���� �� � ����������� Collider �� �����
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
