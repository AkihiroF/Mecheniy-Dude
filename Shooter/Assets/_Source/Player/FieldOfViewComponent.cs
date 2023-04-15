using _Source.Services;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace _Source.Player
{
    public class FieldOfViewComponent : MonoBehaviour
    {
        [SerializeField] private LayerMask layersView;
        [SerializeField] private float angleView;
        [SerializeField] private float radiusView;
        [SerializeField] private int countIteration;
        private Vector3 _origin;
        private Mesh _mesh;

        private FieldOfView _calculator;

        private void Start()
        {
            _mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = _mesh;
            _origin = Vector3.zero;
            Renderer myRenderer = GetComponent<Renderer>();
            myRenderer.sortingLayerName = "FieldOfView";
            myRenderer.sortingOrder = 10;

            _calculator = new FieldOfView(layersView, angleView, radiusView, countIteration, transform);
        }

        private void Update() 
        { 
            _calculator.SetOrigin(transform.position); 
            _calculator.SetAimDirection(transform.up); 
        } 

        private void LateUpdate()
        {
            _mesh.vertices = _calculator.GetVertices(); 
            _mesh.uv = _calculator.GetUV(); 
            _mesh.triangles = _calculator.GetTriangles(); 
            _mesh.bounds = new Bounds(_origin, Vector3.one * 1000f); 
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            var position = transform.position;
            UnityEditor.Handles.DrawWireDisc(position, Vector3.forward, radiusView);
            var eulerAngles = transform.eulerAngles;
            Vector3 angle01 = DirectionFromAngle( -eulerAngles.z,-angleView / 2);
            Vector3 angle02 = DirectionFromAngle(-eulerAngles.z,angleView / 2);
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(position, position + angle01 * radiusView);
            Gizmos.DrawLine(position, position + angle02 * radiusView);
        }

        private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
        {
            angleInDegrees += eulerY;
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    }
}