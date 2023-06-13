using _Source.FireSystem;
using _Source.FireSystem.SOs;
using _Source.Player;
using UnityEngine;
using UnityEngine.AI;

namespace _Source.Enemy.EnemyStateMachine
{
    public class EnemyFightState:IEnemyState
    {
        private readonly GameObject _gunObj;
        private readonly ABaseGunComponent _currentGun;
        private readonly Transform _body;
        private readonly NavMeshAgent _agent;
        private readonly ClipSo _currentClip;
        private readonly float _distanceRetreat;
        public EnemyFightState(ParametersFightEnemy parameters)
        {
            var info = parameters.gunSo;
            _gunObj = Object.Instantiate(parameters.gunSo.GunObjectObject, parameters.pointPositionGun);
            _currentGun = _gunObj.GetComponent<ABaseGunComponent>();
            _currentGun.SetParameters(info.ClipInfo,info.ClipInfo.CountBullet, 0,true);
            _currentClip = info.ClipInfo;
            _body = parameters.enemyTransform;
            _gunObj.SetActive(false);
            _distanceRetreat = parameters.distanceRetreat;
            _agent = parameters.agent;
        }
        public void Enter()
        {
            _gunObj.SetActive(true);
            var countAmmo = InventoryPlayer.GetCountItem(_currentClip);
            if (countAmmo < 100)
            {
                countAmmo = 100 - countAmmo;
                InventoryPlayer.AddItem(_currentClip,countAmmo);
            }

            _currentGun.enabled = true;
        }

        public void Enter(Vector3 position)
        {
            throw new System.NotImplementedException();
        }

        public void Execute()
        {
            
        }

        public void Execute(Vector3 position)
        {
            var positionEnemy = _body.position;
            var direction =position -positionEnemy;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf. Rad2Deg;
            _body.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            
            _currentGun.Fire();

            var distance = Vector3.Distance(positionEnemy, position);
            if (distance <= _distanceRetreat)
            {
                Retreat(position);
            }
        }

        public bool GetActive()
        {
            throw new System.NotImplementedException();
        }

        private void Retreat(Vector3 playerPosition)
        {
            var position = _body.position;
            Vector3 targetPosition = position + (position - playerPosition).normalized * _distanceRetreat/2;
            _agent.SetDestination(targetPosition);
        }

        public void Exit()
        {
            //_gunObj.SetActive(false);
        }
    }
}