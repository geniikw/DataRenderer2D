using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace geniikw.UIMeshLab.Editors
{
    public static class MenuExtender 
    {
        public static Spline BasicLine
        {
            get
            {
                var line = Spline.Default;
                line.points.Add(Point.Zero);

                var p1 = Point.Zero;
                p1.position = Vector3.right * 30f;
                line.points.Add(p1);
                line.option = LineOption.Default;
                return line;
            }
        }

        //[ContextMenu()]
        [MenuItem("GameObject/2D Object/Line/UILine")]
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
            uiline.line = BasicLine;
            
            uiline.transform.SetParent(canvas.transform);
            uiline.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
     
            Selection.activeObject = linego;
        }
        
        //[ContextMenu()]
        [MenuItem("GameObject/2D Object/Line/GizmoLine")]
        public static void CreateNoRenderLine()
        {
            var linego = new GameObject("line");
            var gizmoLine = linego.AddComponent<GizmoLine>();
            gizmoLine.line = BasicLine;


            Selection.activeObject = linego;
        }

        [MenuItem("GameObject/2D Object/Line/WorldLine")]
        public static void CreateWorldLine()
        {
            var linego = new GameObject("line");
            var worldLine = linego.AddComponent<WorldLine>();
            worldLine.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Diffuse"));
            worldLine.line = BasicLine;
            
            Selection.activeObject = linego;
        }


    }
}