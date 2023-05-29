using System;
using UnityEngine;

namespace _Source.Lighting
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class LightComponent : MonoBehaviour
    {
        [SerializeField] private LayerMask layersView;
        [SerializeField] private float radiusView;
        [SerializeField] private int countIteration;
        [SerializeField] private bool isStatic;

        private Mesh _exitMesh;
        private Vector3 _origin;

        private void Start()
        {
            _exitMesh = new Mesh(); 
            GetComponent<MeshFilter>().mesh = _exitMesh;
            _origin = Vector3.zero;
            Renderer myRenderer = GetComponent<Renderer>();
            myRenderer.sortingLayerName = "FieldOfView";
            myRenderer.sortingOrder = 10;
            _origin = transform.position;
            if (isStatic)
            {
                LightMathf.UpdateAroundMesh(ref _exitMesh, countIteration, _origin, transform, radiusView, layersView);
                this.enabled = false;
            }
        }

        private void LateUpdate()
        {
            LightMathf.UpdateAroundMesh(ref _exitMesh, countIteration, _origin, transform, radiusView, layersView);
        }
#if (UNITY_EDITOR)
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            var position = transform.position;
            UnityEditor.Handles.DrawWireDisc(position, Vector3.forward, radiusView);
        }
#endif
    }
}