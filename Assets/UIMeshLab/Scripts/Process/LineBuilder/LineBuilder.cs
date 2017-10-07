using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.UIMeshLab
{
    public class LineBuilder 
    {
        public IBezierDrawer bezierDrawer;
        public IJointDrawer jointDrawer;
        public ICapDrawer capDrawer;

        public MeshData Build(Line line)
        {
            var output = MeshData.Void();
            
            foreach (var pair in line.PairList)
            {
                output = output + bezierDrawer.Build(pair);
            }

            return output;
        }
    }
}