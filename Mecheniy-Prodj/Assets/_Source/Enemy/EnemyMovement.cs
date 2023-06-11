using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

namespace _Source.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private float speed;
        private NavMeshAgent _agent;
        private List<PointInterest> _directory;
        private int _currentIdPoint;
        private float _stoppingDistance;
        private Vector3 _lastPosition;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = speed;
            _stoppingDistance = _agent.stoppingDistance;
            _agent.updateUpAxis = false;
            _agent.updateRotation = false;
            StartMoving();
            InvokeRepeating("CheckDestination", 0,0.3f);
        }

        private void StartMoving()
        {
            _directory = CreatorDirectoryEnemy.CreateNewDirectory(transform.position);
            _currentIdPoint = 0;
            _agent.SetDestination(_directory[_currentIdPoint].Position);
        }

        private void CheckDestination()
        {
            if (OnDestinationReached())
            {
                MoveToNextPoint();
                _lastPosition = transform.position;
            }
        }

        private void FixedUpdate()
        {
            var position = transform.position;
            var direction = position - _lastPosition;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf. Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            _lastPosition = position;
        }


        private void MoveToNextPoint()
        {
            _currentIdPoint++;
            if (_currentIdPoint < _directory.Count)
            {
                _agent.SetDestination(_directory[_currentIdPoint].Position);
            }
            else
            {
                StartMoving();
            }
        }

        private bool OnDestinationReached()
        {
            return _agent.remainingDistance <= _stoppingDistance;
        }
    }
}