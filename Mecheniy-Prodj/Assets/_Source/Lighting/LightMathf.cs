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
            mesh.vertices = GetVerticesAround(countIteration, origin, body, distance, layersView, countVerticesAround);
            mesh.uv = GetAroundUV(countVerticesAround);
            mesh.triangles = GetTriangles(countIteration);
            mesh.bounds = new Bounds(origin, Vector3.one * 1000f);
        }
        public static void UpdateFieldMesh(ParametersField parametersField, float startingAngle)
        {
            parametersField.Mesh.vertices = GetFieldVertices(parametersField, startingAngle);
            parametersField.Mesh.uv = GetAroundUV(parametersField.CountVertices);
            parametersField.Mesh.triangles = GetTriangles(parametersField.CurrentCountIteration);
            parametersField.Mesh.bounds = new Bounds(parametersField.Body.position, Vector3.one * 1000f);
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
        private static Vector3[] GetVerticesAround(int countIteration, Vector3 origin,
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
                    vertex = body.InverseTransformPoint(new Vector3(raycastHit2D.point.x,raycastHit2D.point.y, body.position.z)); 
                } 
                vertices[vertexIndex] = vertex;

                vertexIndex++; 
                angle -= angleIncrease; 
            }

            return vertices;
        }
        private static Vector2[] GetAroundUV(int countVerticesAround) => new Vector2[countVerticesAround];
        private static Vector3[] GetFieldVertices(ParametersField parametersField, float startingAngle)
        {
            float angle = startingAngle; 
            float angleIncreaseField = parametersField.AngleView / parametersField.CountIteration; 
            var vertices = new Vector3[parametersField.CountVertices];
            AddFirstVertices(ref vertices, angle, parametersField);
            int vertexIndex = 2; 
            for (int i = 1; i < parametersField.CountIteration; i++) 
            {
                Vector3 vertex;
                RaycastHit2D raycastHit2D = Physics2D.Raycast(
                    parametersField.Body.position,
                    UtilsClass.GetVectorFromAngle(angle),
                    parametersField.RadiusView,
                    parametersField.LayersView); 
                
                if (raycastHit2D.collider == null) 
                { 
// No hit 
                    vertex = parametersField.Body.InverseTransformPoint(
                        parametersField.Body.position + UtilsClass.GetVectorFromAngle(angle) * parametersField.RadiusView); 
                } 
                else 
                { 
// Hit object 
                    vertex = parametersField.Body.InverseTransformPoint(
                        new Vector3(raycastHit2D.point.x,raycastHit2D.point.y, parametersField.Body.position.z)); 
                } 
                vertices[vertexIndex] = vertex;

                vertexIndex++; 
                angle -= angleIncreaseField; 
            }

            var angleIncreaseAround = (360 - parametersField.AngleView) / parametersField.CountIterationAround;
            for (int i = 0; i <= parametersField.CountIterationAround; i++)
            {
                Vector3 vertex;
                RaycastHit2D raycastHit2D = Physics2D.Raycast(
                    parametersField.Body.position, UtilsClass.GetVectorFromAngle(angle),
                    parametersField.RadiusAroundView,
                    parametersField.LayersView); 
                if (raycastHit2D.collider == null) 
                { 
// No hit 
                    vertex = parametersField.Body.InverseTransformPoint(
                        parametersField.Body.position + UtilsClass.GetVectorFromAngle(angle) * parametersField.RadiusAroundView); 
                } 
                else 
                { 
// Hit object 
                    vertex = parametersField.Body.InverseTransformPoint(
                        new Vector3(raycastHit2D.point.x,raycastHit2D.point.y, parametersField.Body.position.z)); 
                } 
                vertices[vertexIndex] = vertex;

                vertexIndex++; 
                angle -= angleIncreaseAround; 
            }

            return vertices;
        }

        private static void AddFirstVertices(ref Vector3[] vertices,
            float angle,ParametersField parametersField)
        {
            RaycastHit2D firstRayCast = Physics2D.Raycast(
                parametersField.Body.position, UtilsClass.GetVectorFromAngle(angle),
                parametersField.RadiusAroundView,
                parametersField.LayersView); 
            if (firstRayCast.collider == null) 
            {
                vertices[0] = parametersField.Body.InverseTransformPoint(
                    parametersField.Body.position + UtilsClass.GetVectorFromAngle(angle) * parametersField.RadiusAroundView); 
            } 
            else 
            {
                vertices[0] = parametersField.Body.InverseTransformPoint(
                    new Vector3(firstRayCast.point.x,firstRayCast.point.y, parametersField.Body.position.z)); 
            }
            firstRayCast = Physics2D.Raycast(parametersField.Body.position,
                UtilsClass.GetVectorFromAngle(angle),
                parametersField.RadiusView,
                parametersField.LayersView); 
            if (firstRayCast.collider == null) 
            {
                vertices[1] = parametersField.Body.InverseTransformPoint(
                    parametersField.Body.position + UtilsClass.GetVectorFromAngle(angle) * parametersField.RadiusView); 
            } 
            else 
            {
                vertices[1] = parametersField.Body.InverseTransformPoint(
                    new Vector3(firstRayCast.point.x,firstRayCast.point.y, parametersField.Body.position.z)); 
            }
        }
    }

    public struct ParametersField
    {
        public readonly  LayerMask LayersView;
        public float AngleView;
        public readonly float RadiusView;
        public readonly float RadiusAroundView;
        public readonly  int CountIterationAround;
        public readonly  int CountIteration;
        public readonly  int CurrentCountIteration;
        public readonly Mesh Mesh;
        public readonly Transform Body;
        public readonly int CountVertices;

        public ParametersField(LayerMask layersView, float angleView, float radiusView, float radiusAroundView, int countIterationAround, int countIteration, Mesh mesh, Transform body)
        {
            LayersView = layersView;
            AngleView = angleView;
            RadiusView = radiusView;
            RadiusAroundView = radiusAroundView;
            CountIterationAround = countIterationAround;
            CountIteration = countIteration;
            CurrentCountIteration = countIterationAround + countIteration;
            Mesh = mesh;
            Body = body;
            CountVertices = CurrentCountIteration + 1 + 1;
        }
    }
}