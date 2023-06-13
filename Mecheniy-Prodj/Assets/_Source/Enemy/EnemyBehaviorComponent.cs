using System;
using System.Collections.Generic;
using _Source.Enemy.EnemyStateMachine;
using _Source.FireSystem.SOs;
using UnityEngine;
using UnityEngine.AI;

namespace _Source.Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyBehaviorComponent : MonoBehaviour
    {
        [SerializeField] private EnemyFieldOfView fieldOfView;
        [SerializeField] private ParametersMovingEnemy parametersMovingEnemy;
        [SerializeField] private ParametersFightEnemy parametersFightEnemy;
        [SerializeField] private ParametersStalkingEnemy parametersStalkingEnemy;
        private Dictionary<Type, IEnemyState> _states;
        private IEnemyState _currentState;
        private Vector3 _positionPlayer;

        private void Start()
        {
            _states = new Dictionary<Type, IEnemyState>();
            var agent = GetComponent<NavMeshAgent>();
            _states.CreateStateMoving(agent,parametersMovingEnemy);
            _states.CreateStateFighting(parametersFightEnemy);
            _states.CreateStateStalking(parametersStalkingEnemy);
            SwitchState(typeof(EnemyMovementState));
        }
        

        private void FixedUpdate()
        {
            if (fieldOfView.CheckPlayerInField(ref _positionPlayer)) //Have a player in Field
            {
                if (_currentState.GetType() == typeof(EnemyFightState)) // check current state
                {
                    _currentState.Execute(_positionPlayer);
                    return;
                }
                else
                {
                    SwitchState(typeof(EnemyFightState)); // Switch state to fight
                    return;
                }
            }
            else
            {
                if (_currentState.GetType() == typeof(EnemyFightState))
                {
                    SwitchState(typeof(EnemyStalkingState), true);
                    return;
                }

                if (_currentState.GetType() == typeof(EnemyStalkingState))
                {
                    if (_currentState.GetActive())
                    {
                        SwitchState(typeof(EnemyMovementState));
                    }
                }
            }
            if (_currentState != null)
                _currentState.Execute();
        }

        private void SwitchState(Type state, bool isVector = false)
        {
            if (_states.TryGetValue(state, out var state1))
            {
                if (_currentState != null)
                    _currentState.Exit();
                _currentState = state1;
                if (isVector)
                    _currentState.Enter(_positionPlayer);
                else
                    _currentState.Enter();
            }
        }
    }

    [Serializable]
    public struct ParametersMovingEnemy
    {
        public float speed;
        public float stoppingDistance;
    }

    [Serializable]
    public struct ParametersFightEnemy
    { 
        public PlayerGunSo gunSo;
        public Transform pointPositionGun;
        public Transform enemyTransform;
        public float distanceRetreat;
        public NavMeshAgent agent;
    }

    [Serializable]
    public struct ParametersStalkingEnemy
    {
        public NavMeshAgent agent;
        public float angleVision;
        public float timeReview;
    }
}