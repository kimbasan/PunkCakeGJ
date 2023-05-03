using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using static PlayerMovement;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class CloneMovement : MonoBehaviour
{
    /// <summary>
    /// ������ ������������� � ��������� ��� �������� ������ �������
    /// </summary>
    public void Initialization(PlayerMovement playerMovement, Queue<Movements> movements, float stepDistance)
    {
        _startPosition = gameObject.transform.position;
        _startRotation = gameObject.transform.rotation;

        _movementsQueue = new Queue<Movements>();
        _currentMovementsQueue = new Queue<Movements>();
        _playerMovement = playerMovement;
        foreach (var item in movements)
        {
            _movementsQueue.Enqueue(item);
        }
        _stepDistance = stepDistance;
        _playerMovement.PlayerMoved += PlayerMovement_PlayerMoved;

        foreach (var item in _movementsQueue)
        {
            _currentMovementsQueue.Enqueue(item);
        }
    }

    private Collider _myCollider;
    private float _stepDistance;
    private Vector3 _startPosition;
    private Quaternion _startRotation;
    private PlayerMovement _playerMovement { get; set; }
    private Queue<Movements> _currentMovementsQueue; // ������� ������� �����
    private Queue<Movements> _movementsQueue; // ���� �����

    // �������������� � ���������
    private Ray[] raysToCheck = new Ray[4];
    private GameObject[] ItemsArray = new GameObject[2];
    private float RayDistance = Constants.TILE_SIZE;
    [SerializeField] private LayerMask interactableLayer;
    private void PlayerMovement_PlayerMoved(object sender, System.EventArgs e)
    {
        if (gameObject.active == false)
        {
            return;
        }
        Movements result;
        if (_currentMovementsQueue.Count == 0)
        {
            gameObject.transform.position = _startPosition;
            gameObject.transform.rotation = _startRotation;
            gameObject.SetActive(false);

            foreach (var item in _movementsQueue)
            {
                _currentMovementsQueue.Enqueue(item);
            }
        }
        else if(_currentMovementsQueue != null && _currentMovementsQueue.TryDequeue(out result))
        {
            switch (result)
            {
                case Movements.Wait: UpdateInteractableObjects(); break;
                case Movements.Up: Move(Vector3.forward); break;
                case Movements.Down: Move(Vector3.back); break;
                case Movements.Left: Move(Vector3.left); break;
                case Movements.Right: Move(Vector3.right); break;
                case Movements.Action: DoAction(); break;
                case Movements.SecondaryAction: DoSecondaryAction(); break;
            }
        }
    }

    // ������� ������� ��� �������������� ��� ����� � ��������� ������ �� ���
    private void UpdateInteractableObjects()
    {
        raysToCheck[0] = new Ray(transform.position, Vector3.forward);
        raysToCheck[1] = new Ray(transform.position, Vector3.right);
        raysToCheck[2] = new Ray(transform.position, Vector3.back);
        raysToCheck[3] = new Ray(transform.position, Vector3.left);
        Debug.DrawRay(transform.position, Vector3.forward * RayDistance, Color.green, 3, true);
        Debug.DrawRay(transform.position, Vector3.right * RayDistance, Color.green, 3, true);
        Debug.DrawRay(transform.position, Vector3.back * RayDistance, Color.green, 3, true);
        Debug.DrawRay(transform.position, Vector3.left * RayDistance, Color.green, 3, true);
        for (int j = 0; j < ItemsArray.Length; j++) //��������� ��������� � ������� ������
        {
            if (ItemsArray[j] != null)
            {
                ItemsArray[j] = null;
            }
        }
        RaycastHit Hit;
        // ���� ������� � �������� �� ��� ���������
        for (int i = 0; i < raysToCheck.Length; i++)
        {
            if (Physics.Raycast(raysToCheck[i], out Hit, RayDistance, interactableLayer))//��������, ���� �� � ����������� ���� Collider �� �����
            {
                Debug.Log("Clone ray hit " + i);
                for (int j = 0; j < ItemsArray.Length; j++)
                {
                    if (ItemsArray[j] == null && !ItemsArray.Contains(Hit.collider.gameObject))//�������� �� ������������� ������� � ���� ����� �� ���������
                    {
                        ItemsArray[j] = Hit.collider.gameObject;
                    }
                }
            }
        }

        if (ItemsArray[0] == null && ItemsArray[1] != null)//��������� ���� 0 ������ ������,� 1 ���, �� ���������� ��� � ������
        {
            ItemsArray[0] = ItemsArray[1];
            ItemsArray[1] = null;
        }
    }

    private void DoAction()
    {
        if (ItemsArray[0] != null)
        {
            var interact = ItemsArray[0].GetComponent<Interactable>();
            interact?.Interact(transform.position, true);
        }
    }

    private void DoSecondaryAction()
    {
        if (ItemsArray[1] != null)
        {
            var interact = ItemsArray[1].GetComponent<Interactable>();
            interact?.Interact(transform.position, true);
        }
    }

    private void Start()
    {
        _myCollider = gameObject.GetComponent<Collider>();
    }

    private void Move(Vector3 direction)
    {
        Vector3 Move = new Vector3(transform.position.x + direction.x * _stepDistance, transform.position.y, transform.position.z + direction.z * _stepDistance);
        StartCoroutine(MoveAnim(Move));
        transform.rotation = Quaternion.LookRotation(direction);
    }

    private IEnumerator MoveAnim(Vector3 target)
    {
        //_isReady = false;
        while (transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, 5 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        //_isReady = true;

        UpdateInteractableObjects();
    }

    /// <summary>
    /// ������ ����������� ���������� ��� �������� �� ��������� �������, ����� �� ������ ������ � ������
    /// </summary>
    private IEnumerator SpawnActiveTimer()
    {
        _myCollider.enabled = false;
        yield return new WaitForSeconds(1);
        _myCollider.enabled = true;
    }

    /// <summary>
    /// ����� ���������, �� ��� ����� ������������ �� ��������� ������� ���������
    /// </summary>
    public void RestartPosition()
    {
        SpawnActiveTimer();
        gameObject.transform.position = _startPosition;
        gameObject.transform.rotation = _startRotation;
        _currentMovementsQueue.Clear();

        foreach (var item in _movementsQueue)
        {
            _currentMovementsQueue.Enqueue(item);
        }
    }
}
