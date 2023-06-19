using _Source.Services;
using _Source.SignalsEvents.UpgradesEvents;
using DG.Tweening;
using UnityEngine;

namespace _Source.HealthSystem
{
    public class EnemyHealthComponent : ABaseHealth
    {
        [SerializeField] private int countScore;
        [SerializeField] private SpriteRenderer body;
        [SerializeField] private Color colorReaction;
        [SerializeField] private float timeReaction;

        private Color _startColor;

        protected override void Start()
        {
            base.Start();
            _startColor = body.color;
        }
        public override void GetDamage(float damage)
        {
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
            }
        }
        private void Dead()
        {
            body.DOComplete();
            Signals.Get<OnAddScoreUpgrade>().Dispatch(countScore);
            SavedDead();
        }

        public override void ReturnHealth(float health)
        {
            throw new System.NotImplementedException();
        }
    }
}