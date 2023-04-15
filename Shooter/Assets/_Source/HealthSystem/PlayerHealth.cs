using System;
using System.Collections.Generic;
using _Source.Interactable.SOs;
using _Source.Player;
using DG.Tweening;
using UnityEngine;

namespace _Source.HealthSystem
{
    public class PlayerHealth : ABaseHealth
    {
        public static event Action<int> OnHealing;
        public static event Action OnDead;
        [SerializeField] private SpriteRenderer body;
        [Tooltip("Gradation from minimum to maximum HP")][SerializeField] private List<Color> gradationsColors;
        [SerializeField] private MedicalKitSo medicalKit;

        public float GetHp
        {
            get
            {
                return CurrentHp;
            }
        }
        protected  override void Start()
        {
            base.Start();
            CheckHp();
            UpdateStateUI();
        }

        public void SetSavedHeath(float hp)
        {
            CurrentHp = hp;
            CheckHp();
            UpdateStateUI();
        }

        public override void GetDamage(float damage)
        {
            if (CurrentHp - damage <= 0)
            {
                if (OnDead != null) OnDead.Invoke();
            }
            else
            {
                CurrentHp -= damage;
            }
            CheckHp();
        }

        private void CheckHp()
        {
            var porog = maxHp / gradationsColors.Count;
            var color = (int)Math.Round(CurrentHp / porog);
            body.DOColor(gradationsColors[color - 1], 1);
        }

        public override void ReturnHealth(float health)
        {
            if (CurrentHp + health > maxHp)
                CurrentHp = maxHp;
            else
                CurrentHp += health;
            CheckHp();
        }

        public void UseKit()
        {
            var kit = InventoryPlayer.UseItem(medicalKit);
            if (kit == 1)
            {
                ReturnHealth(medicalKit.Hp);
            }
            UpdateStateUI();
        }

        public void UpdateStateUI()
        {
            if (OnHealing != null) OnHealing.Invoke(InventoryPlayer.GetCountItem(medicalKit));
        }
    }
}