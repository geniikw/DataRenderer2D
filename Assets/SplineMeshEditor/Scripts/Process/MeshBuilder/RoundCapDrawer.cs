using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace geniikw.UIMeshLab
{

    public class RoundCapDrawer : ICapBuilder
    {
        ISpline _line;
        public RoundCapDrawer(ISpline target)
        {
            _line = target;
        }
        
        public MeshData Build(Spline.LinePair pair, bool isEnd)
        {
            var output = MeshData.Void();

            var normal = _line.Line.normalVector;
            var divideAngle = _line.Line.divideAngle;

            var t = isEnd ? 1f : 0f;

            var color = _line.Line.color.Evaluate(isEnd ? _line.Line.endRatio : _line.Line.startRatio);
            var position = pair.GetPoisition(t);

            var radian = pair.GetWidth(t);

            var direction = pair.GetDirection(isEnd ? 1 : 0) *(isEnd?-1:1);
      
            var wv = Vector3.Cross(direction, normal).normalized * radian;

            var dc = Mathf.Max(1, Mathf.Floor(180f / divideAngle));
            var da = 180f / dc;
            var rot = Quaternion.Euler(-normal * da);

            for (float a = 0f; a < 180f; a += da)
            {
                var v0 = Vertex.New(position, Vector2.one / 2f, color);
                var v1 = Vertex.New(position + wv, Vector2.one, color);
                var v2 = Vertex.New(position + rot * wv, Vector2.one, color);
                output += MeshData.Triangle(v0, v1, v2);

                wv = rot * wv;
            }

            return output;
        }       
    }
}