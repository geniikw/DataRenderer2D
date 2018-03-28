using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.DataRenderer2D.Example
{
    public class FollowLine : MonoBehaviour
    {
        public GizmoLine line;
        public float speed = 0.01f;
        
        float t = 0;

        // Use this for initialization
        IEnumerator Start()
        {
            while (true)
            {
                while (t < 1)
                {
                    if (line == null)
                        yield break;
                    //라인 길이에 상관없이 같은 속도로 움직이게 한다.
                    t += Time.deltaTime* speed / line.line.Length;
                    transform.position = line.GetPosition(t);
                    transform.LookAt(transform.position + line.GetDirection(t));
                    yield return null;
                }
                t = 0;
            }
        }

    
        
    }
}