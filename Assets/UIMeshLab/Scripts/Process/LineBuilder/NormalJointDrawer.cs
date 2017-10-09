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
            var pos = triple.Position;

            var bd = triple.BackDirection;
            var fd = triple.ForwardDirection;
            //부채꼴에서 양끝점.
            Vector3 p1;
            Vector3 p2;

            if((Vector3.Cross(bd,fd).normalized+_line.normalVector).magnitude < _line.normalVector.magnitude)
            {
                p1 = pos + Vector3.Cross(_line.normalVector, bd).normalized * triple.CurrentWidth;
                p2 = pos + Vector3.Cross(_line.normalVector, fd).normalized * triple.CurrentWidth;
            }
            else
            {
                p1 = pos - Vector3.Cross(_line.normalVector, fd).normalized * triple.CurrentWidth;
                p2 = pos - Vector3.Cross(_line.normalVector, bd).normalized * triple.CurrentWidth;
            }

            var v0 = Vertex.New(pos, Vector2.one / 2f, triple.CurrentColor);
            var v1 = Vertex.New(p1, Vector2.one,  triple.CurrentColor);
            var v2 = Vertex.New(p2, Vector2.one, triple.CurrentColor);

            return MeshData.Triangle(v0,v1,v2);
        }
        
    }
}