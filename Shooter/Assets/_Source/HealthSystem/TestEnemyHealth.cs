using _Source.Services;
using _Source.SignalsEvents.UpgradesEvents;
using UnityEngine;

namespace _Source.HealthSystem
{
    public class TestEnemyHealth : ABaseHealth
    {
        [SerializeField] private int countScore;
        public override void GetDamage(float damage)
        {
            if (CurrentHp - damage <= 0)
            {
                Dead();
            }
            else
            {
                CurrentHp -= damage;
            }
        }

        private void Dead()
        {
            Signals.Get<OnAddScoreUpgrade>().Dispatch(countScore);
            Destroy(this.gameObject);
        }

        public override void ReturnHealth(float health)
        {
        }
    }
}