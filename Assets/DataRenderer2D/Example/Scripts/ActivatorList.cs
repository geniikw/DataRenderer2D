using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace geniikw.DataRenderer2D.Example
{
    public class ActivatorList : MonoBehaviour
    {
        public List<ActivatorModule> list = new List<ActivatorModule>();
        public bool actFlag = false;
        
        public void Activate(float time)
        {
            foreach(var item in list)
            {
                item.Activate(time);
            }
        }

        public void Deactivate(float time)
        {
            foreach(var item in list)
            {
                item.Deactivate(time);
            }
        }
        
        public void Hide(float time)
        {
            foreach(var item in list)
            {
                item.Hide(time);
            }
        }
        
    }


#if UNITY_EDITOR
    [CustomEditor(typeof(ActivatorList))]
    public class ActiEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("active"))
            {
                ((ActivatorList)target).Activate(0);
            }

            if (GUILayout.Button("deactive"))
            {
                ((ActivatorList)target).Deactivate(0);
            }

            if (GUILayout.Button("hide"))
            {
                ((ActivatorList)target).Hide(0);
            }

        }
    }

#endif
}