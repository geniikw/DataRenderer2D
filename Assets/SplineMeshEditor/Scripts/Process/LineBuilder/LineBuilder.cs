using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace geniikw.UIMeshLab
{
    /// <summary>
    /// converter (Line -> MeshData)
    /// </summary>
    public class LineBuilder 
    {
        public IBezierBuilder bezierDrawer;
        public IJointBuilder jointDrawer;
        public ICapBuilder capDrawer;

        public LineBuilder(){ }

        public MeshData Build(ISpline target)
        {
            var output = MeshData.Void();

            if (target.Line.endRatio - target.Line.startRatio <= 0)
                return output;

            var pairList = target.Line.PairList.ToList();
            
            if (target.Line.mode == Spline.Mode.RoundEdge)
            {
                output += capDrawer.Build(pairList.First(), false);
            }

            foreach (var triple in target.Line.TripleList)
            {
                output += jointDrawer.Build(triple);
            }

            foreach (var pair in pairList)
            {
                output += bezierDrawer.Build(pair);
            }

            if(target.Line.mode == Spline.Mode.RoundEdge)
            {
                 output += capDrawer.Build(pairList.Last(),true);
            }

            return output;
        }

        public class Factory
        {
            public static LineBuilder Normal(ISpline line)
            {
                var builder = new LineBuilder()
                {
                    bezierDrawer = new NormalBezierDrawer(line),
                    jointDrawer = new NormalJointDrawer(line),
                    capDrawer = new RoundCapDrawer(line)
                };
 
                return builder;
            }
        }

    }
}