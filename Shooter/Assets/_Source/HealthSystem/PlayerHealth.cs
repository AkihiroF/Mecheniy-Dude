using DG.Tweening;
using UnityEngine;

namespace _Source.HealthSystem
{
    public class PlayerHealth : ABaseHealth
    {
        [SerializeField] private SpriteRenderer body;
        [SerializeField] private Color colorFullHp;
        [SerializeField] private Color colorMediumHp;
        [SerializeField] private Color colorLowHp;
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
            CheckHp();
        }

        private void CheckHp()
        {
            var porog = maxHp / 3;
            if (CurrentHp > porog * 2)
                body.DOColor(colorFullHp, 1);
            if (CurrentHp <= porog * 2)
                body.DOColor(colorMediumHp, 1);
            if (CurrentHp <= porog)
                body.DOColor(colorLowHp, 1);

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