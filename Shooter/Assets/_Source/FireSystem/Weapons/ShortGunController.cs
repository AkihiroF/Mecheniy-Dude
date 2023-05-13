using _Source.FireSystem.Player;

namespace _Source.FireSystem.Weapons
{
    public class ShortGunController : ABaseGunController
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
                .GetComponent<ABulletController>();
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
                StartCoroutine(WaitFire());
            }
            else
            {
                StartReloadWeapon();
            }
        }
    }
}