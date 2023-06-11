using _Source.Services;
using _Source.SignalsEvents.UpgradesEvents;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace _Source.Lighting
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class FieldOfViewComponent : MonoBehaviour
    {
        [SerializeField] private LayerMask layersView;
        [SerializeField] protected float angleView;
        [SerializeField] private float radiusView;
        [Space]
        [SerializeField] private float radiusAroundView;
        [SerializeField] private int countIteration;
        [SerializeField] private int countIterationAround;
        
        private Mesh _exitMesh;
        protected ParametersField ParametersField;

        protected virtual void Awake()
        {
            _exitMesh = new Mesh();
            ParametersField = new ParametersField(layersView, angleView, radiusView, radiusAroundView,
                countIterationAround, countIteration, _exitMesh, this.transform);
        }

        protected virtual void Start()
        {
            GetComponent<MeshFilter>().mesh = _exitMesh;
            Renderer myRenderer = GetComponent<Renderer>();
            myRenderer.sortingLayerName = "FieldOfView";
            myRenderer.sortingOrder = 10;
        }

        protected virtual void LateUpdate()
        {
            var startingAngle = UtilsClass.GetAngleFromVectorFloat(transform.up) + angleView / 2f;
            LightMathf.UpdateFieldMesh(ParametersField, startingAngle);
            _exitMesh.Optimize();
        }
        
#if (UNITY_EDITOR)
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            var position = transform.position;
            UnityEditor.Handles.DrawWireDisc(position, Vector3.forward, radiusView);
            Gizmos.color = Color.red;
            UnityEditor.Handles.DrawWireDisc(position,Vector3.forward, radiusAroundView);
            var eulerAngles = transform.eulerAngles;
            Vector3 angle01 = DirectionFromAngle( -eulerAngles.z,-angleView / 2);
            Vector3 angle02 = DirectionFromAngle(-eulerAngles.z,angleView / 2);
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(position, position + angle01 * radiusView);
            Gizmos.DrawLine(position, position + angle02 * radiusView);
        }
        #endif

        private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
        {
            angleInDegrees += eulerY;
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    }
}