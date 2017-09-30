using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace geniikw.UIMeshLab.Editors {
    
    
    public class LineEditor : Editor
    {
        protected SerializedProperty Line
        {
            get
            {
                return serializedObject.FindProperty("line");
            }
        }

        private void OnSceneGUI()
        {



        }
    }
    
    [CustomEditor(typeof(UILine))]
    public class UILineEditor : LineEditor { }

    [CustomEditor(typeof(WorldLine))]
    public class WorldLineEditor : LineEditor { }


}



