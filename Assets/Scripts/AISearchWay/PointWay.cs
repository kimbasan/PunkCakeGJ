using System;
using System.Collections.Generic;
using UnityEngine;

public class PointWay
{
    public bool IsOpen { get; private set; }
    public Vector2 Position { get => _position; set => _position = value; }
    private Vector2 _position;

    public List<PointWay> NearPoints { get => _nearPoints; set => _nearPoints = value; }
    private List<PointWay> _nearPoints;

    public float Distance { get => _distance; set => _distance = value; }
    private float _distance;

    public PointWay()
    {
        IsOpen = true;
        _nearPoints = new List<PointWay>();
    }

    /// <summary>
    /// Добавляет близкие точки в порядке(Верхняя, правая, нижня, левая) и назначается дистанция до цели
    /// </summary>
    /// <param name="parentPoint">Текущая точка</param>
    /// <param name="stepDistance">Дальность одного шага в метрах</param>
    /// <returns></returns>
    public bool AddNewNearPoint(PointWay parentPoint, float stepDistance, PointWay targetPoint)
    {
        if (NearPoints.Count < 4)
        {
            PointWay newNearPoint = new PointWay();
            
            if (NearPoints.Count == 0) // Верхняя точка
            {
                newNearPoint.Position = new Vector2(parentPoint.Position.x, parentPoint.Position.y + 1 * stepDistance);
            }
            else if (NearPoints.Count == 1) // Правая точка
            {
                newNearPoint.Position = new Vector2(parentPoint.Position.x + 1 * stepDistance, parentPoint.Position.y);
            }
            else if (NearPoints.Count == 2) // Нижняя точка
            {
                newNearPoint.Position = new Vector2(parentPoint.Position.x, parentPoint.Position.y - 1 * stepDistance);
            }
            else // Левая точка
            {
                newNearPoint.Position = new Vector2(parentPoint.Position.x - 1 * stepDistance, parentPoint.Position.y);
            }
            newNearPoint.Distance = GetDistance(newNearPoint, targetPoint);
            NearPoints.Add(newNearPoint);
            return true;
        }
        return false;
    }

    public void CheckAndCloseNearPoints(List<PointWay> pointWays)
    {
        if (NearPoints.Count == 0)
        {
            return;
        }

        foreach (var item in pointWays)
        {
            if (item.IsOpen == false)
            {
                foreach (var myPoint in NearPoints)
                {
                    if (myPoint.Position == item.Position)
                    {
                        myPoint.ClosePoint();
                    }
                }
            }
        }
    }

    public void ClosePoint()
    {
        IsOpen = false;
    }

    public PointWay GetMinOpenNearPoint()
    {
        PointWay minPoint = new PointWay();
        minPoint.Distance = float.MaxValue;

        foreach (var item in _nearPoints)
        {
            if (item.IsOpen)
            {
                if (item.Distance < minPoint.Distance)
                {
                    minPoint = item;
                }
            }
        }
        if (minPoint.Distance == float.MaxValue)
        {
            return null;
        }
        return minPoint;
    }

    public bool SetValueEqualNearPoint(PointWay pointWay)
    {
        for (int i = 0; i < NearPoints.Count; i++)
        {
            if (pointWay.Position == NearPoints[i].Position)
            {
                NearPoints[i] = pointWay;
                return true;
            }
        }
        return false;
    }

    public bool CheckHaveOpenPoints()
    {
        foreach (var item in NearPoints)
        {
            if (item.IsOpen)
            {
                return true;
            }
        }

        return false;
    }

    static public float GetDistance(PointWay currentPoint, PointWay targetPoint)
    {
        return Math.Abs(currentPoint.Position.x - targetPoint.Position.x) + Math.Abs(currentPoint.Position.y - targetPoint.Position.y);
    }
}
