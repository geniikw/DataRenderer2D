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
    ///          Mode mode;
    ///     float startRatio
    ///     float endRatio

    readonly SerializedProperty _points;
    readonly Component _owner;
    readonly SerializedProperty _line;

    readonly SerializedProperty _normal;
    readonly SerializedProperty _mode;

    readonly float AddDeleteButtonSize = 2f;
    readonly float AddButtonDistance = 10f;
    readonly float AddInitialDistance = 30f;
    readonly float DeleteButtonDistance = 20f;

    readonly float AddButtonAngle = 20f;

    public PointHandler(Component owner, SerializedProperty line)
    {
        _owner = owner;
        _line = line;
        _points = line.FindPropertyRelative("points");
        _normal = line.FindPropertyRelative("normalVector");
        _mode = line.FindPropertyRelative("mode");
    }
    
    public void OnSceneGUI()
    {
        for (int i = 0; i < _points.arraySize; i++)
        {
            var node = _points.GetArrayElementAtIndex(i);
            var position = node.FindPropertyRelative("position");


            if(_mode.enumValueIndex == 1 || i != _points.arraySize-1)
                HandleNextControlPoint(i, node);

            if(_mode.enumValueIndex == 1 || i != 0)
                HandlePrevControlPoint(i, node);
            
            HandlePoint(i, position);
        }
        HandlesAddPointButton();
    }

    private void HandleNextControlPoint(int idx, SerializedProperty node)
    {
        var pos = _owner.transform.TransformPoint(node.FindPropertyRelative("position").vector3Value);
        var nextCOffset = node.FindPropertyRelative("nextControlOffset");
        var nextNode = _points.GetArrayElementAtIndex(idx + 1 == _points.arraySize ? 0 : idx + 1);
        var nextPosition = _owner.transform.TransformPoint(nextNode.FindPropertyRelative("position").vector3Value);

        var angle = _points.arraySize == 2 && _mode.enumValueIndex == 1 ? AddButtonAngle : 0f;
        var nextDirection = Quaternion.Euler(_normal.vector3Value* angle) *(nextPosition - pos).normalized;

        var nextDistance = Vector3.Distance(pos, nextPosition);
        
        var buttonSize = Mathf.Min(2f, nextDistance / 10f);
        var buttonDistance =  nextDistance / 4f;

        var buffer = Handles.color;
        Handles.color = Color.red;
        if (nextCOffset.vector3Value.magnitude < 1f)
        {
            if (nextCOffset.vector3Value != Vector3.zero)
            {
                nextCOffset.vector3Value = Vector3.zero;
                _points.serializedObject.ApplyModifiedProperties();
            }
            var buttonPosition = pos + nextDirection * buttonDistance;
            Handles.DrawDottedLine(pos, buttonPosition, 5f);
            if (Handles.Button(buttonPosition, _owner.transform.rotation, buttonSize, buttonSize, Handles.DotHandleCap))
            {
                var mid = (pos + nextPosition) / 2f;
                nextCOffset.vector3Value = mid - pos;
            }
        }
        else
        {
            Handles.DrawDottedLine(pos, pos + nextCOffset.vector3Value, 5f);
            HandleOffset(idx, nextCOffset, pos);
        }

        Handles.color = buffer;
    }

    private void HandlePrevControlPoint(int n, SerializedProperty node)
    {
        var pos = _owner.transform.TransformPoint(node.FindPropertyRelative("position").vector3Value);
        var prevCOffset = node.FindPropertyRelative("previousControlOffset");
        var prevNode = _points.GetArrayElementAtIndex(n - 1 < 0 ? _points.arraySize - 1 : n - 1);
        var prevPosition = _owner.transform.TransformPoint(prevNode.FindPropertyRelative("position").vector3Value);

        var angle = _points.arraySize == 2 && _mode.enumValueIndex == 1 ? AddButtonAngle : 0f;
        var prevDirection = Quaternion.Euler(_normal.vector3Value * -angle) * (prevPosition - pos).normalized;

        var nextDistance = Vector3.Distance(pos, prevPosition);

        var buttonSize = Mathf.Min(2f, nextDistance / 10f);
        var buttonDistance = nextDistance / 4f;

        var buffer = Handles.color;
        Handles.color = Color.blue;
        if (prevCOffset.vector3Value.magnitude < 1f)
        {
            if (prevCOffset.vector3Value != Vector3.zero)
            {
                prevCOffset.vector3Value = Vector3.zero;
                _points.serializedObject.ApplyModifiedProperties();
            }

            var buttonPosition = pos + prevDirection * buttonDistance;
            Handles.DrawDottedLine(pos, buttonPosition, 5f);
            if (Handles.Button(buttonPosition, _owner.transform.rotation, buttonSize, buttonSize, Handles.DotHandleCap))
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


    private void HandlesAddPointButton()
    {
        Vector3 direction;

        var last = _points.arraySize == 0 ? Vector3.zero : _points.GetArrayElementAtIndex(_points.arraySize - 1).FindPropertyRelative("position").vector3Value;

        if (_points.arraySize < 2)
            direction = Vector3.right;
        else
        {
            var last2 = _points.GetArrayElementAtIndex(_points.arraySize - 2).FindPropertyRelative("position").vector3Value;
            direction = (last - last2).normalized;
        }

        var buffer = Handles.color;
        Handles.color = Color.green;
        if (Handles.Button(_owner.transform.TransformPoint(last + direction * AddButtonDistance), _owner.transform.rotation, AddDeleteButtonSize, AddDeleteButtonSize, Handles.DotHandleCap))
        {
            var index = _points.arraySize;
            _points.InsertArrayElementAtIndex(index);
            _points.GetArrayElementAtIndex(index).FindPropertyRelative("position").vector3Value = index == 0 ? Vector3.zero : last + direction * AddInitialDistance;
            _points.GetArrayElementAtIndex(index).FindPropertyRelative("previousControlOffset").vector3Value = Vector3.zero;
            _points.GetArrayElementAtIndex(index).FindPropertyRelative("nextControlOffset").vector3Value = Vector3.zero;
            _points.GetArrayElementAtIndex(index).FindPropertyRelative("width").floatValue = 2f;


            _points.serializedObject.ApplyModifiedProperties();
        }

        if (_points.arraySize > 1)
        {
            Handles.color = Color.black;
            if (Handles.Button(_owner.transform.TransformPoint(last + direction * DeleteButtonDistance), _owner.transform.rotation, AddDeleteButtonSize, AddDeleteButtonSize, Handles.DotHandleCap))
            {
                _points.DeleteArrayElementAtIndex(_points.arraySize - 1);
                _points.serializedObject.ApplyModifiedProperties();
            }
        }
        Handles.color = buffer;
    }
}
