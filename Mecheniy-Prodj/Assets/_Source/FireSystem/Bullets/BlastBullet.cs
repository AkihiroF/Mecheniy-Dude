using _Source.HealthSystem;
using _Source.Services;
using UnityEngine;

namespace _Source.FireSystem.Bullets
{
    public class BlastBullet : ABulletComponent
    {
        public override bool FireBullet(float angle = 0)
        {
            this.gameObject.SetActive(true);
            var direction = UtilsClass.DirectionFromAngle(-transform.eulerAngles.z, angle);
            Rb.AddForce(direction* SpeedMoving);
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