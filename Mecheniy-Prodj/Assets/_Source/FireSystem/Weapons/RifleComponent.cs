namespace _Source.FireSystem.Weapons
{
    public class RifleComponent : ABaseGunComponent
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
                    .GetComponent<ABulletComponent>();
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
                WaitFire();
            }
            else
            {
                StartReloadWeapon();
            }
        }
    }
}