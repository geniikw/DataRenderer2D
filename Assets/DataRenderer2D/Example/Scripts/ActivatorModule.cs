using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace geniikw.DataRenderer2D.Example
{
    public class ActivatorModule : MonoBehaviour
    {
        public enum State
        {
            Activate,
            Deactivate,
            Hide
        }
        
        public Vector3 actPosition;
        public Vector3 actScale;
        public Vector3 deactPosition;
        public Vector3 deactScale;
        public Color actColor;
        public Color deactColor;
                
        public Vector3 hidePosition;

        public void Start()
        {
            Deactivate(0);

        }

        public void Activate(float time)
        {
            this.TweenScale(actScale, time);
            this.TweenMove(actPosition, time);
            var image = GetComponent<Image>();
            if (image != null)
            {
                image.TweenColor(actColor, time);
            }

        }

        public void Deactivate(float time)
        {
            this.TweenScale(deactScale, time);
            this.TweenMove(deactPosition, time);
            var image = GetComponent<Image>();
            if (image != null)
            {
                image.TweenColor(deactColor, time);
            }
        }

        public void Hide(float time)
        {
            this.TweenMove(hidePosition, time);
        }
        
        public void SetDeactive()
        {
            deactPosition = transform.position;
            deactScale = transform.localScale;
        }

        public void SetActive()
        {
            actPosition = transform.position;
            actScale = transform.localScale;
        }

        public void SetHidePosition()
        {
            hidePosition = transform.position;
        }
        

    }
    
#if UNITY_EDITOR
[CustomEditor(typeof(ActivatorModule))]
    public class ActilistEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("set act"))
            {
                ((ActivatorModule)target).SetActive();
            }
            if (GUILayout.Button("set deact"))
            {
                ((ActivatorModule)target).SetDeactive();
            }
            if (GUILayout.Button("setHide"))
            {
                ((ActivatorModule)target).SetHidePosition();
            }
            if (GUILayout.Button("act"))
            {
                ((ActivatorModule)target).Activate(0);
            }
            if (GUILayout.Button("deact"))
            {
                ((ActivatorModule)target).Deactivate(0);
            }
            if (GUILayout.Button("hide"))
            {
                ((ActivatorModule)target).Hide(0);
            }

        }
    }

#endif

}