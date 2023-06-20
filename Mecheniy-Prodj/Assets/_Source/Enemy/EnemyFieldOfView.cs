using _Source.Lighting;
using UnityEngine;

namespace _Source.Enemy
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class EnemyFieldOfView : FieldOfViewComponent
    {
        [SerializeField] private LayerMask layerContactDetected;
        [SerializeField] private LayerMask layerPlayer;
        private Vector3 _lastPlayerPosition;

        public bool CheckPlayerInField(ref Vector3 position)
        {
            if (_lastPlayerPosition != Vector3.zero)
                position = _lastPlayerPosition;
            return LightMathf.SearchPlayerInField(ParametersField, StartingAngle(), layerContactDetected, layerPlayer,
                out _lastPlayerPosition);
        }
    }
}