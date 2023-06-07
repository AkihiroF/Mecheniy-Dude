using System;

namespace _Source.FireSystem
{
    public interface IPoolBullets
    {
        public event Action OnDeleteBullets;
        public void ReturnBulletInPool(ABulletController aBullet);
    }
}