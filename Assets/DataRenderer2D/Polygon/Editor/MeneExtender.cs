using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace geniikw.DataRenderer2D.Polygon.Editors
{
    public static class MeneExtender
    {
        [MenuItem("GameObject/2D Object/DataRenderer/WorldPolygon")]
        public static void WorldPolygon()
        {
            var go = new GameObject("WorldPolygon");
            go.transform.SetParent(Selection.activeTransform);
            var p = go.AddComponent<WorldPolygon>();
            go.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Diffuse"));
            p.data = Defulat();

        }
        [MenuItem("GameObject/2D Object/DataRenderer/UIPolygon")]
        public static void UIPolygon()
        {
            var parent = Selection.activeTransform;
            if (parent == null || !CheckCanvas(parent))
            {
                Debug.LogError("there is no canvas");
                return;
            }

            var go = new GameObject("UIPolygon");
            go.transform.SetParent(Selection.activeTransform);
            var p = go.AddComponent<UIPolygon>();
            p.rectTransform.anchoredPosition = Vector3.zero;
            p.data = Defulat();
        }

        public static bool CheckCanvas(Transform t)
        {
            if(t.parent != null)
                return CheckCanvas(t.parent);

            if (t.GetComponent<Canvas>() == null)
                return false;
            return true;
        }

        private static PolygonData Defulat()
        {
            return new PolygonData
            {
                curve = AnimationCurve.Linear(0, 1, 1, 1),
                outerColor = new Gradient(),
                endAngle = 1f,
                sinCft = 1f,
                cosCft = 1f,
                centerColor = Color.white,
                count = 3,
                innerRatio = 0f,
                startAngle = 0f,
                type = PolygonType.ZigZag
            };
        }
    }
}