using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.DataRenderer2D.Example {
    public class ScriptScenario : MonoBehaviour {

        public List<Transform> list = new List<Transform>();
        UILine m_line;
        // Use this for initialization
        public void Start()
        {
            StartCoroutine(StartRoutine());
        }

        IEnumerator StartRoutine() {
            m_line = UILine.CreateLine(transform);
            m_line.transform.SetAsFirstSibling();
            m_line.line.option.divideLength = 100;
            while(m_line.line.Count < list.Count)
                m_line.line.Push();

            while (true)
            {
                float t = 0f;
                m_line.line.option.startRatio = 0;
                m_line.line.option.endRatio = 0;
                while (t < 1f)
                {
                    t += Time.deltaTime;
                    m_line.line.option.endRatio = t;
                    yield return null;
                }
                t = 0f;
                while (t < 1f)
                {
                    t += Time.deltaTime;
                    m_line.line.option.startRatio = t;
                    yield return null;
                }
            }
        }

        // Update is called once per frame
        void Update() {
            for (int i = 0; i < list.Count; i++)
            {
                m_line.line.EditPoint(i, list[i].position, 2f);
            }
            
        }
    }
}