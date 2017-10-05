using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace geniikw.UIMeshLab.Editors {
        
    public class LineEditor : Editor
    {
        protected SerializedProperty _line;
        protected SerializedProperty _points;
        private PointHandler _pointHandler;
        private MonoBehaviour _owner;
        
        protected void OnEnable()
        {
            _line = serializedObject.FindProperty("line");
            _points = _line.FindPropertyRelative("points");
            _owner = target as MonoBehaviour;

            _pointHandler = new PointHandler(_points, _owner);
        }
        protected void OnSceneGUI()
        {
            _pointHandler.OnSceneGUI();
        }
    }
    
    [CustomEditor(typeof(UILine))]
    public class UILineEditor : LineEditor {}

    [CustomEditor(typeof(WorldLine))]
    public class WorldLineEditor : LineEditor { }

    [CustomEditor(typeof(GizmoLine))]
    public class NoRenderLineEditor : LineEditor { }

}



