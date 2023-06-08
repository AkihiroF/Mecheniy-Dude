using _Source.HealthSystem;
using UnityEngine;

namespace _Source.FireSystem.Bullets
{
    public class PistolBullet : ABulletComponent
    {
        public override bool FireBullet(float angle = 0)
        {
            this.gameObject.SetActive(true);
            Rb.AddForce(transform.up* SpeedMoving);
            return true;
        }

        protected override void OnTriggerEnter2D(Collider2D col)
        {
            var obj = col.gameObject;
            
            if ((contactLayer.value & (1 << obj.layer)) > 0)
                obj.GetComponent<ABaseHealth>().GetDamage(Damage);
            base.OnTriggerEnter2D(col);
        }
    }
}