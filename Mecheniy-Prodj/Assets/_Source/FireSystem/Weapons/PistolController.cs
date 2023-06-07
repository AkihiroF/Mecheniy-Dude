namespace _Source.FireSystem.Weapons
{
    public class PistolController : ABaseGunController
    {
        protected override void InitialiseBullet()
        {
            if (BulletPool.Count > 0)
            {
                var bullet = BulletPool[0];
                SetPositionBullet(bullet.transform);
                bullet.FireBullet();
                BulletPool.RemoveAt(0);
            }
            else
            {
                var newBullet = Instantiate(BulletObject)
                    .GetComponent<ABulletController>();
                newBullet.SetParameters(this, SpeedBullet, Damage);
                SetPositionBullet(newBullet.transform);
                newBullet.FireBullet();
            }
        }

        protected override void DoFire()
        {
            if (CurrentCountAmmoInGun > 0)
            {
                InitialiseBullet();
                UpdateCountAmmo();
                StartCoroutine(WaitFire());
            }
            else
            {
                StartReloadWeapon();
            }
        }
    }
}