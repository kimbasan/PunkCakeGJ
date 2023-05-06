using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static LevelController;

public class PlayerMovement : MonoBehaviour
{
    public event EventHandler PlayerMoved;
    public PlayerInputActions PlayerInputActions;//ñêðèïò íà óïðàâëåíèå
    private Vector2 MoveDirection;// 1)âåêòîð íàïðàâëåíèÿ
    private Vector3 TransformVector;//âåêòîð íàïðàâëåíèÿ ëó÷à
    private Queue<Movements> movementsQueue;
    public LayerMask LayerMask;//ìàñêà, êîòîðóþ èùåò ëó÷
    public int NumberOfSteps, NumberOfStepsLeft;//1)îáùåå êîëè÷åñâòî õîäîâ, 2)îñòàâøååñÿ êîëè÷åñòâî õîäîâ
    public TextMeshProUGUI _numberOfStepsText;
    public Interaction interaction;
    public CameraState MyCameraState;
    private PlayerAnimManager _playerAnimManager;
    public Action PlayerStep;
    /// <summary>
    /// Äàëüíîñòü ïåðåäâèæåíèÿ çà îäèí øàã
    /// </summary>
    [Header("Äàëüíîñòü ïåðåäâèæåíèÿ çà îäèí øàã")]
    public float StepDistance;
    public bool IsPlayerStep = true;

    public enum Movements
    {
        Up, 
        Down, 
        Left,
        Right,
        Wait,
        Action, SecondaryAction
    }
    private bool _isReady;

    private void Awake()
    {
        _isReady = true;
        PlayerInputActions = new PlayerInputActions();
        _playerAnimManager = GetComponent<PlayerAnimManager>();
        _playerAnimManager._animator = this.gameObject.GetComponent<Animator>();
        PlayerInputActions.Player.Move.performed += context => MovePlane();
        PlayerInputActions.Player.Stay.performed += context => MovePlane(stay : true);
        PlayerInputActions.Player.Action.performed += context => DoAction();
        PlayerInputActions.Player.SecondaryAction.performed += context => DoSecondaryAction();
        NumberOfStepsLeft = NumberOfSteps;
        movementsQueue = new Queue<Movements>();
    }

    private void DoAction()
    {
        if (IsPlayerStep)
        {
            NumberOfStepsLeft--;
            PlayerStep?.Invoke();
            RecordMove(Movements.Action);
            interaction?.DoAction();
            PlayerMoved?.Invoke(this, EventArgs.Empty);
            StartCoroutine(InteractAnim());
        }
    }
    private void DoSecondaryAction()
    {
        if (IsPlayerStep)
        {
            NumberOfStepsLeft--;
            PlayerStep?.Invoke();
            RecordMove(Movements.SecondaryAction);
            interaction?.DoSecondaryAction();
            PlayerMoved?.Invoke(this, EventArgs.Empty);
            StartCoroutine(InteractAnim());
        }
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
    {//âûáîð íàïðàâëåíèÿ
        if (NumberOfStepsLeft > 0 && _isReady && IsPlayerStep)
        {
            MoveDirection = PlayerInputActions.Player.Move.ReadValue<Vector2>();
            MoveDirection = ChangeDirection(MoveDirection);

            Vector3 Move = new Vector3(transform.position.x + MoveDirection.x * StepDistance, transform.position.y, transform.position.z + MoveDirection.y * StepDistance);
            TransformVector = new Vector3(MoveDirection.x, 0, MoveDirection.y);

            if (stay)
            {
                PlayerStep?.Invoke();
                NumberOfStepsLeft--;
                _numberOfStepsText.text = NumberOfStepsLeft.ToString();
                RecordMove(MoveDirection, stay);

                if (PlayerMoved != null)
                {
                    PlayerMoved.Invoke(this, EventArgs.Empty);
                }
            }
            else if (CheckPlane()) // Åñëè ìîæíî ïðîéòè
            {
                // ïîâåðíóòñÿ
                PlayerStep?.Invoke();
                transform.rotation = Quaternion.LookRotation(TransformVector);
                _playerAnimManager.PlayWalk();
                StartCoroutine(MoveAnim(Move));//ïåðåìåùåíèå
                NumberOfStepsLeft--;
                _numberOfStepsText.text = NumberOfStepsLeft.ToString();
                RecordMove(MoveDirection, stay);

                // ñîáûòèå ÷òîáû ñäâèíóëèñü äðóãèå êëîíû
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
        if(Physics.Raycast(transform.position, TransformVector, out Hit, 1.5f, LayerMask))//ïðîâåðêà, åñòü ëè â íàïðàâëåíèè Collider ñî ñëîåì
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

    private void RecordMove(Movements move)
    {
        movementsQueue.Enqueue(move);
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
        // ïðîâåðèòü åñòü ëè ðÿäîì íîâûå îáúåêòû äëÿ äåéñòâèé
        interaction.RayItems();
    }

    private IEnumerator InteractAnim() {
        _isReady = false;
        yield return new WaitForSeconds(1f);
        _isReady = true;
    }

    private Vector2 ChangeDirection(Vector2 inputDirection)
    {

        switch (MyCameraState)
        {
            case CameraState.Front:
                return inputDirection;

            case CameraState.Right:
                return new Vector2(inputDirection.y, -inputDirection.x);

            case CameraState.Back:
                return -inputDirection;

            case CameraState.Left:
                return new Vector2(-inputDirection.y, inputDirection.x);

            default:
                Debug.LogError("Неопознанное направление камеры!!!");
                return inputDirection;
        }
    }
}
