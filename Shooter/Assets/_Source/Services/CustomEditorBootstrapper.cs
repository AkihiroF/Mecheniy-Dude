using _Source.Core;
using _Source.FireSystem.Player;
using _Source.HealthSystem;
using _Source.Interactable;
using _Source.Player;
using UnityEditor;

namespace _Source.Services
{
    [CustomEditor(typeof(Bootstrapper))]
    public class CustomEditorBootstrapper : Editor
    {
        private SerializedProperty _playerMovement;
        private SerializedProperty _playerFireSystem;
        private SerializedProperty _playerHealth;
        private SerializedProperty _playerInteractiveComponent;

        private void OnEnable()
        {
            _playerMovement = serializedObject.FindProperty("playerMovement");
            _playerFireSystem = serializedObject.FindProperty("playerFireSystem");
            _playerHealth = serializedObject.FindProperty("playerHealth");
            _playerInteractiveComponent = serializedObject.FindProperty("playerInteractiveComponent");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (_playerMovement.objectReferenceValue == null)
            {
                _playerMovement.objectReferenceValue = FindObjectOfType<PlayerMovement>();
            }
            if (_playerFireSystem.objectReferenceValue == null)
            {
                _playerFireSystem.objectReferenceValue = FindObjectOfType<PlayerFireSystem>();
            }
            if (_playerHealth.objectReferenceValue == null)
            {
                _playerHealth.objectReferenceValue = FindObjectOfType<PlayerHealth>();
            }
            if (_playerInteractiveComponent.objectReferenceValue == null)
            {
                _playerInteractiveComponent.objectReferenceValue = FindObjectOfType<PlayerInteractiveComponent>();
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}