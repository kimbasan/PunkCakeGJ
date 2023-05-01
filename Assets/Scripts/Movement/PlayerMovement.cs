using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public event EventHandler PlayerMoved;
    private PlayerInputActions PlayerInputActions;//скрипт на управление
    private Vector2 MoveDirection;// 1)вектор направления
    private Vector3 TransformVector;//вектор направления луча
    private Queue<Movements> movementsQueue;
    public LayerMask LayerMask;//маска, которую ищет луч
    public int NumberOfSteps, NumberOfStepsLeft;//1)общее количесвто ходов, 2)оставшееся количество ходов
    public Text _numberOfStepsText;
    /// <summary>
    /// Дальность передвижения за один шаг
    /// </summary>
    [Header("Дальность передвижения за один шаг")]
    public float StepDistance;

    public enum Movements
    {
        Up, 
        Down, 
        Left,
        Right,
        Wait, Interact
    }
    private bool _isReady;

    private void Awake()
    {
        _isReady = true;
        PlayerInputActions = new PlayerInputActions();
        PlayerInputActions.Player.Move.performed += context => MovePlane();
        PlayerInputActions.Player.Stay.performed += context => MovePlane(stay : true);
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
    private void MovePlane(bool stay = false)
    {//выбор направления
        if (NumberOfStepsLeft > 0 && _isReady)
        {
            MoveDirection = PlayerInputActions.Player.Move.ReadValue<Vector2>();
            Vector3 Move = new Vector3(transform.position.x + MoveDirection.x * StepDistance, transform.position.y, transform.position.z + MoveDirection.y * StepDistance); // Дальность перемещение это 1, по нажатию. 
            TransformVector = new Vector3(MoveDirection.x, 0, MoveDirection.y);

            if (stay)
            {
                NumberOfStepsLeft--;
                _numberOfStepsText.text = NumberOfStepsLeft.ToString();
                RecordMove(MoveDirection, stay);

                if (PlayerMoved != null)
                {
                    PlayerMoved.Invoke(this, EventArgs.Empty);
                }
            }
            else if (CheckPlane()) // Если можно пройти
            {
                // повернутся
                transform.rotation = Quaternion.LookRotation(TransformVector);

                StartCoroutine(MoveAnim(Move));//перемещение
                NumberOfStepsLeft--;
                _numberOfStepsText.text = NumberOfStepsLeft.ToString();
                RecordMove(MoveDirection, stay);

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
    
    private void RecordMove(Vector2 move, bool wait)
    {
        if (wait)
        {
            movementsQueue.Enqueue(Movements.Wait);
            return;
        }

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

    public void ClearMovements()
    {
        movementsQueue.Clear();
    }

    private IEnumerator MoveAnim(Vector3 target)
    {
        _isReady = false;
        while(transform.position != target)
        {
            gameObject.transform.position = Vector3.MoveTowards(transform.position, target, 5 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(0.2f);
        _isReady = true;
    }
}
