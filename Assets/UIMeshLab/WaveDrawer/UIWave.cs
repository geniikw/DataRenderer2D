using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace geniikw.UIMeshLab.Sin
{
    [RequireComponent(typeof(RectTransform))]
    public class UIWave : UIDataMesh
    {
        RectTransform m_rect = null;
        RectTransform Rect
        {
            get
            {
                if (m_rect == null)
                    m_rect = GetComponent<RectTransform>();

                return m_rect;
            }
        }

        public float divide = 1f;
        public float amp = 1f;
        public float frq = 1f;
        public float t = 0f;

        private const float tFactor = 100f;

        public Gradient WaveColor = new Gradient();

        protected override IEnumerable<IMesh> DrawerFactory
        {
            get
            {
                var size = Rect.sizeDelta;

                var width = size.x / divide;

                var v0 = new Vertex(new Vector2(-size.x / 2f , -size.y / 2f ), Vector2.zero, Color.white);
                var v1 = new Vertex(new Vector2(-size.x / 2f , size.y / 2f ), Vector2.zero, Color.white);
                var v2 = new Vertex(new Vector2(-size.x / 2f + width, size.y / 2f), Vector2.zero, Color.white);
                var v3 = new Vertex(new Vector2(-size.x / 2f + width , -size.y / 2f), Vector2.zero, Color.white);
                               

                for (var i = 0f; i < 1f; i+= 1f/divide)
                {
                    var ni = Mathf.Min(1f, i + 1f / divide);

                    v0.position.x = -size.x / 2f + i * size.x;
                    v1.position.x = -size.x / 2f + i * size.x;
                    v2.position.x = -size.x / 2f + ni * size.x;
                    v3.position.x = -size.x / 2f + ni* size.x;

                    v1.position.y = size.y / 2f + amp* Mathf.Sin((i+t/ tFactor) * frq);
                    v2.position.y = size.y / 2f + amp *Mathf.Sin((ni + t/ tFactor) * frq);

                    v0.color = WaveColor.Evaluate(i);
                    v1.color = WaveColor.Evaluate(i);
                    v2.color = WaveColor.Evaluate(ni);
                    v3.color = WaveColor.Evaluate(ni);
                    
                    yield return new Quad(v0, v1, v2, v3);
                }
            }
        }
        
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(geniikw.UIMeshLab.Sin.UIWave))]
public class UISinEditor : Editor
{

}
#endif