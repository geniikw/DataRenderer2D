using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;

namespace geniikw.DataRenderer2D.Editors {
        
    public class LineEditor : Editor
    {
        private PointHandler _pointHandler;
        //private LineInpectorHandler _inspector;
        private MonoBehaviour _owner;

        protected void OnEnable()
        { 
            _owner = target as MonoBehaviour;

            _pointHandler = new PointHandler(_owner,serializedObject);
            //_inspector = new LineInpectorHandler(serializedObject);
        }
        protected void OnSceneGUI()
        {
            _pointHandler.OnSceneGUI();
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }
    
    [CustomEditor(typeof(WorldLine))]
    public class WorldLineEditor : LineEditor { }

    [CustomEditor(typeof(GizmoLine))]
    public class NoRenderLineEditor : LineEditor { }

}



