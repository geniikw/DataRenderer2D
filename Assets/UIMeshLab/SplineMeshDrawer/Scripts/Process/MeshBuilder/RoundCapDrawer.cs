using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace geniikw.UIMeshLab
{

    public class RoundCapDrawer : ICapBuilder
    {
        ISpline _line;
        public RoundCapDrawer(ISpline target)
        {
            _line = target;
        }
        
        public void Build(Spline.LinePair pair, bool isEnd)
        {
            //var output = new MeshQueue();

            var normal = _line.Line.option.normalVector;
            var divideAngle = _line.Line.option.DivideAngle;

            var t = isEnd ? 1f : 0f;

            var color = _line.Line.option.color.Evaluate(isEnd ? _line.Line.option.endRatio : _line.Line.option.startRatio);
            var position = pair.GetPoisition(t);

            var radian = pair.GetWidth(t);

            var direction = pair.GetDirection(isEnd ? 1 : 0) *(isEnd?-1:1);
      
            var wv = Vector3.Cross(direction, normal).normalized * radian;

            var dc = Mathf.Max(1, Mathf.Floor(180f / divideAngle));
            var da = 180f / dc;
            var rot = Quaternion.Euler(-normal * da);


            var uv = new Vector2[] { new Vector2(0, 1), new Vector2(1, 1), Vector2.zero, new Vector2(1, 0) };
            if (_line is Image && ((Image)_line).sprite != null)
                uv = ((Image)_line).sprite.uv;
            
            var center = (uv[0] + uv[1] + uv[2] + uv[3]) / 4f;

            for (float a = 0f; a < 179f; a += da)
            {
                var v0 = Vertex.New(position, center, color);
                var v1 = Vertex.New(position + wv, (!isEnd ? a > 90 : a < 90) ? uv[1]:uv[0], color);
                var v2 = Vertex.New(position + rot * wv, (!isEnd ? a > 90 : a < 90) ? uv[3]:uv[2], color);
               // output += MeshData.Triangle(v0, v1, v2);

                wv = rot * wv;
            }

            //return output;
        }       
    }
}