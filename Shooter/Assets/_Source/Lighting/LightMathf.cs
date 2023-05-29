using _Source.Services;
using UnityEngine;

namespace _Source.Lighting
{
    public static class LightMathf
    {
        public static void UpdateAroundMesh(ref Mesh mesh, int countIteration, Vector3 origin,
            Transform body, float distance, LayerMask layersView)
        {
            var countVerticesAround = countIteration + 1 + 1;
            mesh.vertices = GetVertices(countIteration, origin, body, distance, layersView, countVerticesAround);
            mesh.uv = GetAroundUV(countVerticesAround);
            mesh.triangles = GetTriangles(countIteration);
            mesh.bounds = new Bounds(origin, Vector3.one * 1000f);
        }
        private static int[] GetTriangles(int countIterationAround)
        {
            var triangles = new int[countIterationAround * 3];
            int vertexIndex = 1; 
            int triangleIndex = 0; 
            for (int i = 0; i <= countIterationAround; i++)
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
        private static Vector3[] GetVertices(int countIteration, Vector3 origin,
            Transform body, float distance,LayerMask layersView,int countVerticesAround)
        {
            float angle = 0; 
            float angleIncrease = 360 / countIteration; 
            var vertices = new Vector3[countVerticesAround];
            vertices[0] = body.InverseTransformPoint(origin);
            int vertexIndex = 1; 
            for (int i = 0; i <= countIteration; i++) 
            { 
                Vector3 vertex;
                RaycastHit2D raycastHit2D = Physics2D.Raycast(origin,
                    UtilsClass.GetVectorFromAngle(angle), distance, layersView); 
                if (raycastHit2D.collider == null) 
                { 
// No hit 
                    vertex = body.InverseTransformPoint(origin + UtilsClass.GetVectorFromAngle(angle) * distance); 
                } 
                else 
                { 
// Hit object 
                    vertex = body.InverseTransformPoint(raycastHit2D.point); 
                } 
                vertices[vertexIndex] = vertex;

                vertexIndex++; 
                angle -= angleIncrease; 
            }

            return vertices;
        }
        private static Vector2[] GetAroundUV(int countVerticesAround) => new Vector2[countVerticesAround];
    }
}