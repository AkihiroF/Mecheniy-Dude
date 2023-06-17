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
                
            }
            else
            {
                CurrentHp -= damage;
            }
        }

        public override void ReturnHealth(float health)
        {
        }
    }
}