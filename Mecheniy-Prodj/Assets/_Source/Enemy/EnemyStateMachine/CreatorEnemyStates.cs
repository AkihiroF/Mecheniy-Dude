using System;
using System.Collections.Generic;
using UnityEngine.AI;

namespace _Source.Enemy.EnemyStateMachine
{
    public static class CreatorEnemyStates
    {
        public static void CreateStateMoving(this Dictionary<Type, IEnemyState> states, NavMeshAgent agent,ParametersMovingEnemy parameters)
        {
            if (states.ContainsKey(typeof(EnemyMovementState)) == false)
            {
                agent.speed = parameters.speed;
                agent.stoppingDistance = parameters.stoppingDistance;
                agent.updateUpAxis = false;
                agent.updateRotation = false;
                states.Add(typeof(EnemyMovementState), new EnemyMovementState(agent));
            }
        }

        public static void CreateStateFighting(this Dictionary<Type, IEnemyState> states,
            ParametersFightEnemy parameters)
        {
            if (states.ContainsKey(typeof(EnemyFightState)) == false)
            {
                states.Add(typeof(EnemyFightState), new EnemyFightState(parameters));
            }
        }

        public static void CreateStateStalking(this Dictionary<Type, IEnemyState> states,
            ParametersStalkingEnemy parametersStalkingEnemy)
        {
            if (states.ContainsKey(typeof(EnemyStalkingState)) == false)
            {
                states.Add(typeof(EnemyStalkingState), new EnemyStalkingState(parametersStalkingEnemy));
            }
        }
    }
}