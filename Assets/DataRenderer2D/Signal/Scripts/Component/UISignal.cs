using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace geniikw.DataRenderer2D.Signal
{
    [RequireComponent(typeof(RectTransform))]
    public class UISignal : UIDataMesh, IUnitSize, ISignalData
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

        public SignalData signal;

        protected override void Reset()
        {
            base.Reset();
            signal = SignalData.Default;
        }

        private void Update()
        {
            UpdateGeometry();
        }

        protected override IEnumerable<IMesh> DrawerFactory
        {
            get
            {
                return (new SignalBuilder(this, this)).Draw();
            }
        }

        public Vector2 Size
        {
            get
            {
                return Rect.rect.size;
            }
        }

        public SignalData Signal
        {
            get
            {
                return signal;
            }
        }
    }


#if UNITY_EDITOR
    [CustomEditor(typeof(UISignal))]
    public class UISinEditor : Editor
    {

    }
#endif



}
