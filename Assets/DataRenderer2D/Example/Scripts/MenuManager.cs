using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.DataRenderer2D.Example {
    public class MenuManager : MonoBehaviour {

        public ActivatorList[] list;

        public float time = 1f;

        int activeIdx = -1;

        public void Awake()
        {
            list = transform.GetComponentsInChildren<ActivatorList>();
        }

        public void Toggle(int idx)
        {
            if(activeIdx >= 0)
            {
                SetDeactivate();
                return;
            }

            for (int i = 0; i < list.Length; i++)
            {
                if (idx == i)
                    list[i].Activate(time);
                else
                    list[i].Hide(time);
            }
            activeIdx = idx;
        }

        public void SetDeactivate()
        {
            activeIdx = -1;
            for (int i = 0; i < list.Length; i++)
            {
                list[i].Deactivate(time);
            }
        }
    }
}