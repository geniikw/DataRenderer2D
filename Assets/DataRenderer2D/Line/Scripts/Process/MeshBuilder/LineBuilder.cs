using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.DataRenderer2D
{
    /// <summary>
    /// converter (Line -> MeshData)
    /// </summary>
    /// 
   
    public class LineBuilder : IMeshDrawer
    {
        readonly IBezierBuilder _bezierDrawer;
        readonly IJointBuilder _jointDrawer;
        readonly IJointBuilder _jointIntersectDrawer;
        readonly ICapBuilder _capDrawer;
        readonly ISpline _line;

        public LineBuilder(IBezierBuilder b, IJointBuilder j,IJointBuilder js, ICapBuilder c, ISpline line){
            _bezierDrawer = b;
            _jointDrawer = j;
            _capDrawer = c;
            _line = line;
            _jointIntersectDrawer = js;
        }

        public IEnumerable<IMesh> Draw()
        {
            if (_line.Line.option.endRatio - _line.Line.option.startRatio <= 0)
                yield break;
            
            //todo : merge Pairlist with TripleList to one iteration.
            var ff = true;
            Spline.LinePair last = new Spline.LinePair();
            foreach(var pair in _line.Line.TargetPairList)
            {
                if (ff)
                {
                    ff = false;
                    if (_line.Line.option.mode == LineOption.Mode.RoundEdge)// && pairList.Count() > 0)
                    {
                        foreach (var mesh in _capDrawer.Build(pair, false))
                        {
                            yield return mesh;
                        }
                    }
                }

                
                foreach (var mesh in _bezierDrawer.Build(pair))
                {
                    yield return mesh;
                }
                last = pair;
            }

            foreach (var triple in _line.Line.TripleList)
            {
                var joint = _line.Line.option.jointOption == LineOption.LineJointOption.round ? _jointDrawer : _jointIntersectDrawer;

                foreach (var mesh in joint.Build(triple))
                {
                    yield return mesh;
                }
            }

            if(_line.Line.option.mode == LineOption.Mode.RoundEdge)
            {
                foreach (var mesh in _capDrawer.Build(last, true))
                {
                    yield return mesh;
                }
            }
        }

        public class Factory
        {
            public static IMeshDrawer Normal(ISpline line, Transform trans)
            {
                var builder = new LineBuilder
                (
                    new NormalBezierDrawer(line),
                    new NormalJointDrawer(line),
                    new IntersectJointDrawer(line),
                    new RoundCapDrawer(line),
                    line
                );
 
                return builder;
            }
        }

    }
}