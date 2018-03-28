using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace geniikw.DataRenderer2D.Example
{
    [RequireComponent(typeof(RectTransform))]
    public class DragMove : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        public RectTransform Rect { get { return GetComponent<RectTransform>(); } }
        public RectTransform ClampArea;

        public void OnDrag(PointerEventData eventData)
        {
            var pos = ClampArea.InverseTransformPoint( eventData.position);

            var xmin = (ClampArea.rect.xMin + Rect.rect.width / 2f) ;
            var xmax = (ClampArea.rect.xMax - Rect.rect.width / 2f) ;
            var ymin = (ClampArea.rect.yMin + Rect.rect.height / 2f);
            var ymax = (ClampArea.rect.yMax - Rect.rect.height / 2f);

            pos.x = Mathf.Clamp(pos.x, xmin, xmax);
            pos.y = Mathf.Clamp(pos.y, ymin, ymax);
            transform.localPosition = pos;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            
        }

    }
}