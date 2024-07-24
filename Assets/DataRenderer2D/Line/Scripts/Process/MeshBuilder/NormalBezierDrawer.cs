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
                if(t + dt > pair.end)
                    dt = pair.end - t;

                var ws = Mathf.Lerp(pair.n0.width, pair.n1.width, t);
                var we = Mathf.Lerp(pair.n0.width, pair.n1.width, t+dt);

                var ps = Curve.Auto(pair.n0, pair.n1, t);
                var pe = Curve.Auto(pair.n0, pair.n1, Mathf.Min(pair.end,t + dt));

                var angle1 = Mathf.Lerp(pair.n0.forwardAngle, pair.n1.backAngle, t);
                var angle2 = Mathf.Lerp(pair.n0.forwardAngle, pair.n1.backAngle, t + dt);


                var cs = LineData.option.color.Evaluate(pair.sRatio + t * pair.RatioLength);
                var ce = LineData.option.color.Evaluate(pair.sRatio + (t+dt) * pair.RatioLength);

                var d = pe - ps;
                var wd = Vector3.Cross(d, Vector3.back).normalized;
                
                wd = Quaternion.Euler(0, 0, angle2) * wd;
                
                
                var startStartWidth =  Vector3.Cross(pair.GetDirection(0f), Vector3.back).normalized ;
                var endEndWidth = Vector3.Cross(pair.GetDirection(1f), Vector3.back).normalized;
  

                startStartWidth = Quaternion.Euler(0, 0, angle1) * startStartWidth;
                endEndWidth = Quaternion.Euler(0, 0, angle2) * endEndWidth;


                var uv = new Vector2[] { new(0, 1), new(1, 1), new(0, 0), new(1, 0) };

                if (_line is Image && ((Image)_line).sprite != null)
                    uv = ((Image)_line).sprite.uv;

                var p0 = Vertex.New(t == pair.start ? ps + startStartWidth * ws : prv1, uv[0], cs);
                var p1 = Vertex.New(t == pair.start ? ps - startStartWidth * ws : prv2, uv[1], cs);

                var end = t + dt == pair.end;

                var p2 = Vertex.New(end ? pe + endEndWidth * we : pe + wd * we, uv[2], ce);
                var p3 = Vertex.New(end ? pe - endEndWidth * we : pe - wd * we, uv[3], ce);

                prv1 = pe + wd * we;
                prv2 = pe - wd * we;

                yield return new Quad(p0, p1, p2, p3);
            }
            
        }
    }
}