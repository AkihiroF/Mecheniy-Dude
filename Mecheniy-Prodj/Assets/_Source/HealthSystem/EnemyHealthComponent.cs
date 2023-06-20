using _Source.Enemy;
using _Source.Services;
using _Source.SignalsEvents;
using _Source.SignalsEvents.UpgradesEvents;
using DG.Tweening;
using UnityEngine;

namespace _Source.HealthSystem
{
    public class EnemyHealthComponent : ABaseHealth
    {
        [SerializeField] private EnemyBehaviorComponent enemyBehaviorComponent;
        [SerializeField] private int countScore;
        [SerializeField] private SpriteRenderer body;
        [SerializeField] private Color colorReaction;
        [SerializeField] private float timeReaction;

        private Color _startColor;

        protected override void Start()
        {
            base.Start();
            _startColor = body.color;
            Signals.Get<OnDeadEnemy>().Dispatch(false);
        }
        public override void GetDamage(float damage)
        {
            body.DOComplete();
            if (CurrentHp - damage <= 0)
            {
                Dead();
                return;
            }
            else
            {
                CurrentHp -= damage;
                Sequence sequence = DOTween.Sequence();
                sequence.Append(body.DOColor(colorReaction, timeReaction));
                sequence.Append(body.DOColor(_startColor, timeReaction));
                sequence.Play();
                enemyBehaviorComponent.OnGetDamage();
            }
        }
        private void Dead()
        {
            body.DOComplete();
            Signals.Get<OnAddScoreUpgrade>().Dispatch(countScore);
            Signals.Get<OnDeadEnemy>().Dispatch(true);
            SavedDead();
        }

        public override void ReturnHealth(float health)
        {
            throw new System.NotImplementedException();
        }
    }
}