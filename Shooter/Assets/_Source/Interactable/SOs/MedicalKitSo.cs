using UnityEngine;

namespace _Source.Interactable.SOs
{
    [CreateAssetMenu(menuName = "HealthSystem/MedicalKit", fileName = "MedicalKit")] 
    public class MedicalKitSo : ScriptableObject
    {
        [SerializeField] private float addingHp;

        public float HP
        {
            get
            {
                return addingHp;
            }
        }
    }
}