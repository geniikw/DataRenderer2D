using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.UIMeshLab
{
    public class NormalJointDrawer : IJointDrawer
    {
        Line _line;
        public NormalJointDrawer(Line line)
        {
            _line = line;
        }

        public MeshData Build(Line.Triple triple)
        {
            ///부채꼴에서 가운데점
            var p0 = triple.Position;

            var bd = triple.BackDirection;
            var fd = triple.ForwardDirection;
            //부채꼴에서 양끝점.
            Vector3 p1;
            Vector3 p2;

            if((Vector3.Cross(bd,fd).normalized+_line.normalVector).magnitude < _line.normalVector.magnitude)
            {
                p1 = p0 + Vector3.Cross(_line.normalVector, bd).normalized * triple.CurrentWidth;
                p2 = p0 + Vector3.Cross(_line.normalVector, fd).normalized * triple.CurrentWidth;
            }
            else
            {
                p1 = p0 - Vector3.Cross(_line.normalVector, fd).normalized * triple.CurrentWidth;
                p2 = p0 - Vector3.Cross(_line.normalVector, bd).normalized * triple.CurrentWidth;
            }

            var angle = Vector3.Angle( p1 - p0, p2 - p0);
            var dc = Mathf.Max(1, Mathf.Floor( angle / _line.divideAngle));
            var da =  angle/ dc;
            var mesh = MeshData.Void();
            var rot = Quaternion.Euler(-_line.normalVector * da);
            var d = p1 - p0;

            for (float a = 0f; a < angle; a+=da)
            {
                var v0 = Vertex.New(p0, Vector2.one / 2f, triple.CurrentColor);
                var v1 = Vertex.New(p0+d, Vector2.one, triple.CurrentColor);
                var v2 = Vertex.New(p0+rot*d, Vector2.one, triple.CurrentColor);
                mesh += MeshData.Triangle(v0, v1, v2);

                d = rot * d;
            }
            
            return mesh;
        }
        
    }
}