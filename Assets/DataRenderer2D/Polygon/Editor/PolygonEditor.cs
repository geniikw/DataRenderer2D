using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;

namespace geniikw.DataRenderer2D.Polygon.Editors
{
    [CustomEditor(typeof(UIPolygon))]
    public class PolygonEditor : ImageEditor
    {
        private SerializedProperty m_data;

        protected override void OnEnable()
        {
            base.OnEnable();
            m_data = serializedObject.FindProperty("data");
            
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();
            EditorGUILayout.PropertyField(m_data, true);

            serializedObject.ApplyModifiedProperties();   
        }
    }
}