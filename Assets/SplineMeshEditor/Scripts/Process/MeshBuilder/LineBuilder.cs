using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace geniikw.UIMeshLab
{
    /// <summary>
    /// converter (Line -> MeshData)
    /// </summary>
    /// 
    public interface IMeshBuilder
    {
        MeshData Build();
    }
    public class LineBuilder : IMeshBuilder
    {
        readonly IBezierBuilder _bezierDrawer;
        readonly IJointBuilder _jointDrawer;
        readonly ICapBuilder _capDrawer;
        readonly ISpline _line;

        public LineBuilder(IBezierBuilder b, IJointBuilder j , ICapBuilder c, ISpline line){
            _bezierDrawer = b;
            _jointDrawer = j;
            _capDrawer = c;
            _line = line;
        }

        public MeshData Build()
        {
            var output = MeshData.Void();

            if (_line.Line.endRatio - _line.Line.startRatio <= 0)
                return output;

            var pairList = _line.Line.PairList.ToList();
            
            if (_line.Line.mode == Spline.Mode.RoundEdge)
            {
                output += _capDrawer.Build(pairList.First(), false);
            }

            foreach (var triple in _line.Line.TripleList)
            {
                output += _jointDrawer.Build(triple);
            }

            foreach (var pair in pairList)
            {
                output += _bezierDrawer.Build(pair);
            }

            if(_line.Line.mode == Spline.Mode.RoundEdge)
            {
                 output += _capDrawer.Build(pairList.Last(),true);
            }

            return output;
        }

        public class Factory
        {
            public static IMeshBuilder Normal(ISpline line)
            {
                var builder = new LineBuilder
                (
                    new NormalBezierDrawer(line),
                    new NormalJointDrawer(line),
                    new RoundCapDrawer(line),
                    line
                );
 
                return builder;
            }
        }

    }
}