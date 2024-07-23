using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace geniikw.DataRenderer2D.Example
{
    public class DragAddPoint : MonoBehaviour, IDragHandler,IPointerDownHandler
    {

        public bool modeToggle = false;

        public float addDistance = 3f;

        UILine m_line;
        Vector3 m_prevPos;

        private void Start()
        {
            m_line = GetComponent<UILine>();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (modeToggle && Vector3.Distance(m_prevPos, eventData.position) > addDistance)
            {
                m_line.line.Push();
                m_prevPos = eventData.position;
            }
            
            m_line.line.EditPoint(eventData.position);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            m_line.line.Push();
            m_line.line.EditPoint(eventData.position);
        }

        public void Toggle()
        {
            modeToggle = !modeToggle;
        }
    }
}