using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;

namespace geniikw.DataRenderer2D.Editors
{
    [CustomPropertyDrawer(typeof(Point))]
    public class PointPropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var lineCount = 4.5f;

            if (EditorSetting.Get.onlyViewWidth)
                lineCount = 1f;

            return lineCount * EditorGUIUtility.singleLineHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty sp, GUIContent label)
        {
            var labelPos = position;
            labelPos.x = 0f;

            EditorGUI.LabelField(labelPos, (Regex.Match(label.text, @"\d+").Value).ToString());
           
            var pos = position;
            pos.height = EditorGUIUtility.singleLineHeight;
            if (!EditorSetting.Get.onlyViewWidth)
            {
                EditorGUI.PropertyField(pos, sp.Position());
                pos.y += EditorGUIUtility.singleLineHeight;

                EditorGUI.PropertyField(pos, sp.Width());
                pos.y += EditorGUIUtility.singleLineHeight;

                EditorGUI.PropertyField(pos, sp.NextOffset());
                pos.y += EditorGUIUtility.singleLineHeight;
                

                EditorGUI.PropertyField(pos, sp.PrevOffset());
                pos.y += EditorGUIUtility.singleLineHeight;
                
                pos.height /= 2f;
                EditorGUI.DrawRect(pos, Color.black);
            }
            else
            {
                EditorGUI.PropertyField(pos, sp.Width());
            }
        }
 
    }
    static class SerializedPropertyAccessor
    {
        public static SerializedProperty Position(this SerializedProperty sp)
        {
            return sp.FindPropertyRelative("position");
        }
        public static SerializedProperty PrevOffset(this SerializedProperty sp)
        {
            return sp.FindPropertyRelative("previousControlOffset");
        }
        public static SerializedProperty NextOffset(this SerializedProperty sp)
        {
            return sp.FindPropertyRelative("nextControlOffset");
        }
        public static SerializedProperty Width(this SerializedProperty sp)
        {
            return sp.FindPropertyRelative("width");
        }
    }
}