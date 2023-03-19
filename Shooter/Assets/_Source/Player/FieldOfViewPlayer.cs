using System;
using CodeMonkey.Utils;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace _Source.Player
{
    public class FieldOfViewPlayer : MonoBehaviour
    {
        [SerializeField] private LayerMask layersView;
        [SerializeField] private float angleView;
        [SerializeField] private float radiusView;
        [SerializeField] private int countIteration;
        private Vector3 _origin;
        private Mesh _mesh;
        private float _angleIncrease;
        private float _startingAngle; 

        private void Start()
        {
            _mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = _mesh;
            _origin = Vector3.zero;
            _angleIncrease = angleView / countIteration;
            
            Renderer myRenderer = GetComponent<Renderer>();
            myRenderer.sortingLayerName = "FieldOfView";
            myRenderer.sortingOrder = 10;
        }

        private void Update() 
        { 
            SetOrigin(transform.position); 
            SetAimDirection(transform.up); 
        } 

        private void LateUpdate()
        {
            float angle = _startingAngle; 
            float angleIncrease = angleView / countIteration; 
            Vector3[] vertices = new Vector3[countIteration + 1 + 1]; 
            Vector2[] uv = new Vector2[vertices.Length]; 
            int[] triangles = new int[countIteration * 3]; 

            vertices[0] = transform.InverseTransformPoint(_origin); 

            int vertexIndex = 1; 
            int triangleIndex = 0; 
            for (int i = 0; i <= countIteration; i++) 
            { 
                Vector3 vertex; 
                RaycastHit2D raycastHit2D = Physics2D.Raycast(_origin, UtilsClass.GetVectorFromAngle(angle), radiusView, layersView); 
                if (raycastHit2D.collider == null) 
                { 
// No hit 
                    vertex = transform.InverseTransformPoint(_origin + UtilsClass.GetVectorFromAngle(angle) * radiusView); 
                } 
                else 
                { 
// Hit object 
                    vertex = transform.InverseTransformPoint(raycastHit2D.point); 
                } 
                vertices[vertexIndex] = vertex; 

                if (i > 0) 
                { 
                    triangles[triangleIndex + 0] = 0; 
                    triangles[triangleIndex + 1] = vertexIndex - 1; 
                    triangles[triangleIndex + 2] = vertexIndex; 

                    triangleIndex += 3; 
                } 

                vertexIndex++; 
                angle -= angleIncrease; 
            } 


            _mesh.vertices = vertices; 
            _mesh.uv = uv; 
            _mesh.triangles = triangles; 
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
        
        private void SetOrigin(Vector3 origin) 
        { 
            _origin = origin; 
        } 

        private void SetAimDirection(Vector3 aimDirection) 
        { 
            _startingAngle = UtilsClass.GetAngleFromVectorFloat(aimDirection) + angleView / 2f; 
        }
    }
}