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

    readonly float buttonSize = 1f;
    readonly float buttonDistance = 5f;

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

        HandlesAddPointButton();
    }

    private void HandlesAddPointButton()
    {
        Vector3 direction;
        var last = _points.GetArrayElementAtIndex(_points.arraySize - 1).FindPropertyRelative("position").vector3Value;
        if (_points.arraySize < 2)
            direction = Vector3.right;
        else
        {
            var last2 = _points.GetArrayElementAtIndex(_points.arraySize - 2).FindPropertyRelative("position").vector3Value;
            direction = (last - last2).normalized;
        }


        var buffer = Handles.color;
        Handles.color = Color.green;
        if (Handles.Button(_owner.transform.TransformPoint(last + direction * 5f), _owner.transform.rotation, 1, 1, Handles.DotHandleCap))
        {
            _points.InsertArrayElementAtIndex(_points.arraySize);
            _points.GetArrayElementAtIndex(_points.arraySize - 1).FindPropertyRelative("position").vector3Value = last + direction * 5f;
            _points.serializedObject.ApplyModifiedProperties();
        }
        
        if(_points.arraySize > 1)
        {
            Handles.color = Color.black;
            if (Handles.Button(_owner.transform.TransformPoint(last + direction * 3f), _owner.transform.rotation, 1, 1, Handles.DotHandleCap))
            {
                _points.DeleteArrayElementAtIndex(_points.arraySize-1);
                _points.serializedObject.ApplyModifiedProperties();
            }
        }
        
        Handles.color = buffer;

    }

    private void HandleControlPoint(int n, SerializedProperty node)
    {
        var pos = _owner.transform.TransformPoint(node.FindPropertyRelative("position").vector3Value);
        var prevCOffset = node.FindPropertyRelative("previousControlOffset");
        var prevNode = _points.GetArrayElementAtIndex(n - 1 < 0 ? _points.arraySize - 1 : n - 1);
        var prevPosition = _owner.transform.TransformPoint(prevNode.FindPropertyRelative("position").vector3Value);
        var prevDirection = (prevPosition - pos).normalized;

        var nextCOffset = node.FindPropertyRelative("nextControlOffset");
        var nextNode = _points.GetArrayElementAtIndex(n + 1 == _points.arraySize ? 0 : n + 1);
        var nextPosition = _owner.transform.TransformPoint(nextNode.FindPropertyRelative("position").vector3Value);
        var nextDirection = (nextPosition - pos).normalized;

        var buffer = Handles.color;
        Handles.color = Color.blue;
        if (prevCOffset.vector3Value.magnitude < 1f)
        {
            if (prevCOffset.vector3Value != Vector3.zero)
            {
                prevCOffset.vector3Value = Vector3.zero;
                _points.serializedObject.ApplyModifiedProperties();
            }

            if (Handles.Button(pos + prevDirection * buttonDistance, _owner.transform.rotation, buttonSize, buttonSize, Handles.CubeHandleCap))
            {
                var mid = (pos + prevPosition) / 2f;
                prevCOffset.vector3Value = mid - pos;
                _points.serializedObject.ApplyModifiedProperties();
            }
        }
        else
        {
            Handles.DrawDottedLine(pos, pos + prevCOffset.vector3Value, 5f);
            HandleOffset(n, prevCOffset, pos);
        }

        Handles.color = Color.red;
        if (nextCOffset.vector3Value.magnitude < 1f)
        {
            if(nextCOffset.vector3Value != Vector3.zero)
            {
                nextCOffset.vector3Value = Vector3.zero;
                _points.serializedObject.ApplyModifiedProperties();
            }
            
            if (Handles.Button(pos + nextDirection * buttonDistance, _owner.transform.rotation, buttonSize, buttonSize, Handles.CubeHandleCap))
            {
                var mid = (pos + nextPosition) / 2f;
                nextCOffset.vector3Value = mid - pos;
                _points.serializedObject.ApplyModifiedProperties();
            }
        }
        else
        {
            Handles.DrawDottedLine(pos, pos + nextCOffset.vector3Value, 5f);
            HandleOffset(n, nextCOffset, pos);
        }
        Handles.color = buffer;
    }

    private void HandleOffset(int n, SerializedProperty offset, Vector3 position)
    {
        EditorGUI.BeginChangeCheck();
        var pos = position + offset.vector3Value;
        var changedPosition = Handles.DoPositionHandle(pos, _owner.transform.rotation);
        Handles.Label(pos, n + " CP");
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(_owner, "edit offset");
            offset.vector3Value = changedPosition - position;
            offset.serializedObject.ApplyModifiedProperties();
        }
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
