using System.Collections.Generic;
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

        public Mesh CreateCircleMesh(float radius)
        {
            var currentCountIteration = countIteration / 2;
            Mesh circleMesh = new Mesh();
            List<Vector3> verticiesList = new List<Vector3> { };
            float x;
            float y;
            for (int i = 0; i < currentCountIteration; i ++)
            {
                x = radius * Mathf.Sin((2 * Mathf.PI * i) / currentCountIteration);
                y = radius * Mathf.Cos((2 * Mathf.PI * i) / currentCountIteration);
                verticiesList.Add(new Vector3(x, y, 0f));
            }
            Vector3[] verticies = verticiesList.ToArray();

            //triangles
            List<int> trianglesList = new List<int> { };
            for(int i = 0; i < (currentCountIteration-2); i++)
            {
                trianglesList.Add(0);
                trianglesList.Add(i+1);
                trianglesList.Add(i+2);
            }
            int[] triangles = trianglesList.ToArray();

            //normals
            List<Vector3> normalsList = new List<Vector3> { };
            for (int i = 0; i < verticies.Length; i++)
            {
                normalsList.Add(-Vector3.forward);
            }
            Vector3[] normals = normalsList.ToArray();
            Vector2[] uvs = new Vector2[verticiesList.Count];
            for (int i = 0; i < uvs.Length; i++)
            {
                uvs[i] = new Vector2(verticiesList[i].x / (radius*2) + 0.5f, verticiesList[i].y / (radius*2) + 0.5f);
            }

// Later...
            circleMesh.uv = new Vector2[verticiesList.Count];

            //initialise
            circleMesh.vertices = verticies;
            circleMesh.triangles = triangles;
            circleMesh.normals = normals;
            return circleMesh;
        }

        public Vector2[] GetUV() => new Vector2[_countVertices];
    }
}