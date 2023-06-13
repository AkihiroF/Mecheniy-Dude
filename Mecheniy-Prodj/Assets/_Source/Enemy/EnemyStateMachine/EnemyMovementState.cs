using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace _Source.Enemy.EnemyStateMachine
{
    public class EnemyMovementState : IEnemyState
    {
        private readonly NavMeshAgent _agent;
        private List<PointInterest> _directory;
        private int _currentIdPoint;
        private readonly float _stoppingDistance;
        private Vector3 _lastPosition;

        public EnemyMovementState(NavMeshAgent agent)
        {
            _agent = agent;
            _stoppingDistance = _agent.stoppingDistance;
        }
        public void Enter()
        {
            StartMoving();
        }

        public void Enter(Vector3 position)
        {
            throw new System.NotImplementedException();
        }

        private void StartMoving()
        {
            _directory = CreatorDirectoryEnemy.CreateNewDirectory(_agent.transform.position);
            _currentIdPoint = 0;
            _agent.SetDestination(_directory[_currentIdPoint].Position);
        }

        public void Execute()
        {
            var position = _agent.transform.position;
            if (OnDestinationReached())
            {
                MoveToNextPoint();
                _lastPosition = position;
            }
            var direction = position - _lastPosition;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf. Rad2Deg;
            _agent.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            _lastPosition = position;
            
        }

        public void Execute(Vector3 position)
        {
            
        }

        public bool GetActive()
        {
            throw new System.NotImplementedException();
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

        public void Exit()
        {
            _agent.SetDestination(_agent.transform.position);
        }
    }
}