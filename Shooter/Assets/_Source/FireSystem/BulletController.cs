using System;
using _Source.FireSystem.Player;
using _Source.HealthSystem;
using UnityEngine;

namespace _Source.FireSystem
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private LayerMask contactLayer;
        private float _speedMoving;
        private float _damage;
        private Rigidbody2D _rb;
        private PlayerGunController _controller;

        public void SetParameters(PlayerGunController controller, float speed, float damage)
        {
            _controller = controller;
            _speedMoving = speed;
            _rb = GetComponent<Rigidbody2D>();
            _damage = damage;
        }

        public void FireBullet()
        {
            this.gameObject.SetActive(true);
            _rb.AddForce(transform.up * _speedMoving);
        }
        private void OnTriggerEnter2D(Collider2D col)
        {
            var obj = col.gameObject;
            
            if ((contactLayer.value & (1 << obj.layer)) > 0)
                obj.GetComponent<ABaseHealth>().GetDamage(_damage);
            
            this.gameObject.SetActive(false);
            _controller.ReturnBulletInPool(this);
        }
    }
}