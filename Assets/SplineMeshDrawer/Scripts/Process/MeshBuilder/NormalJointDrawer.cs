using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.UIMeshLab
{
    public class NormalJointDrawer : IJointBuilder
    {
        readonly ISpline _line;
        public NormalJointDrawer(ISpline target)
        {
            _line = target;
        }
        
        public MeshData Build(Spline.Triple triple)
        {
            ///부채꼴에서 가운데점
            var p0 = triple.Position;

            var bd = triple.BackDirection;
            var fd = triple.ForwardDirection;
            //부채꼴에서 양끝점.
            Vector3 p1;
            Vector3 p2;
            var nv = _line.Line.option.normalVector;
    
            if((Vector3.Cross(bd,fd).normalized+ nv).magnitude < nv.magnitude)
            {
                p1 = p0 + Vector3.Cross(nv, bd).normalized * triple.CurrentWidth;
                p2 = p0 + Vector3.Cross(nv, fd).normalized * triple.CurrentWidth;
            }
            else
            {
                p1 = p0 - Vector3.Cross(nv, fd).normalized * triple.CurrentWidth;
                p2 = p0 - Vector3.Cross(nv, bd).normalized * triple.CurrentWidth;
            }

            var angle = Vector3.Angle( p1 - p0, p2 - p0);
            var dc = Mathf.Max(1, Mathf.Floor( angle / _line.Line.option.DivideAngle));
            var da =  angle/ dc;
            var mesh = MeshData.Void();
            var rot = Quaternion.Euler(-nv * da);
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