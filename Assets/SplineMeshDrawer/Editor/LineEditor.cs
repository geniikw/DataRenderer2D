using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace geniikw.UIMeshLab.Editors {
        
    public class LineEditor : Editor
    {
        private PointHandler _pointHandler;
        private MonoBehaviour _owner;
        
        protected void OnEnable()
        {
            _owner = target as MonoBehaviour;
                  
            _pointHandler = new PointHandler(_owner,serializedObject);
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



