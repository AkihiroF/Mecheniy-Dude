
namespace _Source.FireSystem.Weapons
{
    public class ShortGunComponent : ABaseGunComponent
    {
        private int currentIndex;
        protected override void InitialiseBullet()
        {
            if (BulletPool.Count > 0)
            {
                var bullet = BulletPool[currentIndex];
                SetPositionBullet(bullet.transform);
                if (bullet.FireBullet() == false)
                {
                    if (currentIndex < BulletPool.Count-1)
                    {
                        currentIndex++;
                        InitialiseBullet();
                    }
                    else
                    {
                        CreateNewBullet();
                    }
                }
                SetPositionBullet(bullet.transform);
                bullet.FireBullet();
                currentIndex = 0;
                BulletPool.Remove(bullet);
            }
            else
            {
                CreateNewBullet();
            }
        }

        private void CreateNewBullet()
        {
            var newBullet = Instantiate(BulletObject)
                .GetComponent<ABulletComponent>();
            newBullet.SetParameters(this, SpeedBullet, Damage);
            SetPositionBullet(newBullet.transform);
            newBullet.FireBullet();
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

        protected override bool IsBulletReloading() => true;
    }
}