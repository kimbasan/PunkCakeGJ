using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIPointWay : MonoBehaviour
{
    [SerializeField] private Transform _startT;
    [SerializeField] private Transform _targetT;
    [SerializeField] private LayerMask _layerPlaneTrue;
    [SerializeField] private GameObject _prefabCurrentStep;
    private Transform _currentStepTransform;

    private List<PointWay> _exploredPointsWays;
    private List<PointWay> _routePoints;
    private PointWay _currentPoint;
    private PointWay _startPoint;
    private PointWay _targetPoint;
    private int _currentIndexRoutePoint = 0;

    [SerializeField] private float _stepDistance;
    private bool _isFindRoute;

    private void Awake()
    {
        _startPoint = new PointWay();
        _startPoint.Position = new Vector2(_startT.position.x, _startT.position.z);

        _targetPoint = new PointWay();
        _targetPoint.Position = new Vector2(_targetT.position.x, _targetT.position.z);

        _exploredPointsWays = new List<PointWay>();
        _currentPoint = _startPoint;
        _exploredPointsWays.Add(_currentPoint);
        _currentStepTransform = Instantiate(_prefabCurrentStep).GetComponent<Transform>();

        _routePoints = new List<PointWay>();

        GoToPoint(_currentPoint, transform);
        GoToPoint(_currentPoint, _currentStepTransform);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (_isFindRoute == false)
            {
                Step();
            }
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            if (_isFindRoute)
            {
                MoveStep();
            }
        }
    }

    public void Step()
    {
        if (_currentPoint == null)
        {
            Debug.LogError("Не возможно пройти до точки!!!");
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
                    Debug.LogError("Точка не найдена!");
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
            GoToPoint(_currentPoint, _currentStepTransform);
            return;
        }

        _currentPoint = _currentPoint.GetMinOpenNearPoint();
        GoToPoint(_currentPoint, _currentStepTransform);

        if (_currentPoint.Position == _targetPoint.Position)
        {
            _routePoints.Add(_currentPoint);
            _isFindRoute = true;
        }
    }

    public void MoveStep()
    {
        GoToPoint(_routePoints[_currentIndexRoutePoint], transform);
        if (_currentIndexRoutePoint == _routePoints.Count - 1)
        {
            return;
        }
        _currentIndexRoutePoint += 1;
    }

    private bool CheckPlane(Vector2 direction2)
    {
        bool movable = false;
        Vector3 direction = new Vector3(direction2.x, 0, direction2.y);
        RaycastHit Hit;
        Ray Ray = new Ray(_currentStepTransform.position, direction);//направляет луч
        Debug.DrawRay(Ray.origin, Ray.direction * 1.5f);//рисует луч (короткий промежуток)
        if (Physics.Raycast(_currentStepTransform.position, direction, out Hit, 1.5f, _layerPlaneTrue))//проверка, есть ли в направлении Collider со слоем
        {
            movable = true;
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
        vartransform.position = new Vector3(point.Position.x, vartransform.position.y, point.Position.y);
    }
}
