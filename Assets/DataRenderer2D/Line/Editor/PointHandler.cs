using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using System;

namespace geniikw.DataRenderer2D.Editors
{
    public class PointHandler
    {
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

        readonly Component _owner;
        
        readonly SerializedProperty _mode;
        readonly SerializedProperty _line;
        readonly SerializedObject _target;

        public float m_sizeBuffer = 0f;
        public float m_distanceBuffer = 0f;
                
        readonly float AddButtonAngle = 20f;
        
        public IEnumerable<SerializedProperty> Points
        {
            get
            {
                if(!(_line.FindPropertyRelative("mode").enumValueIndex == 0))
                {
                    yield return _line.FindPropertyRelative("pair").FindPropertyRelative("n0");
                    yield return _line.FindPropertyRelative("pair").FindPropertyRelative("n1");
                }
                else
                {
                    var points = _line.FindPropertyRelative("points");
                    for (int i = 0; i < points.arraySize; i++)
                    {
                        yield return points.GetArrayElementAtIndex(i);
                    }
                }
            }
        }
        public SerializedProperty GetPoint(int index)
        {
            if(_line.FindPropertyRelative("mode").enumValueIndex == 1)
            {
                if (index == 0)
                    return _line.FindPropertyRelative("pair").FindPropertyRelative("n0");
                else
                    return _line.FindPropertyRelative("pair").FindPropertyRelative("n1");
            }
            return _line.FindPropertyRelative("points").GetArrayElementAtIndex(index);
        }
        public int Size
        {
            get {
                if (_line.FindPropertyRelative("mode").enumValueIndex== 1) return 2;
                return _line.FindPropertyRelative("points").arraySize; }
        }

        public PointHandler(Component owner, SerializedObject target)
        {
            _owner = owner;
            _target = target;

            _line = target.FindProperty("line");
                        
            var option = _line.FindPropertyRelative("option");
            
            _mode = option.FindPropertyRelative("mode");
           
        }

        public void OnSceneGUI()
        {
            if (Application.isPlaying)
                return;

            int index = 0;
            int size = Points.Count();

            foreach(var node in Points)
            {
                var position = node.FindPropertyRelative("position");

                if (_mode.enumValueIndex == 1 || index != size - 1)
                    HandleNextControlPoint(index, node);

                if (_mode.enumValueIndex == 1 || index != 0)
                    HandlePrevControlPoint(index, node);

                HandlePoint(index, position);
                index++;
            }
            if(_line.FindPropertyRelative("mode").enumValueIndex == 0)
                HandlesAddPointButton();
        }

        private void HandleNextControlPoint(int idx, SerializedProperty node)
        {
            var pos = _owner.transform.TransformPoint(node.FindPropertyRelative("position").vector3Value);
            var nextCOffset =  node.FindPropertyRelative("nextControlOffset");
            
            var nextNode = GetPoint(idx + 1 == Size ? 0 : idx + 1);
            var nextPosition = _owner.transform.TransformPoint(nextNode.FindPropertyRelative("position").vector3Value);

            var angle = Size == 2 && _mode.enumValueIndex == 1 ? AddButtonAngle : 0f;
            var nextDirection = Quaternion.Euler(Vector3.back * angle) * (nextPosition - pos).normalized;

            var nextDistance = Vector3.Distance(pos, nextPosition);

            var buttonSize = Mathf.Min(2f, nextDistance / 10f);
            m_sizeBuffer = buttonSize;
            m_distanceBuffer = Mathf.Max(0.5f, m_sizeBuffer * 6f);
            var buttonDistance = nextDistance / 4f;

            var buffer = Handles.color;
            Handles.color = Color.red;
            if (nextCOffset.vector3Value.magnitude < 1f)
            {
                if (nextCOffset.vector3Value != Vector3.zero)
                {
                    nextCOffset.vector3Value = Vector3.zero;
                    _target.ApplyModifiedProperties();
                }
                var buttonPosition = pos + nextDirection * buttonDistance;
                Handles.DrawDottedLine(pos, buttonPosition, 5f);
                if (Handles.Button(buttonPosition, _owner.transform.rotation, buttonSize, buttonSize, Handles.DotHandleCap))
                {
                    var mid = (pos + nextPosition) / 2f;
                    nextCOffset.vector3Value = _owner.transform.InverseTransformVector( mid - pos);
                    _target.ApplyModifiedProperties();
                }
            }
            else
            {
                HandleOffset(idx, nextCOffset, pos);
            }

            Handles.color = buffer;
        }

