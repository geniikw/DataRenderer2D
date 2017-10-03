using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PointHandler {

    readonly SerializedProperty _points;
    readonly Component _owner;
    
    public PointHandler(SerializedProperty points, Component owner)
    {
        _points = points;
        _owner = owner;
    }
    
    public void OnSceneGUI()
    {
        
        for (int i = 0; i < _points.arraySize; i++)
        {
            var point = _points.GetArrayElementAtIndex(i);
            var position = point.FindPropertyRelative("position");

            HandlePoint(position);
        }
    }

    private void HandlePoint(SerializedProperty position)
    {
        EditorGUI.BeginChangeCheck();
        var pos = _owner.transform.TransformPoint(position.vector3Value);
        var changedPosition = Handles.DoPositionHandle(pos, _owner.transform.rotation);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(_owner, "edit point");
            position.vector3Value = _owner.transform.InverseTransformPoint(changedPosition);
            position.serializedObject.ApplyModifiedProperties();
        }
    }
}
