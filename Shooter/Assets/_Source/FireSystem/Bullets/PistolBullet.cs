using _Source.HealthSystem;
using UnityEngine;

namespace _Source.FireSystem.Bullets
{
    public class PistolBullet : ABulletController
    {
        public override bool FireBullet(float angle = 0)
        {
            this.gameObject.SetActive(true);
            var up = transform.up;
            var direction = new Vector2(up.x + angle, up.y);
            Rb.AddForce(direction* SpeedMoving);
            return true;
        }

        protected override void OnTriggerEnter2D(Collider2D col)
        {
            var obj = col.gameObject;
            
            if ((contactLayer.value & (1 << obj.layer)) > 0)
                obj.GetComponent<ABaseHealth>().GetDamage(Damage);
            this.gameObject.SetActive(false);
            PoolBullets.ReturnBulletInPool(this);
        }
    }
}