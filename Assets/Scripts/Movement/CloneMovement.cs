using System.Collections;
using System.Collections.Generic;
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
                case Movements.Wait: break;
                case Movements.Up: Move(Vector3.forward); break;
                case Movements.Down: Move(Vector3.back); break;
                case Movements.Left: Move(Vector3.left); break;
                case Movements.Right: Move(Vector3.right); break;
            }
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
