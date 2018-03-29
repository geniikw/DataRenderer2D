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
        public float Speed = 1f;

        SignalBuilder m_meshBuilder = null;

        public void Update()
        {
            if(Application.isPlaying)
                signal.t += Time.deltaTime * Speed;
            UpdateGeometry();
        }

        protected new void Reset()
        {
            signal = SignalData.Default;
        }

        protected override IEnumerable<IMesh> DrawerFactory
        {
            get
            {
                return (m_meshBuilder ?? (m_meshBuilder =new SignalBuilder(this, this))).Draw();
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

        //example.
        public void AmpHandler(float amf)
        {
            signal.up.amplify = amf * 10;
            signal.down.amplify = amf * 10;
        }

        public void UpUseHandler(bool t)
        {
            signal.up.use = t;
        }
        
        public void DownUseHandler(bool t)
        {
            signal.down.use = t;
        }

        public void UpFrequencyHandler(float v)
        {
            signal.up.frequncy = v*20;
        }

    }


#if UNITY_EDITOR
    [CustomEditor(typeof(UISignal))]
    public class UISinEditor : Editor
    {

    }
#endif



}
