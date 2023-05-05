using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;

[RequireComponent(typeof(AIPointWay))]
[RequireComponent(typeof(RouteSecurity))]
[RequireComponent (typeof(Animator))]
[RequireComponent(typeof(SecurityAnimManager))]
public class SecurityController : MonoBehaviour
{
    public enum SecurityState
    {
       Patrol,
       Pursuit,
       Seek,
       Return
    }

    [SerializeField] private SecurityState _myState = SecurityState.Patrol;
    [SerializeField] private List<Vizor> _myVizors;
    [SerializeField] private AIPointWay _myAIPointWay;
    [SerializeField] private RouteSecurity _myRouteSecurity;
    [SerializeField] private Animator _animator;
    [SerializeField] private SecurityAnimManager _animManager;
    [SerializeField] private LayerMask _wallLayerMask;
    [SerializeField] private LayerMask _playerLayerMask;
    [SerializeField] private Vector3 _lastPlayerPosition;
    private Vector3 _startPosition;

    private void Awake()
    {
        _myAIPointWay = gameObject.GetComponent<AIPointWay>();
        _myRouteSecurity = gameObject.GetComponent<RouteSecurity>();
        _startPosition = transform.position;
        _animator = gameObject.GetComponent<Animator>();
        _animManager = gameObject.GetComponent<SecurityAnimManager>();
        _animManager.MyAnimator = _animator;
    }

    public void Step()
    {
        if (CheckPlayerVis())
        {
            _myState = SecurityState.Pursuit;
            _lastPlayerPosition = GetNearestClone();
            _myAIPointWay.SetStartAndEndPoints(this.transform.position, _lastPlayerPosition);
            _myAIPointWay.SearhRoute();
            _myAIPointWay.MoveStep();
            _animManager.PlayWalk();
        }
        else if (_myState == SecurityState.Pursuit)
        {
            _myState = SecurityState.Seek;
            _myAIPointWay.MoveStep();
            _animManager.PlayWalk();
        }
        else if (_myState == SecurityState.Seek)
        {
            if (_myAIPointWay.IsFinish)
            {
                _myState = SecurityState.Return;
                _myAIPointWay.SetStartAndEndPoints(this.transform.position, _startPosition);
                _myAIPointWay.SearhRoute();
                LookAround();
            }

            _myAIPointWay.MoveStep();
            _animManager.PlayWalk();
        }
        else if (_myState == SecurityState.Return)
        {
            if (_myAIPointWay.IsFinish)
            {
                _myState = SecurityState.Patrol;
                _myRouteSecurity.ResetRoute();
            }

            _myAIPointWay.MoveStep();
            _animManager.PlayWalk();
        }
        else // _myState == SecurityState.Patrol
        {
            _myRouteSecurity.Step();
            _animManager.PlayWalk();
        }

        StartCoroutine(SearchWait());
    }

    /// <summary>
    /// Проверка на видимость игрока
    /// </summary>
    private bool CheckPlayerVis()
    {
        foreach (var item in _myVizors)
        {
            if (item.DetectedPlayer)
            {
                var direction = item.transform.position - this.transform.position;
                if (Physics.Raycast(this.transform.position, direction, Vector3.Distance(item.transform.position, this.transform.position), _wallLayerMask))
                {
                    return false;
                }
                else
                {
                    if (Physics.Raycast(this.transform.position, direction, Vector3.Distance(item.transform.position, this.transform.position), _playerLayerMask))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private Vector3 GetNearestClone()
    {
        Vector3 nearestClonePosition = new Vector3(10000, 10000, 10000);
        foreach (var item in _myVizors)
        {
            if (item.DetectedPlayer)
            {
                var direction = item.transform.position - this.transform.position;
                if (Physics.Raycast(this.transform.position, direction, Vector3.Distance(item.transform.position, this.transform.position), _wallLayerMask))
                {
                    continue;
                }
                else
                {
                    if (Vector3.Distance(item.transform.position, this.transform.position) < Vector3.Distance(nearestClonePosition, this.transform.position))
                    {
                        nearestClonePosition = item.transform.position;
                    }
                }
            }
        }
        return nearestClonePosition;
    }

    private void LookAround()
    {

    }

    private IEnumerator SearchWait()
    {
        yield return new WaitForSeconds(0.1f);
        yield return new WaitForEndOfFrame();

        if (CheckPlayerVis())
        {
            _myState = SecurityState.Pursuit;
            _lastPlayerPosition = GetNearestClone();
            _myAIPointWay.SetStartAndEndPoints(this.transform.position, _lastPlayerPosition);
            _myAIPointWay.SearhRoute();
        }

        yield return new WaitForEndOfFrame();

        UnDetectedVizors();
    }

    private void UnDetectedVizors()
    {
        for (int i = 0; i < _myVizors.Count; i++)
        {
            _myVizors[i].DetectedPlayer = false;
        }
    }

    public void RestartPosition()
    {
        this.transform.position = _startPosition;
        UnDetectedVizors();
        _myRouteSecurity.ResetRoute();
        _myState = SecurityState.Patrol;
    }
}
