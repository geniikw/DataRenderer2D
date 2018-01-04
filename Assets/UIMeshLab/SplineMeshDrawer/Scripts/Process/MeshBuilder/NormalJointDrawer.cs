using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace geniikw.UIMeshLab
{
    public class NormalJointDrawer : IJointBuilder
    {
        readonly ISpline _line;
        public NormalJointDrawer(ISpline target)
        {
            _line = target;
        }
        
        public void Build(Spline.Triple triple)
        {
            ///부채꼴에서 가운데점
            var p0 = triple.Position;

            var bd = triple.BackDirection;
            var fd = triple.ForwardDirection;
            //부채꼴에서 양끝점.
            Vector3 p1;
            Vector3 p2;
            var nv = _line.Line.option.normalVector;

            var uv = new Vector2[] { new Vector2(0, 1), new Vector2(1, 1), Vector2.zero, new Vector2(1, 0) };
            if (_line is Image && ((Image)_line).sprite != null)
                uv = ((Image)_line).sprite.uv;

            var center = (uv[0] + uv[1] + uv[2] + uv[3]) / 4f;
            Vector2[] uvUse = new Vector2[2];
            if ((Vector3.Cross(bd,fd).normalized+ nv).magnitude < nv.magnitude)
            {
                p1 = p0 + Vector3.Cross(nv, bd).normalized * triple.CurrentWidth;
                p2 = p0 + Vector3.Cross(nv, fd).normalized * triple.CurrentWidth;
                uvUse = new Vector2[] { uv[1], uv[3] };
            }
            else
            {
                p1 = p0 - Vector3.Cross(nv, fd).normalized * triple.CurrentWidth;
                p2 = p0 - Vector3.Cross(nv, bd).normalized * triple.CurrentWidth;
                uvUse = new Vector2[] { uv[0], uv[2] };
            }

            var angle = Vector3.Angle( p1 - p0, p2 - p0);
            var dc = Mathf.Max(1, Mathf.Floor( angle / _line.Line.option.DivideAngle));
            var da =  angle/ dc;
//            var mesh = MeshData.Void();
            var rot = Quaternion.Euler(-nv * da);
            var d = p1 - p0;
                        
            for (float a = 0f; a < angle; a+=da)
            {
                var v0 = Vertex.New(p0, center, triple.CurrentColor);
                var v1 = Vertex.New(p0+d,uvUse[0], triple.CurrentColor);
                var v2 = Vertex.New(p0+rot*d, uvUse[1], triple.CurrentColor);
     //           mesh += MeshData.Triangle(v0, v1, v2);

                d = rot * d;
            }
            //return null;
    //        return mesh;
        }
        
    }
}