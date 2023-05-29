using System.Linq;
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
        [Space]
        [SerializeField] private MeshFilter aroundMeshFilter;
        [SerializeField] private MeshFilter fieldMeshFilter;
        [SerializeField] private float radiusAroundView;
        [SerializeField] private int countIterationAround;
        
        private Vector3 _origin;
        private Mesh _meshFieldOfView;
        private Mesh _meshAroundCircle;
        private Mesh _exitMesh;
        private CombineInstance[] _combine = new CombineInstance[2];
        private MeshFilter _meshFilter;

        private FieldOfView _calculator;


        private void Start()
        {
            _meshFieldOfView = new Mesh();
            _meshAroundCircle = new Mesh();
            _exitMesh = new Mesh();
            _meshFilter = GetComponent<MeshFilter>();
            _meshFilter.mesh = _meshAroundCircle;
            aroundMeshFilter.mesh = _meshAroundCircle;
            fieldMeshFilter.mesh = _meshFieldOfView;
            _origin = Vector3.zero;
            Renderer myRenderer = GetComponent<Renderer>();
            myRenderer.sortingLayerName = "FieldOfView";
            myRenderer.sortingOrder = 10;
            
            _combine[0].mesh = _meshFieldOfView;
            _combine[1].mesh = _meshAroundCircle;
            
            _calculator = new FieldOfView(
                layersView,
                angleView,
                radiusView,
                countIteration,
                transform,
                radiusAroundView,
                countIterationAround);
        }

        private void Update() 
        { 
            _calculator.SetOrigin(transform.position); 
            _calculator.SetAimDirection(transform.up);
        } 

        private void LateUpdate()
        {
            //_calculator.CreateFieldOfView(ref _meshFieldOfView);
            _calculator.CreateCircleMesh(ref _meshAroundCircle);
             // //_combine[0].mesh = _meshFieldOfView;
             // _combine[0].transform = aroundMeshFilter.transform.localToWorldMatrix;
             //
             // //_combine[1].mesh = _meshAroundCircle;
             // _combine[1].transform = fieldMeshFilter.transform.localToWorldMatrix;
             //
             // _exitMesh.CombineMeshes(_combine);
             // _exitMesh.bounds = new Bounds(Vector3.down, Vector3.one); 

             // _exitMesh.vertices = _meshFieldOfView.vertices;
             // _exitMesh.uv = _meshFieldOfView.uv;
             // _exitMesh.triangles = _meshFieldOfView.triangles;
             //
             // int vertexOffset = _exitMesh.vertices.Length;
             // Vector3[] vertices = _meshAroundCircle.vertices;
             // for (int j = 0; j < vertices.Length; j++)
             // {
             //     vertices[j] += new Vector3(0, vertexOffset, 0);
             // }
             //
             // _exitMesh.vertices = _exitMesh.vertices.Concat(vertices).ToArray();
             // _exitMesh.triangles = _exitMesh.triangles.Concat(_meshAroundCircle.triangles.Select(t => t + vertexOffset)).ToArray();
             // _exitMesh.uv = _exitMesh.uv.Concat(_meshAroundCircle.uv).ToArray();

             //_exitMesh.Optimize();
            
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