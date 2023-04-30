using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Runtime.Serialization.Formatters.Binary;

public class PlayerMovement : MonoBehaviour
{
    //public Queue<Movements> movementsQueue;

    //public event EventHandler PlayerMoved;
    private Rigidbody RbPlayer;
    private PlayerInputActions PlayerInputActions;//скрипт на управление
    private Vector2 MoveDirection, MoveVector;// 1)вектор направления, 2)вектор движения
    private Vector3 TransformVector;//вектор направления луча
    public LayerMask LayerMask;//маска, которую ищет луч
    public int NumberOfSteps, NumberOfStepsLeft;//1)общее количесвто ходов, 2)оставшееся количество ходов
    public List<Vector2> MemorizingThePath;//массив совершенного пути 
    private string FileName = "vectors.dat";

    private void Awake()
    {
        PlayerInputActions = new PlayerInputActions();
        //PlayerInputActions.Player.W.performed += W_performed;
        PlayerInputActions.Player.Move.performed += context => MovePlane();
        NumberOfStepsLeft = NumberOfSteps;
        //PlayerInputActions playerInputActions = new PlayerInputActions();        
        //playerInputActions.Enable();
        //playerInputActions.Player.W.performed += W_performed;
        //playerInputActions.Player.S.performed += S_performed;
        //playerInputActions.Player.A.performed += A_performed;
        //playerInputActions.Player.D.performed += D_performed;
        //playerInputActions.Player.Wait.performed += Wait_performed;

        //movementsQueue = new Queue<Movements>();
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
    private void MovePlane()
    {//выбор направления
        MoveDirection = PlayerInputActions.Player.Move.ReadValue<Vector2>();
        Vector3 Move = new Vector3(transform.position.x + MoveDirection.x, transform.position.y, transform.position.z + MoveDirection.y);
        TransformVector = new Vector3(MoveDirection.x, 0, MoveDirection.y);
        if(NumberOfStepsLeft > 0)
        {
            //проверка, чтобы вектор направления и движения совпадал
            if (MoveVector == MoveDirection)
            {
                transform.position = new Vector3(Move.x, Move.y, Move.z);//перемещение
                MoveVector = new Vector2(0, 0);//обнуление вектора перемещения
                NumberOfStepsLeft--;
            }
            else if (MoveVector != MoveDirection)
            {
                CheckPlane();
            }
        }
    }
    private void CheckPlane()
    {
        RaycastHit Hit;
        Ray Ray = new Ray(transform.position, TransformVector);//направляет луч
        Debug.DrawRay(Ray.origin, Ray.direction * 1.5f);//рисует луч (короткий промежуток)
        if(Physics.Raycast(transform.position, TransformVector, out Hit, 1.5f, LayerMask))//проверка, есть ли в направлении Collider со слоем
        {
            MemorizingThePath.Add(MoveDirection);
            MoveVector = MoveDirection;
        }               
    }
    public void SaveWay()//сохранение пройденного пути
    {
        string Data = "";
        for (int i = 0; i < MemorizingThePath.Count; i++)
        {
            Data += MemorizingThePath[i].x + "," + MemorizingThePath[i].y + ";";
        }
        PlayerPrefs.SetString("Positions", Data);
        //MemorizingThePath
    }
    //private void Wait_performed(InputAction.CallbackContext obj)
    //{
    //    movementsQueue.Enqueue(Movements.Wait);
    //    PlayerMoved?.Invoke(this, EventArgs.Empty);
    //}

    //private void W_performed(InputAction.CallbackContext context)
    //{
    //    Debug.Log(context);
    //    transform.position += Vector3.forward;
    //    movementsQueue.Enqueue(Movements.Up);
    //    PlayerMoved?.Invoke(this, EventArgs.Empty);
    //}

    //private void S_performed(InputAction.CallbackContext context)
    //{
    //    Debug.Log(context);
    //    transform.position += Vector3.back;
    //    movementsQueue.Enqueue(Movements.Down);
    //    PlayerMoved?.Invoke(this, EventArgs.Empty);
    //}
    //private void A_performed(InputAction.CallbackContext context)
    //{
    //    Debug.Log(context);
    //    transform.position += Vector3.left;
    //    movementsQueue.Enqueue(Movements.Left);
    //    PlayerMoved?.Invoke(this, EventArgs.Empty);
    //}
    //private void D_performed(InputAction.CallbackContext context)
    //{
    //    Debug.Log(context);
    //    transform.position += Vector3.right;
    //    movementsQueue.Enqueue(Movements.Right);
    //    PlayerMoved?.Invoke(this, EventArgs.Empty);
    //}

    //public enum Movements
    //{
    //    Up, Down, Left, Right, Wait
    //}

    //public void SaveData()
    //{
    //    SavingManager.Save(new PlayerMoves(movementsQueue));
    //}

    //public void LoadData()
    //{
    //    PlayerMoves moves = SavingManager.Load();
    //    Debug.Log("Loaded Moves");
    //    foreach(Movements move in moves.moves)
    //    {
    //        Debug.Log(move);
    //    }
    //}
}
