using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

namespace _Source.Enemy.EnemyStateMachine
{
    public class EnemyStalkingState : IEnemyState
    {
        private readonly NavMeshAgent _agent;
        private readonly float _stoppingDistance;
        private Vector3 _lastPosition;
        private float _timeReview;
        private float _angleReview;
        private bool _isReview;

        private bool _isFinish;

        public EnemyStalkingState(ParametersStalkingEnemy parameters)
        {
            _agent = parameters.agent;
            _stoppingDistance = _agent.stoppingDistance;
            _angleReview = parameters.angleVision;
            _timeReview = parameters.timeReview;
        }
        public void Enter()
        {
            throw new System.NotImplementedException();
        }

        public void Enter(Vector3 position)
        {
            _agent.SetDestination(position);
            _isReview = false;
            _isFinish = false;
        }

        public void Execute()
        {
            if(_isReview)
                return;
            if (OnDestinationReached())
            {
                _isReview = true;
                var body = _agent.transform;
                
                var startRotation = body.eulerAngles;
                var angleRotationRight = new Vector3(startRotation.x, startRotation.y, startRotation.z + _angleReview);
                var angleRotationLeft= new Vector3(startRotation.x, startRotation.y, startRotation.z - _angleReview);

                void Callback() => _isFinish = true;
                Sequence sequence = DOTween.Sequence();
                sequence.Append(body.DORotate(angleRotationRight, _timeReview));
                sequence.Append(body.DORotate(angleRotationLeft, _timeReview));
                sequence.Append(body.DORotate(startRotation, _timeReview));
                sequence.AppendCallback(Callback);
                sequence.Play();
                return;
            }
            var position = _agent.transform.position;
            var direction = position - _lastPosition;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf. Rad2Deg;
            _agent.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            _lastPosition = position;
        }

        public void Execute(Vector3 position)
        {
            throw new System.NotImplementedException();
        }

        public bool GetActive()
        {
            return _isFinish;
        }

        private bool OnDestinationReached()
        {
            return _agent.remainingDistance <= _stoppingDistance;
        }

        public void Exit()
        {
            _agent.SetDestination(_agent.transform.position);
            _agent.transform.DOComplete();
        }
    }
}