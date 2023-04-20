using _Source.FireSystem.Player;
using _Source.HealthSystem;
using _Source.Saving_System;
using UnityEditor;
using UnityEngine;

namespace _Source.Services
{
    [CustomEditor(typeof(PlayerSaverComponent))]
    public class CustomEditorPlayerSaverComponent : Editor
    {
        private SerializedProperty _healthComponent;
        private SerializedProperty _fireSystem;
        private void OnEnable()
        {
            _healthComponent = serializedObject.FindProperty("health");
            _fireSystem = serializedObject.FindProperty("playerFireSystem");
            
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (_healthComponent.objectReferenceValue is null)
            {
                _healthComponent.objectReferenceValue = FindObjectOfType<PlayerHealth>();
            }
            if (_fireSystem.objectReferenceValue is null)
            {
                _fireSystem.objectReferenceValue = FindObjectOfType<PlayerFireSystem>();
            }
            serializedObject.ApplyModifiedProperties();
            if (GUILayout.Button("Delete data"))
            {
                PlayerSaverComponent myScript = target as PlayerSaverComponent;
                if (myScript != null) myScript.ClearData();
            }
            
        }


    }
}