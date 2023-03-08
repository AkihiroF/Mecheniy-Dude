using System;
using System.Collections;
using _Source.HealthSystem;
using UnityEngine;

namespace _Source.FireSystem
{
    public class EnemyFire : MonoBehaviour
    {
        [SerializeField] private float speedAttack;
        [SerializeField] private float damage;
        [SerializeField] private LayerMask layerAttack;
        private ABaseHealth _target;

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Enter");
            var obj = other.gameObject;
            if ((layerAttack.value & (1 << obj.layer)) > 0)
                StartAttack(obj.GetComponent<ABaseHealth>());
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            var obj = other.gameObject;
            if ((layerAttack.value & (1 << obj.layer)) > 0)
                StopAttack();
            
        }

        private void StartAttack(ABaseHealth target)
        {
            _target = target;
            Attack();
        }

        private void StopAttack()
        {
            _target = null;
        }

        private void Attack()
        {
            if (_target)
            {
                _target.GetDamage(damage);
                StartCoroutine(Wait());
            }
        }

        private IEnumerator Wait()
        {
            yield return new WaitForSeconds(speedAttack);
            Attack();
        }
        
    }
}