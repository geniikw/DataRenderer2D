using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace geniikw.UIMeshLab
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
                    t += Time.deltaTime* speed;
                    transform.position = line.GetPosition(t);
                    transform.rotation = Quaternion.LookRotation(transform.position + line.GetDirection(t));
                    yield return null;
                }
                while(t > 0)
                {
                    t -= Time.deltaTime* speed;
                    transform.position = line.GetPosition(t);
                    transform.rotation = Quaternion.LookRotation(transform.position + line.GetDirection(t));
                    yield return null;
                }


            }



        }
        
    }
}