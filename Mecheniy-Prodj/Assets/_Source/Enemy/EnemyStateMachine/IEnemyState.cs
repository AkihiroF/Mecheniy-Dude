using UnityEngine;

namespace _Source.Enemy.EnemyStateMachine
{
    public interface IEnemyState
    {
        public void Enter();
        public void Enter(Vector3 position);
        public void Execute();
        public void Execute(Vector3 position);

        public bool GetActive();
        public void Exit();
    }
}