namespace _Source.HealthSystem
{
    public class TestEnemyHealth : ABaseHealth
    {
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
            Destroy(this.gameObject);
        }

        public override void ReturnHealth(float health)
        {
        }
    }
}