using System.Collections; 
using System.Collections.Generic;
using CodeMonkey.Utils;
using UnityEngine;

public class FieldOfView : MonoBehaviour 
{ 
[SerializeField] private LayerMask layerMask; 
private Mesh mesh; 
private float fov; 
private float viewDistance; 
private Vector3 origin; 
private float startingAngle; 

private void Start() 
{ 
mesh = new Mesh(); 
GetComponent<MeshFilter>().mesh = mesh; 
fov = 90f; 
viewDistance = 50f; 
origin = Vector3.zero; 
} 

private void Update() 
{ 
SetOrigin(transform.position); 
SetAimDirection(transform.up); 
} 

private void LateUpdate() 
{ 
int rayCount = 50; 
float angle = startingAngle; 
float angleIncrease = fov / rayCount; 

Vector3[] vertices = new Vector3[rayCount + 1 + 1]; 
Vector2[] uv = new Vector2[vertices.Length]; 
int[] triangles = new int[rayCount * 3]; 

vertices[0] = transform.InverseTransformPoint(origin); 

int vertexIndex = 1; 
int triangleIndex = 0; 
for (int i = 0; i <= rayCount; i++) 
{ 
Vector3 vertex; 
RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, UtilsClass.GetVectorFromAngle(angle), viewDistance, layerMask); 
if (raycastHit2D.collider == null) 
{ 
// No hit 
vertex = transform.InverseTransformPoint(origin + UtilsClass.GetVectorFromAngle(angle) * viewDistance); 
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


mesh.vertices = vertices; 
mesh.uv = uv; 
mesh.triangles = triangles; 
mesh.bounds = new Bounds(origin, Vector3.one * 1000f); 
} 

public void SetOrigin(Vector3 origin) 
{ 
this.origin = origin; 
} 

public void SetAimDirection(Vector3 aimDirection) 
{ 
startingAngle = UtilsClass.GetAngleFromVectorFloat(aimDirection) + fov / 2f; 
}
}
