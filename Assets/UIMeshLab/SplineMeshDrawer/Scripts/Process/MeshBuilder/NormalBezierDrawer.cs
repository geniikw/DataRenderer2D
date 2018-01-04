using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace geniikw.UIMeshLab
{
    public class NormalBezierDrawer 
    {
        readonly ISpline _line;
        public NormalBezierDrawer(ISpline line)
        {
            _line = line;
        }

        //public MeshData Build(Spline.LinePair pair)
        //{
        //    //var output = MeshData.Void();

        //    //float dt = pair.GetDT(_line.Line.option.DivideLength);
        //    //Vector3 prv1 = Vector3.zero;
        //    //Vector3 prv2 = Vector3.zero;
        //    ///// todo : too complicate need split code for test or something.
        //    //for (float t = pair.start; t < pair.end; t += dt)
        //    //{
        //    //    var ws = Mathf.Lerp(pair.n0.width, pair.n1.width, t);
        //    //    var we = Mathf.Lerp(pair.n0.width, pair.n1.width, t+dt);

        //    //    var ps = Curve.Auto(pair.n0, pair.n1, t);
        //    //    var pe = Curve.Auto(pair.n0, pair.n1, Mathf.Min(pair.end,t + dt));

        //    //    var cs = _line.Line.option.color.Evaluate(pair.sRatio + t * pair.RatioLength);
        //    //    var ce = _line.Line.option.color.Evaluate(pair.sRatio + (t+dt) * pair.RatioLength);

        //    //    var d = pe - ps;
        //    //    var wd = Vector3.Cross(d, _line.Line.option.normalVector).normalized;
        //    //    var wds = t ==0f ? Vector3.Cross(Curve.AutoDirection(pair.n0, pair.n1, 0), _line.Line.option.normalVector).normalized : wd;
        //    //    var wde = Vector3.Cross(pair.GetDirection(1f), _line.Line.option.normalVector).normalized;

        //    //    var uv = new Vector2[] { new Vector2(0, 1), new Vector2(1, 1), Vector2.zero, new Vector2(1, 0) };

        //    //    if (_line is Image && ((Image)_line).sprite != null)
        //    //        uv = ((Image)_line).sprite.uv;

        //    //    var p0 = Vertex.New(t == pair.start ? ps + wds * ws : prv1, uv[0], cs);
        //    //    var p1 = Vertex.New(t == pair.start ? ps - wds * ws : prv2, uv[1], cs);

        //    //    var end = Mathf.Abs(t - pair.end) < dt;

        //    //    var p2 = Vertex.New(end ? pe + wde * we : pe + wd * we, uv[2], ce);
        //    //    var p3 = Vertex.New(end ? pe - wde * we : pe - wd * we, uv[3], ce);

        //    //    prv1 = pe + wd * we;
        //    //    prv2 = pe - wd * we;

        //    //    output += MeshData.Quad(p0, p1, p2, p3);
        //    //}
            
        //    return output;
        //}
    }
}