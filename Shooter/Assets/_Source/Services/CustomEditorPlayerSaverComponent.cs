using _Source.Saving_System;
using UnityEditor;
using UnityEngine;

namespace _Source.Services
{
    [CustomEditor(typeof(PlayerSaverComponent))]
    public class CustomEditorPlayerSaverComponent : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Delete data"))
            {
                PlayerSaverComponent myScript = target as PlayerSaverComponent;
                if (myScript != null) myScript.ClearData();
            }
        }


    }
}