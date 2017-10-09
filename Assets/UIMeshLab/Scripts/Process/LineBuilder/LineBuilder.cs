using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.UIMeshLab
{
    public class LineBuilder 
    {
        Line _line;

        public IBezierDrawer bezierDrawer;
        public IJointDrawer jointDrawer;
        public ICapDrawer capDrawer;

        public bool DrawJoint = true;
        public bool DrawLine = true;

        public LineBuilder(Line line)
        {
            _line = line;
        }

        public MeshData Build()
        {
            var output = MeshData.Void();
            if (DrawJoint)
            {
                foreach (var triple in _line.TripleList)
                {
                    output += jointDrawer.Build(triple);
                }
            }

            if (DrawLine)
            {
                foreach (var pair in _line.PairList)
                {
                    output += bezierDrawer.Build(pair);
                }
            }

            return output;
        }

        public class Factory
        {
            public static LineBuilder Normal(Line line)
            {
                var builder = new LineBuilder(line)
                {
                    bezierDrawer = new NormalBezierDrawer(line),
                    jointDrawer = new NormalJointDrawer(line)
                };
                //builder.DrawLine = false;

                return builder;
            }
        }

    }
}