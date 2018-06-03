using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace geniikw.DataRenderer2D.Example
{
    public static class MonobehaviourExtension
    {
        public static Coroutine TweenMove(this MonoBehaviour owner, Vector3 end, float t)
        {
            return owner.StartCoroutine(MoveRoutine(owner.transform, end, t));
        }

        public static Coroutine TweenScale(this MonoBehaviour owner, Vector3 end, float t)
        {
            return owner.StartCoroutine(ScalerRoutine(owner.transform, end, t));
        }

        public static Coroutine TweenColor(this MonoBehaviour owner, Color end, float t)
        {
            var image = owner.GetComponent<Image>();
            if (image == null)
                return null ;
            return owner.StartCoroutine(ImageColorTween(image, end, t));
        }

        static IEnumerator ScalerRoutine(Transform owner, Vector3 end, float time)
        {
            float t = 0f;
            Vector3 start = owner.localScale;
            while (t < 1f)
            {
                t += Time.deltaTime / time;
                owner.localScale = Vector3.Lerp(start, end, t);
                yield return null;
            }
        }

        static IEnumerator MoveRoutine(Transform owner, Vector3 end, float time)
        {
            float t = 0f;
            Vector3 start = owner.position;
            while (t < 1f)
            {
                t += Time.deltaTime / time;
                owner.position = Vector3.Lerp(start, end, t);
                yield return null;
            }
        }

        static IEnumerator ImageColorTween(Image target, Color end, float time)
        {
            float t = 0f;
            var start = target.color;
            while (t < 1f)
            {
                t += Time.deltaTime / time;
                target.color = Color.Lerp(start, end, t);
                yield return null;
            }
        }



    }
}