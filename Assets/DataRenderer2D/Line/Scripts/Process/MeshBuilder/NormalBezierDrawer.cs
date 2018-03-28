using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace geniikw.DataRenderer2D
{
    public class NormalBezierDrawer : IBezierBuilder
    {
        readonly ISpline _line;
        
        public NormalBezierDrawer(ISpline line)
        {
            _line = line;
        }
        
        public IEnumerable<IMesh> Build(Spline.LinePair pair)
        {
            var LineData = _line.Line;//hard copy.

            float dt = pair.GetDT(LineData.option.DivideLength);
            Vector3 prv1 = Vector3.zero;
            Vector3 prv2 = Vector3.zero;
            /// todo : too complicate need split code for test or something.
            for (float t = pair.start; t < pair.end; t += dt)
            {
                var ws = Mathf.Lerp(pair.n0.width, pair.n1.width, t);
                var we = Mathf.Lerp(pair.n0.width, pair.n1.width, t+dt);

                var ps = Curve.Auto(pair.n0, pair.n1, t);

                var pe = Curve.Auto(pair.n0, pair.n1, Mathf.Min(pair.end,t + dt));


                var cs = LineData.option.color.Evaluate(pair.sRatio + t * pair.RatioLength);
                var ce = LineData.option.color.Evaluate(pair.sRatio + (t+dt) * pair.RatioLength);

                var d = pe - ps;
                var wd = Vector3.Cross(d, Vector3.back).normalized;
                var wds = t ==0f ? Vector3.Cross(Curve.AutoDirection(pair.n0, pair.n1, 0), Vector3.back).normalized : wd;
                var wde = Vector3.Cross(pair.GetDirection(1f), Vector3.back).normalized;

                var uv = new Vector2[] { new Vector2(0, 1), new Vector2(1, 1), Vector2.zero, new Vector2(1, 0) };

                if (_line is Image && ((Image)_line).sprite != null)
                    uv = ((Image)_line).sprite.uv;

                var p0 = Vertex.New(t == pair.start ? ps + wds * ws : prv1, uv[0], cs);
                var p1 = Vertex.New(t == pair.start ? ps - wds * ws : prv2, uv[1], cs);

                var end = Mathf.Abs(t - pair.end) < dt;

                var p2 = Vertex.New(end ? pe + wde * we : pe + wd * we, uv[2], ce);
                var p3 = Vertex.New(end ? pe - wde * we : pe - wd * we, uv[3], ce);

                prv1 = pe + wd * we;
                prv2 = pe - wd * we;

                yield return new Quad(p0, p1, p2, p3);
            }
            
        }
    }
}