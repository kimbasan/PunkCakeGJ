using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

public class RouteSecurity : MonoBehaviour
{
    [SerializeField] private List<SecurityMovement> _routeMovements;
    [SerializeField] private int _currentIndexPoint = 0;
    [SerializeField] private float _stepDistance;
    public Vector3 _startRoutePoint { get; private set; }
    private bool _revertRoute;
    private Coroutine _coroutine;

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

    public void PreviousStep()
    {
        if (_revertRoute)
        {
            _currentIndexPoint--;
        }
        else
        {
            _currentIndexPoint++;
        }
    }

    private void Move(Vector2 direction, float stepDistance)
    {
        Turn(direction * _stepDistance / stepDistance, this.transform);
        var a = this.transform.position;
        a += new Vector3(direction.x, 0, direction.y) * stepDistance;

        _coroutine = StartCoroutine(EMove(a, this.transform));
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

    private IEnumerator EMove(Vector3 point, Transform vartransform)
    {
        while (vartransform.position != point)
        {
            vartransform.position = Vector3.MoveTowards(vartransform.position, point, 3 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
    }

    private void Turn(Vector2 direction, Transform vartransform)
    {
        if (direction == Vector2.up)
        {
            ETurn(0, vartransform);
        }
        else if (direction == Vector2.right)
        {
            ETurn(90, vartransform);
        }
        else if (direction == Vector2.down)
        {
            ETurn(180, vartransform);
        }
        else if (direction == Vector2.left)
        {
            ETurn(270, vartransform);
        }

        //else
        //{
        //    Debug.LogError("Ќеопознанный поворот охранника");
        //    ETurn(0, this.transform);
        //}
    }

    private void ETurn(float angle, Transform vartransform)
    {
        var rotationPoint = new Vector3(0, angle, 0);
        vartransform.rotation = Quaternion.Euler(rotationPoint);
    }

    public void StopMove()
    {
        StopCoroutine(_coroutine);
    }
}
