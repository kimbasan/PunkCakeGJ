using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIPointWay : MonoBehaviour
{
    //[SerializeField] private Transform _startT;
    //[SerializeField] private Transform _targetT;
    [SerializeField] private LayerMask _layerPlaneTrue;
    [SerializeField] private LayerMask _layerSecurity;
    //[SerializeField] private GameObject _prefabCurrentStep;
    //private Transform _currentStepTransform;

    private List<PointWay> _exploredPointsWays;
    private List<PointWay> _routePoints;
    private PointWay _currentPoint;
    private PointWay _startPoint;
    private PointWay _targetPoint;
    private int _currentIndexRoutePoint = 0;

    [SerializeField] private float _stepDistance;
    private bool _isFindRoute;
    public bool IsFinish;
    private Coroutine _coroutine;

    private void Awake()
    {
        _startPoint = new PointWay();
        //_startPoint.Position = new Vector2(_startT.position.x, _startT.position.z);

        _targetPoint = new PointWay();
        //_targetPoint.Position = new Vector2(_targetT.position.x, _targetT.position.z);

        _exploredPointsWays = new List<PointWay>();
        _currentPoint = _startPoint;
        _exploredPointsWays.Add(_currentPoint);
        //_currentStepTransform = Instantiate(_prefabCurrentStep).GetComponent<Transform>();

        _routePoints = new List<PointWay>();

        //GoToPoint(_currentPoint, transform);
        //GoToPoint(_currentPoint, _currentStepTransform);
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.H))
    //    {
    //        if (_isFindRoute == false)
    //        {
    //            SearchStep();
    //        }
    //    }
    //    else if (Input.GetKeyDown(KeyCode.M))
    //    {
    //        if (_isFindRoute)
    //        {
    //            MoveStep();
    //        }
    //    }
    //}

    public void SetStartAndEndPoints(Vector3 startPoint, Vector3 endPoint)
    {
        _startPoint.Position = new Vector2(startPoint.x, startPoint.z);
        _targetPoint.Position = new Vector2(endPoint.x, endPoint.z);
        _currentPoint = _startPoint;
        _currentPoint.NearPoints.Clear();
        _currentIndexRoutePoint = 0;
        _isFindRoute = false;
        IsFinish = false;
        _exploredPointsWays.Clear();
        _routePoints.Clear();
    }

    public void SearhRoute()
    {
        while (_isFindRoute == false)
        {
            SearchStep();
        }
    }

    private void SearchStep()
    {
        if (_currentPoint == null)
        {
            Debug.LogError("?? ???????? ?????? ?? ?????!!!");
            _isFindRoute = true;
            return;
        }

        while (_currentPoint.AddNewNearPoint(_currentPoint, _stepDistance, _targetPoint))
        {
            var lastNearPoint = _currentPoint.NearPoints.Last();

            if (CheckPlane(lastNearPoint.Position - _currentPoint.Position) == false)
            {
                lastNearPoint.ClosePoint();
                if (_currentPoint.SetValueEqualNearPoint(lastNearPoint) == false)
                {
                    Debug.LogError("????? ?? ???????!");
                }
            }
            _exploredPointsWays.Add(_currentPoint.NearPoints.Last());
        }

        _currentPoint.ClosePoint();
        _routePoints.Add(_currentPoint);
        _currentPoint.CheckAndCloseNearPoints(_exploredPointsWays);

        if (_currentPoint.CheckHaveOpenPoints() == false)
        {
            _currentPoint = GetMinOpenExploredPoint();
            //GoToPoint(_currentPoint, _currentStepTransform);
            return;
        }

        _currentPoint = _currentPoint.GetMinOpenNearPoint();
        //GoToPoint(_currentPoint, _currentStepTransform);

        if (_currentPoint.Position == _targetPoint.Position)
        {
            _routePoints.Add(_currentPoint);
            _isFindRoute = true;
        }
    }

    public void MoveStep()
    {
        if (_currentIndexRoutePoint > _routePoints.Count - 1)
        {
            Debug.LogError("???? ? ?????? ????!");
            IsFinish = true;
            return;
        }

        if (_currentIndexRoutePoint == _routePoints.Count - 1)
        {
            IsFinish = true;
            return;
        }
        _currentIndexRoutePoint += 1;
        GoToPoint(_routePoints[_currentIndexRoutePoint], transform);
    }

    private bool CheckPlane(Vector2 direction2)
    {
        Vector3 currentPosition = new Vector3(_currentPoint.Position.x, 0, _currentPoint.Position.y);
        bool movable = false;
        Vector3 direction = new Vector3(direction2.x, 0, direction2.y);
        RaycastHit Hit;
        Ray Ray = new Ray(currentPosition, direction);//?????????? ???
        Debug.DrawRay(Ray.origin, Ray.direction * _stepDistance);//?????? ??? (???????? ??????????)
        if (Physics.Raycast(currentPosition, direction, out Hit, _stepDistance, _layerPlaneTrue))//????????, ???? ?? ? ??????????? Collider ?? ?????
        {
            if (Physics.Raycast(currentPosition, direction, out Hit, _stepDistance, _layerSecurity))
            {
                return movable;
            }
            else
            {
                movable = true;
            }
        }
        return movable;
    }

    private PointWay GetMinOpenExploredPoint()
    {
        PointWay minPoint = new PointWay();
        minPoint.Distance = float.MaxValue;

        for (int i = _exploredPointsWays.Count - 1; i > 0; i--)
        {
            if (_exploredPointsWays[i].CheckHaveOpenPoints())
            {
                return _exploredPointsWays[i].GetMinOpenNearPoint();
            }
            else
            {
                _routePoints.Remove(_exploredPointsWays[i]);
            }
        }

        return null;
    }

    private void GoToPoint(PointWay point, Transform vartransform)
    {
        Turn(point, vartransform);
        _coroutine = StartCoroutine(Move(point, vartransform));
    }

    private IEnumerator Move(PointWay point, Transform vartransform)
    {
        while (vartransform.position != new Vector3(point.Position.x, vartransform.position.y, point.Position.y))
        {
            vartransform.position = Vector3.MoveTowards(vartransform.position, new Vector3(point.Position.x, vartransform.position.y, point.Position.y), 3 * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
    }

    private void Turn(PointWay point, Transform vartransform)
    {
        if (point.Position.y > vartransform.position.z)
        {
            ETurn(0, vartransform);
        }
        else if (point.Position.x > vartransform.position.x)
        {
            ETurn(90, vartransform);
        }
        else if (point.Position.y < vartransform.position.z)
        {
            ETurn(180, vartransform);
        }
        else if (point.Position.x < vartransform.position.x)
        {
            ETurn(270, vartransform);
        }

        else
        {
            Debug.LogError("???????????? ??????? ?????????");
            ETurn(0, this.transform);
        }
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
