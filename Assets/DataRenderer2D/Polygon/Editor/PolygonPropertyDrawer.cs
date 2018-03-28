using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace geniikw.DataRenderer2D.Polygon.Editors
{
    [CustomPropertyDrawer(typeof(PolygonData))]
    public class PolygonPropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var height = 5f;
            property.serializedObject.Update();
            var type = (PolygonType)property.FindPropertyRelative("type").enumValueIndex;
            if (type >= PolygonType.Hole)
                height += 3f;

            if (type == PolygonType.HoleCurve)
                height += 1;
            if (type >= PolygonType.HoleCenterColor)
                height += 1;


            height *= EditorGUIUtility.singleLineHeight;
            return height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            property.serializedObject.Update();
            position.height = EditorGUIUtility.singleLineHeight;
            var type = (PolygonType)property.FindPropertyRelative("type").enumValueIndex;

            EditorGUI.PropertyField(position, property.FindPropertyRelative("type"));
            position.y += EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("sinCft"));
            position.y += EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("cosCft"));
            position.y += EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("count"));
            position.y += EditorGUIUtility.singleLineHeight;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("outerColor"));

            if (type >= PolygonType.Hole)
            {
                position.y += EditorGUIUtility.singleLineHeight;
                EditorGUI.PropertyField(position, property.FindPropertyRelative("startAngle"));
                position.y += EditorGUIUtility.singleLineHeight;
                EditorGUI.PropertyField(position, property.FindPropertyRelative("endAngle"));
                position.y += EditorGUIUtility.singleLineHeight;
                EditorGUI.PropertyField(position, property.FindPropertyRelative("innerRatio"));
            
            }
           
            if(type >= PolygonType.HoleCenterColor)
            {
                position.y += EditorGUIUtility.singleLineHeight;
                EditorGUI.PropertyField(position, property.FindPropertyRelative("centerColor"));
            }

            if (type == PolygonType.HoleCurve)
            {
                position.y += EditorGUIUtility.singleLineHeight;
                EditorGUI.PropertyField(position, property.FindPropertyRelative("curve"));
            }

            property.serializedObject.ApplyModifiedProperties();

            //base.OnGUI(position, property, label);
        }
    }
}