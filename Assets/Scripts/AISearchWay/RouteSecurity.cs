using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RouteSecurity : MonoBehaviour
{
    [SerializeField] private List<SecurityMovement> _routeMovements;
    [SerializeField] private int _currentIndexPoint = 0;
    [SerializeField] private float _stepDistance;
    public Vector3 _startRoutePoint { get; private set; }
    private bool _revertRoute;

    private void Awake()
    {
        _startRoutePoint = this.transform.position;
    }

    public enum SecurityMovement
    {
        Up,
        Down,
        Left,
        Right,
        Wait
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Step();
        }
    }

    public void Step()
    {
        if (_revertRoute)
        {
            Move(GetVectorMove(_routeMovements[_currentIndexPoint]), -_stepDistance);
            if (_currentIndexPoint == 0)
            {
                _revertRoute = false;
            }
            else
            {
                _currentIndexPoint--;
            }
        }
        else
        {
            Move(GetVectorMove(_routeMovements[_currentIndexPoint]), _stepDistance);
            if (_currentIndexPoint == _routeMovements.Count - 1)
            {
                _revertRoute = true;
            }
            else
            {
                _currentIndexPoint++;
            }
        }
    }

    private void Move(Vector2 direction, float stepDistance)
    {
        this.transform.position += new Vector3(direction.x, 0, direction.y) * stepDistance;
    }

    private Vector2 GetVectorMove(SecurityMovement movement) 
    {
        if (movement == SecurityMovement.Wait)
        {
            return Vector2.zero;
        }

        if (movement == SecurityMovement.Up)
        {
            return Vector2.up;
        } 
        else if (movement == SecurityMovement.Right)
        {
            return Vector2.right;
        } 
        else if (movement == SecurityMovement.Down)
        {
            return Vector2.down;
        } 
        else if (movement == SecurityMovement.Left)
        {
            return Vector2.left;
        }

        Debug.LogError("Ќеопознаное направление потрулировани€");
        return Vector2.zero;
    }

    public void ResetRoute()
    {
        _currentIndexPoint = 0;
    }
}
