using _Source.Interactable;
using UnityEditor;
namespace _Source.Services
{
#if (UNITY_EDITOR)
    [CustomEditor(typeof(AmmoObject))]
    public class CustomEditorAmmo : Editor
    {
        private SerializedProperty _typeAmmo;
        private SerializedProperty _countBullet;
        private AmmoObject _target;
        private int _maxCountBullet;

        private void OnEnable()
        {
            _typeAmmo = serializedObject.FindProperty("typeAmmo");
            _countBullet = serializedObject.FindProperty("countBullet");
            _target = (AmmoObject)target;
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(_typeAmmo);
            if (_target.TypeAmmo != null)
            {
                _maxCountBullet = _target.TypeAmmo.CountBullet;
                EditorGUILayout.IntSlider(_countBullet, 1, _maxCountBullet);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
    #endif
}