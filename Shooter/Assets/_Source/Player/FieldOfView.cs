using _Source.Services;
using UnityEngine;

namespace _Source.Player
{
    public class FieldOfView
    {
        public FieldOfView(LayerMask layersView, float angleView, float radiusView, int countIteration, Transform bodyPlayer)
        {
            this.layersView = layersView;
            this.angleView = angleView;
            this.radiusView = radiusView;
            this.countIteration = countIteration;

            _body = bodyPlayer;

            _countVertices = countIteration + 1 + 1;
        }

        private LayerMask layersView;
        private float angleView;
        private float radiusView;
        private int countIteration;
        private Vector3 _origin;
        private Mesh _mesh;
        private float _startingAngle;

        private Transform _body;

        private int _countVertices;


        public void SetOrigin(Vector3 origin) 
        { 
            _origin = origin; 
        } 

        public void SetAimDirection(Vector3 aimDirection) 
        { 
            _startingAngle = UtilsClass.GetAngleFromVectorFloat(aimDirection) + angleView / 2f; 
        }

        public Vector3[] GetVertices()
        {
            float angle = _startingAngle; 
            float angleIncrease = angleView / countIteration; 
            var vertices = new Vector3[_countVertices];
            vertices[0] = _body.InverseTransformPoint(_origin);
            int vertexIndex = 1; 
            for (int i = 0; i <= countIteration; i++) 
            { 
                Vector3 vertex; 
                RaycastHit2D raycastHit2D = Physics2D.Raycast(_origin, UtilsClass.GetVectorFromAngle(angle), radiusView, layersView); 
                if (raycastHit2D.collider == null) 
                { 
// No hit 
                    vertex = _body.InverseTransformPoint(_origin + UtilsClass.GetVectorFromAngle(angle) * radiusView); 
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

        public int[] GetTriangles()
        {
            var triangles = new int[countIteration * 3];
            int vertexIndex = 1; 
            int triangleIndex = 0; 
            for (int i = 0; i <= countIteration; i++)
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

        public Vector2[] GetUV() => new Vector2[_countVertices];
    }
}