using _Source.HealthSystem;
using UnityEngine;

namespace _Source.FireSystem.Bullets
{
    public class PistolBullet : ABulletController
    {
        public override void FireBullet()
        {
            this.gameObject.SetActive(true);
            Rb.AddForce(transform.up * SpeedMoving);
        }

        protected override void OnTriggerEnter2D(Collider2D col)
        {
            var obj = col.gameObject;
            
            if ((contactLayer.value & (1 << obj.layer)) > 0)
                obj.GetComponent<ABaseHealth>().GetDamage(Damage);
            this.gameObject.SetActive(false);
            Controller.ReturnBulletInPool(this);
        }
    }
}