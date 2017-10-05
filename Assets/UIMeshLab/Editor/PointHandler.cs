using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PointHandler {

    /// Line 
    ///     List<Node> points 
    ///          Vector3 position;
    ///          Vector3 previousControlOffset;
    ///          Vector3 nextControlOffset;
    ///          Color color;
    ///          float width;
    ///          float angle;
    ///          int nextDivieCount;
    ///          bool loop;
    ///     float startRatio
    ///     float endRatio

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
            var node = _points.GetArrayElementAtIndex(i);
            var position = node.FindPropertyRelative("position");
            HandleControlPoint(i, node);
            HandlePoint(i, position);
        }
    }

    private void HandleControlPoint(int n, SerializedProperty node)
    {
        var pos = _owner.transform.TransformPoint(node.FindPropertyRelative("position").vector3Value);
        var prevCOffset = node.FindPropertyRelative("previousControlOffset").vector3Value;
        var nextCOffset = node.FindPropertyRelative("nextControlOffset").vector3Value;

        var colorBuffer = Handles.color;
        Handles.color = Color.red;
        Handles.DrawDottedLine(pos, pos + prevCOffset, 5f);
        Handles.DrawDottedLine(pos, pos + nextCOffset, 5f);
        Handles.color = colorBuffer;


    }

    private void HandlePoint(int n,SerializedProperty position)
    {
        EditorGUI.BeginChangeCheck();
        var pos = _owner.transform.TransformPoint(position.vector3Value);
        var changedPosition = Handles.DoPositionHandle(pos, _owner.transform.rotation);
        Handles.Label(pos, n + " point");
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(_owner, "edit point");
            position.vector3Value = _owner.transform.InverseTransformPoint(changedPosition);
            position.serializedObject.ApplyModifiedProperties();
        }
    }
}
