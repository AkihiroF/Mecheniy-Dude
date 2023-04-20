using _Source.Saving_System;
using UnityEditor;

namespace _Source.Services
{
    [CustomEditor(typeof(SaverData))]
    public class CustomEditorSaverData : Editor
    {
        private SerializedProperty _saverComponent;
        private PlayerSaverComponent _playerSaverComponent;

        private void OnEnable()
        {
            _saverComponent = serializedObject.FindProperty("saverComponent");
            if(_saverComponent.objectReferenceValue == null)
                _playerSaverComponent = FindObjectOfType<PlayerSaverComponent>();
        }

        public override void OnInspectorGUI()
        {
            if(_saverComponent.objectReferenceValue == null)
                _saverComponent.objectReferenceValue = _playerSaverComponent;
            EditorGUILayout.PropertyField(_saverComponent);
            serializedObject.ApplyModifiedProperties();
        }
    }
}