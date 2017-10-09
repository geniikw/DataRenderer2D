using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.UIMeshLab
{
    public class NormalBezierDrawer : IBezierDrawer
    {
        readonly Line _line;
        public NormalBezierDrawer(Line line)
        {
            _line = line;
        }

        public MeshData Build(Line.LinePair pair)
        {
            var output = MeshData.Void();

            float dt = pair.GetDT(_line.divideLength);
            Vector3 prv1 = Vector3.zero;
            Vector3 prv2 = Vector3.zero;

            for (float t = pair.start; t < pair.end; t += dt)
            {
                var ws = Mathf.Lerp(pair.n0.width, pair.n1.width, t);
                var we = Mathf.Lerp(pair.n0.width, pair.n1.width, t+dt);

                var ps = Curve.Auto(pair.n0, pair.n1, t);
                var pe = Curve.Auto(pair.n0, pair.n1, t+dt);

                var cs = Color.Lerp(pair.n0.color, pair.n1.color, t);
                var ce = Color.Lerp(pair.n0.color, pair.n1.color, t+dt);
                
                var d = pe - ps;
                var wd = Vector3.Cross(d, _line.normalVector).normalized;
                var wds = t ==0f ? Vector3.Cross(Curve.AutoDirection(pair.n0, pair.n1, 0), _line.normalVector).normalized : wd;
                var wde = Vector3.Cross(Curve.AutoDirection(pair.n0, pair.n1, 1), _line.normalVector).normalized;
                
                var p0 = Vertex.New(t == pair.start ? ps + wds * ws : prv1, _line.UVRotate(new Vector2(0, 1)), cs);
                var p1 = Vertex.New(t == pair.start ? ps - wds * ws : prv2, _line.UVRotate(new Vector2(1, 1)), cs);

                var end = Mathf.Abs(t - pair.end) < dt;

                var p2 = Vertex.New(end ? pe + wde * we : pe + wd * we, _line.UVRotate(new Vector2(0, 0)), ce);
                var p3 = Vertex.New(end ? pe - wde * we : pe - wd * we, _line.UVRotate(new Vector2(1, 0)), ce);

                prv1 = pe + wd * we;
                prv2 = pe - wd * we;

                output += MeshData.Quad(p0, p1, p2, p3);
            }
            
            return output;
        }
    }
}