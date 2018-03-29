using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace geniikw.DataRenderer2D.Editors
{
    public static class MenuExtender 
    {
        //[ContextMenu()]
        [MenuItem("GameObject/2D Object/DataRenderer/UILine")]
        public static void CreateUILine()
        {
            var canvas = Object.FindObjectOfType<Canvas>();
            if(canvas == null)
            {
                var canvasGo = new GameObject("Canvas");
                canvas = canvasGo.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            }

            var linego = new GameObject("line");
            var uiline =linego.AddComponent<UILine>();
            uiline.line.Initialize();
            
            uiline.transform.SetParent(canvas.transform);
            uiline.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
     
            Selection.activeObject = linego;
        }
        
        //[ContextMenu()]
        [MenuItem("GameObject/2D Object/DataRenderer/GizmoLine")]
        public static void CreateNoRenderLine()
        {
            var linego = new GameObject("line");
            var gizmoLine = linego.AddComponent<GizmoLine>();
            gizmoLine.line = Spline.Default;
            
            Selection.activeObject = linego;
        }

        [MenuItem("GameObject/2D Object/DataRenderer/WorldLine")]
        public static void CreateWorldLine()
        {
            var linego = new GameObject("line");
            var worldLine = linego.AddComponent<WorldLine>();
            worldLine.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Diffuse"));
            worldLine.line.Initialize();
            
            Selection.activeObject = linego;
        }


    }
}