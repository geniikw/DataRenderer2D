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
                var wd = Vector3.Cross(d, _line.CrossVectorForWidthDirectionVector).normalized;

                var p0 = Vertex.New(prv1 == Vector3.zero ? ps + wd * ws : prv1, new Vector2(0, 1), cs);
                var p1 = Vertex.New(prv2 == Vector3.zero ? ps - wd * ws : prv2, new Vector2(1, 1), cs);
                var p2 = Vertex.New(pe + wd * we, new Vector2(0, 0), ce);
                var p3 = Vertex.New(pe - wd * we, new Vector2(1, 0), ce);

                prv1 = pe + wd * we;
                prv2 = pe - wd * we;

                output =  output + MeshData.Quad(p0, p1, p2, p3);
            }

            return output;
        }
    }
}