        private void HandlePrevControlPoint(int n, SerializedProperty node)
        {
            var pos = _owner.transform.TransformPoint(node.FindPropertyRelative("position").vector3Value);
            var prevCOffset = node.FindPropertyRelative("previousControlOffset");
            var prevNode = GetPoint(n - 1 < 0 ? Size - 1 : n - 1);
            var prevPosition = _owner.transform.TransformPoint(prevNode.FindPropertyRelative("position").vector3Value);

            var angle = Size == 2 && _mode.enumValueIndex == 1 ? AddButtonAngle : 0f;
            var prevDirection = Quaternion.Euler(Vector3.back * -angle) * (prevPosition - pos).normalized;

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
                    _target.ApplyModifiedProperties();
                }

                var buttonPosition = pos + prevDirection * buttonDistance;
                Handles.DrawDottedLine(pos, buttonPosition, 5f);
                if (Handles.Button(buttonPosition, _owner.transform.rotation, buttonSize, buttonSize, Handles.DotHandleCap))
                {
                    var mid = (pos + prevPosition) / 2f;
                    prevCOffset.vector3Value = _owner.transform.InverseTransformVector(mid - pos);
                    _target.ApplyModifiedProperties();
                }
            }
            else
            {
                HandleOffset(n, prevCOffset, pos);
            }

            Handles.color = buffer;
        }

        private void HandleOffset(int n, SerializedProperty offset, Vector3 position)
        {
            Handles.DrawDottedLine(position, position + _owner.transform.TransformVector(offset.vector3Value), 5f);

            EditorGUI.BeginChangeCheck();
            var pos =  position + _owner.transform.TransformVector(offset.vector3Value);
            var changedPosition = Handles.DoPositionHandle(pos, _owner.transform.rotation);
            Handles.Label(pos, n + " CP");
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(_owner, "edit offset");
                offset.vector3Value = _owner.transform.InverseTransformVector(changedPosition - position) ;
                offset.serializedObject.ApplyModifiedProperties();
            }
        }

        private void HandlePoint(int n, SerializedProperty position)
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
            ///add button
            Vector3 direction;

            var last = Size == 0 ? Vector3.zero : GetPoint(Size - 1).FindPropertyRelative("position").vector3Value;

            if (Size < 2)
                direction = Vector3.right;
            else
            {
                var last2 = GetPoint(Size - 2).FindPropertyRelative("position").vector3Value;
                direction = (last - last2).normalized;
            }

            var buffer = Handles.color;
            Handles.color = Color.green;
            
            if (Handles.Button(_owner.transform.TransformPoint(last + direction *m_distanceBuffer),
                               _owner.transform.rotation,
                               m_sizeBuffer,
                               m_sizeBuffer, 
                               Handles.DotHandleCap))
            {
                var index = Size;

           
                var width = index == 0 ? 1 :  GetPoint(index - 1).FindPropertyRelative("width").floatValue;

                _line.FindPropertyRelative("points").InsertArrayElementAtIndex(index);

                var addedPoint = _line.FindPropertyRelative("points").GetArrayElementAtIndex(index);
                addedPoint.FindPropertyRelative("position").vector3Value = index == 0 ? Vector3.zero : last + direction * m_distanceBuffer;
                addedPoint.FindPropertyRelative("previousControlOffset").vector3Value = Vector3.zero;
                addedPoint.FindPropertyRelative("nextControlOffset").vector3Value = Vector3.zero;
                addedPoint.FindPropertyRelative("width").floatValue = width;

                _target.ApplyModifiedProperties();
            }

            //delete button
            if (Size > 1)
            {
                Handles.color = Color.black;
                if (Handles.Button(_owner.transform.TransformPoint(last + direction * m_distanceBuffer * 2), 
                                   _owner.transform.rotation,
                                   m_sizeBuffer,
                                   m_sizeBuffer, 
                                   Handles.DotHandleCap))
                {
                    _line.FindPropertyRelative("points").DeleteArrayElementAtIndex(Size - 1);
                    _target.ApplyModifiedProperties();
                }
            }
            Handles.color = buffer;
        }
    }
}