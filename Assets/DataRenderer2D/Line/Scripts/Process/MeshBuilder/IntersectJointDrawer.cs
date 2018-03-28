using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace geniikw.DataRenderer2D
{
    public class IntersectJointDrawer : IJointBuilder
    {
        readonly ISpline _line;
        public IntersectJointDrawer(ISpline target)
        {
            _line = target;
        }
        
        public IEnumerable<IMesh> Build(Spline.Triple triple)
        {
            ///부채꼴에서 가운데점
            var p0 = triple.Position;

            var bd = triple.BackDirection;
            var fd = triple.ForwardDirection;
            //부채꼴에서 양끝점.
            Vector3 p1;
            Vector3 p2;
            var nv = Vector3.back;

            if ((Vector3.Cross(bd, fd).normalized + nv).magnitude < nv.magnitude)
            {
                p1 = p0 + Vector3.Cross(nv, bd).normalized * triple.CurrentWidth;
                p2 = p0 + Vector3.Cross(nv, fd).normalized * triple.CurrentWidth;
            }
            else
            {
                p1 = p0 - Vector3.Cross(nv, fd).normalized * triple.CurrentWidth;
                p2 = p0 - Vector3.Cross(nv, bd).normalized * triple.CurrentWidth;
            }
           

            var v1 = Vector3.Cross(p1-p0, Vector3.back);
            var v2 = Vector3.Cross(p2-p0, Vector3.back);

            Vector3 cp;
            ClosestPointsOnTwoLines(out cp, out cp, p1, v1, p2, v2);
            
            var uv = new Vector2[] { new Vector2(0, 1), new Vector2(1, 1), Vector2.zero, new Vector2(1, 0) };
            if (_line is Image && ((Image)_line).sprite != null)
                uv = ((Image)_line).sprite.uv;
            var center = (uv[0] + uv[1] + uv[2] + uv[3]) / 4f;

            var vertice = new Vertex[] {
                Vertex.New(p0, center, triple.CurrentColor),
                Vertex.New(p1, uv[1], triple.CurrentColor),
                Vertex.New(p2, uv[1], triple.CurrentColor),
                Vertex.New(cp, uv[3], triple.CurrentColor)
             };

            yield return new Triangle(vertice[0], vertice[1], vertice[3]);
            yield return new Triangle(vertice[0], vertice[3], vertice[2]);

        }


        private bool ClosestPointsOnTwoLines(out Vector3 closestPointLine1, out Vector3 closestPointLine2, Vector3 linePoint1, Vector3 lineVec1, Vector3 linePoint2, Vector3 lineVec2)
        {
            closestPointLine1 = Vector3.zero;
            closestPointLine2 = Vector3.zero;

            float a = Vector3.Dot(lineVec1, lineVec1);
            float b = Vector3.Dot(lineVec1, lineVec2);
            float e = Vector3.Dot(lineVec2, lineVec2);

            float d = a * e - b * b;

            //lines are not parallel
            if (d != 0.0f)
            {

                Vector3 r = linePoint1 - linePoint2;
                float c = Vector3.Dot(lineVec1, r);
                float f = Vector3.Dot(lineVec2, r);

                float s = (b * f - c * e) / d;
                float t = (a * f - c * b) / d;

                closestPointLine1 = linePoint1 + lineVec1 * s;
                closestPointLine2 = linePoint2 + lineVec2 * t;

                return true;
            }

            else
            {
                return false;
            }
        }

    }
}