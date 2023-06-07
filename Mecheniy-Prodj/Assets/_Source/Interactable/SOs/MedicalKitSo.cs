using UnityEngine;

namespace _Source.Interactable.SOs
{
    [CreateAssetMenu(menuName = "HealthSystem/Medical Kit", fileName = "Medical Kit")] 
    public class MedicalKitSo : ScriptableObject
    {
        [SerializeField] private float addingHp;

        public float Hp
        {
            get
            {
                return addingHp;
            }
        }
    }
}