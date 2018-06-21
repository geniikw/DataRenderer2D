using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace geniikw.DataRenderer2D.Hole.Editor
{
    public static class MenuExtender
    {
        [MenuItem("GameObject/2D Object/DataRenderer/UIHole")]
        public static void CreateUILine()
        {
            var canvas = Object.FindObjectOfType<Canvas>();
            if (canvas == null)
            {
                var canvasGo = new GameObject("Canvas");
                canvas = canvasGo.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            }

            var linego = new GameObject("hole");
            var uiline = linego.AddComponent<UIHole>();

            uiline.hole.SizeX = 0.5f;
            uiline.hole.SizeY = 0.5f;
            uiline.hole.Color = Color.white;
            uiline.hole.Inner = 3;
            

            uiline.transform.SetParent(canvas.transform);
            uiline.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

            Selection.activeObject = linego;
        }
       
    }
}