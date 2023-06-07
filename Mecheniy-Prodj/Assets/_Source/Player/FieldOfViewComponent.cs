using System;
using System.Linq;
using _Source.Services;
using _Source.SignalsEvents.UpgradesEvents;
using UnityEngine;
using UnityEngine.Serialization;
using Vector3 = UnityEngine.Vector3;

namespace _Source.Player
{
    public class FieldOfViewComponent : MonoBehaviour
    {
        [SerializeField] private LayerMask layersView;
        [SerializeField] private float angleView;
        [SerializeField] private float radiusView;
        [Space]
        [SerializeField] private float radiusAroundView;
        [SerializeField] private int countIteration;
        [SerializeField] private int countIterationAround;
        
        private Mesh _exitMesh;

        private FieldOfView _calculator;

        private void Awake()
        {
            Signals.Get<OnUpgradeAngleVision>().AddListener(UpgradeAngle);
            _calculator = new FieldOfView(
                layersView,
                angleView,
                radiusView,
                countIteration,
                transform,
                radiusAroundView,
                countIterationAround);
        }

        private void Start()
        {
            _exitMesh = new Mesh(); 
            GetComponent<MeshFilter>().mesh = _exitMesh;
            Renderer myRenderer = GetComponent<Renderer>();
            myRenderer.sortingLayerName = "FieldOfView";
            myRenderer.sortingOrder = 10;
        }

        private void UpgradeAngle(float percent)
        {
            angleView += angleView * percent / 100;
            _calculator.UpgradeAngle(angleView);
        }

        private void Update() 
        { 
            _calculator.SetOrigin(transform.position); 
            _calculator.SetAimDirection(transform.up);
        } 

        private void LateUpdate()
        {
            _calculator.UpdateMesh(ref _exitMesh);
            _exitMesh.Optimize();
            
        }

        private void OnDestroy()
        {
            Signals.Get<OnUpgradeAngleVision>().RemoveListener(UpgradeAngle);
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