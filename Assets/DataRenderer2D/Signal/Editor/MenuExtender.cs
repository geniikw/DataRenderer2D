using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace geniikw.DataRenderer2D.Signal.Editors
{
    public static class MenuExtender
    {
        //[ContextMenu()]
        [MenuItem("GameObject/2D Object/DataRenderer/UISignal")]
        public static void CreateUILine()
        {
            var canvas = Object.FindObjectOfType<Canvas>();
            if (canvas == null)
            {
                var canvasGo = new GameObject("Canvas");
                canvas = canvasGo.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            }

            var linego = new GameObject("UISignal");
            var uiline = linego.AddComponent<UISignal>();
            uiline.signal = SignalData.Default;

            uiline.transform.SetParent(canvas.transform);
            uiline.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

            Selection.activeObject = linego;
        }
    }
}