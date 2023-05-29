using _Source.Services;
using UnityEngine;

namespace _Source.Player
{
    public class FieldOfView
    {
        public FieldOfView(
            LayerMask layersView,
            float angleView,
            float radiusView,
            //int countIteration,
            Transform bodyPlayer,
            float radiusAroundView,
            int countIterationAround
            )
        {
            this._layersView = layersView;
            this._angleView = angleView;
            this._radiusView = radiusView;
            //this._countIteration = countIteration;
            this._radiusAroundView = radiusAroundView;
            this._countIterationAround = countIterationAround;

            _body = bodyPlayer;

            //_countVertices = countIteration + 1 + 1;
            _countVerticesAround = countIterationAround + 1 + 1;
        }

        private readonly LayerMask _layersView;
        private float _angleView;
        private float _radiusView;
        private float _radiusAroundView;
        private readonly int _countIterationAround;
        //private readonly int _countIteration;
        private Vector3 _origin;
        private Mesh _mesh;
        private float _startingAngle;

        private readonly Transform _body;

        private readonly int _countVertices;
        private int _countVerticesAround;


        public void SetOrigin(Vector3 origin) 
        { 
            _origin = origin; 
        } 

        public void SetAimDirection(Vector3 aimDirection) 
        { 
            _startingAngle = UtilsClass.GetAngleFromVectorFloat(aimDirection) + _angleView / 2f; 
        }

//         private Vector3[] GetVertices()
//         {
//             float angle = _startingAngle; 
//             float angleIncrease = _angleView / _countIteration; 
//             var vertices = new Vector3[_countVertices];
//             vertices[0] = _body.InverseTransformPoint(_origin);
//             int vertexIndex = 1; 
//             for (int i = 0; i <= _countIteration; i++) 
//             { 
//                 Vector3 vertex; 
//                 RaycastHit2D raycastHit2D = Physics2D.Raycast(_origin, UtilsClass.GetVectorFromAngle(angle), _radiusView, _layersView); 
//                 if (raycastHit2D.collider == null) 
//                 { 
// // No hit 
//                     vertex = _body.InverseTransformPoint(_origin + UtilsClass.GetVectorFromAngle(angle) * _radiusView); 
//                 } 
//                 else 
//                 { 
// // Hit object 
//                     vertex = _body.InverseTransformPoint(raycastHit2D.point); 
//                 } 
//                 vertices[vertexIndex] = vertex;
//
//                 vertexIndex++; 
//                 angle -= angleIncrease; 
//             }
//
//             return vertices;
//         }
//
//         private int[] GetTriangles()
//         {
//             var triangles = new int[_countIteration * 3];
//             int vertexIndex = 1; 
//             int triangleIndex = 0; 
//             for (int i = 0; i <= _countIteration; i++)
//             {
//                 if (i > 0) 
//                 { 
//                     triangles[triangleIndex + 0] = 0; 
//                     triangles[triangleIndex + 1] = vertexIndex - 1; 
//                     triangles[triangleIndex + 2] = vertexIndex; 
//
//                     triangleIndex += 3; 
//                 } 
//
//                 vertexIndex++; 
//             }
//             return triangles;
//         }
        private int[] GetAroundTriangles()
        {
            var triangles = new int[_countIterationAround * 3];
            int vertexIndex = 1; 
            int triangleIndex = 0; 
            for (int i = 0; i <= _countIterationAround; i++)
            {
                if (i > 0) 
                { 
                    triangles[triangleIndex + 0] = 0; 
                    triangles[triangleIndex + 1] = vertexIndex - 1; 
                    triangles[triangleIndex + 2] = vertexIndex; 

                    triangleIndex += 3; 
                } 

                vertexIndex++; 
            }
            return triangles;
        }

        private Vector3[] GetAroundVertices()
        {
            float angle = _startingAngle; 
            float angleIncrease = 360 / _countIterationAround; 
            var vertices = new Vector3[_countVerticesAround];
            RaycastHit2D firstRayCast = Physics2D.Raycast(_origin, UtilsClass.GetVectorFromAngle(angle), _radiusAroundView, _layersView); 
            if (firstRayCast.collider == null) 
            { 
// No hit 
                vertices[0] = _body.InverseTransformPoint(_origin + UtilsClass.GetVectorFromAngle(angle) * _radiusAroundView); 
            } 
            else 
            { 
// Hit object 
                vertices[0] = _body.InverseTransformPoint(firstRayCast.point); 
            } 
            int vertexIndex = 1; 
            for (int i = 0; i <= _countIterationAround; i++) 
            { 
                Vector3 vertex;
                var currentDistance = Mathf.Abs(_startingAngle) - angle < _angleView ? _radiusView : _radiusAroundView;
                RaycastHit2D raycastHit2D = Physics2D.Raycast(_origin, UtilsClass.GetVectorFromAngle(angle), currentDistance, _layersView); 
                if (raycastHit2D.collider == null) 
                { 
// No hit 
                    vertex = _body.InverseTransformPoint(_origin + UtilsClass.GetVectorFromAngle(angle) * currentDistance); 
                } 
                else 
                { 
// Hit object 
                    vertex = _body.InverseTransformPoint(raycastHit2D.point); 
                } 
                vertices[vertexIndex] = vertex;

                vertexIndex++; 
                angle -= angleIncrease; 
            }

            return vertices;
        }

        public void CreateCircleMesh(ref Mesh mesh)
        {
            mesh.vertices = GetAroundVertices();
            mesh.uv = GetAroundUV();
            mesh.triangles = GetAroundTriangles();
            mesh.bounds = new Bounds(_origin, Vector3.one * 1000f); 
        }

        // public void CreateFieldOfView(ref Mesh mesh)
        // {
        //     mesh.vertices = GetVertices(); 
        //     mesh.uv = GetUV(); 
        //     mesh.triangles = GetTriangles(); 
        //     mesh.bounds = new Bounds(_origin, Vector3.one * 1000f); 
        // }

        //private Vector2[] GetUV() => new Vector2[_countVertices];
        private Vector2[] GetAroundUV() => new Vector2[_countVerticesAround];
    }
}