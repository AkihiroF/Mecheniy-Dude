using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Source.Enemy
{
    public static class CreatorDirectoryEnemy
    {
        public static readonly List<PointInterest> PositionsPoints;
        private static List<PointInterest> _currentPointInterests;

        static CreatorDirectoryEnemy()
        {
            PositionsPoints = new List<PointInterest>();
        }

        public static void AddPoints(this List<PointInterest> points, List<Transform> newPoints)
        {
            foreach (var point in newPoints)
            {
                points.Add(new PointInterest()
                {
                    Position = point.position
                });
            }
        }

        public static List<PointInterest> CreateNewDirectory(Vector2 position)
        {
            _currentPointInterests = new List<PointInterest>(PositionsPoints);
            var currentCountPoints = Random.Range(PositionsPoints.Count / 2, PositionsPoints.Count - 1);
            var direction = new List<PointInterest>();
            direction.Add(GetNearestPoint(position));
            for (int i = 1; i < currentCountPoints; i++)
            {
                var point = GetPointInterest(direction[i - 1]);
                _currentPointInterests.Remove(point);
                direction.Add(point);
            }
            return direction;
        }
        private static PointInterest GetNearestPoint(Vector2 position)
        {
            var currentDistance = Vector2.Distance(position, _currentPointInterests[0].Position);
            PointInterest currentPoint = default;
            foreach (var point in _currentPointInterests)
            {
                var distance = Vector2.Distance(position, point.Position);
                if(distance == 0)
                    continue;
                if (distance <= currentDistance)
                {
                    currentDistance = distance;
                    currentPoint = point;
                }
            }
            return currentPoint;
        }

        private static PointInterest GetPointInterest(PointInterest currentPoint)
        {
            var first = Random.Range(0, 2) == 0;
            var nearestPoint = GetNearestPoint(currentPoint.Position);
            if (first)
                return nearestPoint;
            var sortedPoints = new List<PointInterest>(_currentPointInterests);
            sortedPoints.Sort((v1, v2) => 
                (v1.Position - currentPoint.Position).sqrMagnitude.CompareTo((v2.Position - currentPoint.Position).sqrMagnitude));

            var id = sortedPoints.IndexOf(nearestPoint);

            if (id+1 >= sortedPoints.Count-1)
            {
                return sortedPoints[id - 1];
            }
            return sortedPoints[id + 1];
        }

    }

    public struct PointInterest
    {
        public Vector2 Position;
    }
}