using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.UIMeshLab {
    public class LineUpdater : MonoBehaviour {

        public Transform p0;
        public Transform p1;
        public WorldLine line;

        Vector3 p0Position;
        Vector3 p1Position;

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {
            
            if(p0Position != p0.position)
            {
                p0Position = p0.position;
                var point = line.line.points[0];
                point.position = transform.InverseTransformPoint(p0Position);
                line.line.points[0] = point;
                line.UpdateGeometry();
            }
            if (p1Position != p1.position)
            {
                p1Position = p1.position;
                var point = line.line.points[1];
                point.position = transform.InverseTransformPoint(p1Position);
                line.line.points[1] = point;
                line.UpdateGeometry();
            }
        }
    }
}