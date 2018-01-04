//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;

//public class LineInpectorHandler  {

//    readonly SerializedProperty _p0;
//    readonly SerializedProperty _p1;
//    readonly SerializedProperty _points;
//    readonly SerializedProperty _option;
//    readonly SerializedProperty _mat;

//    readonly SerializedProperty _useLinePoints;

//    public LineInpectorHandler(SerializedObject target)
//    {
//        _mat = target.FindProperty("m_Material");
//    }

//    public void OnInspector()
//    {
//        EditorGUI.BeginChangeCheck();
//        EditorGUILayout.PropertyField(_mat);
//        if (EditorGUI.EndChangeCheck())
//        {
//            _mat.serializedObject.ApplyModifiedProperties();
//        }
//    }
//}
