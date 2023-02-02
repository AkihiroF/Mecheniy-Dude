using UnityEngine;

namespace _Source.HealthSystem
{
    public class PlayerHealth : ABaseHealth
    {
        public override void GetDamage(float damage)
        {
            if (CurrentHp - damage <= 0)
            {
                Debug.Log("Player Dead");
            }
            else
            {
                CurrentHp -= damage;
            }
        }

        public override void ReturnHealth(float health)
        {
            if (CurrentHp + health > maxHp)
                CurrentHp = maxHp;
            else
                CurrentHp += health;
        }
    }
}