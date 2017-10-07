using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.UIMeshLab
{
    public class LineBuilder 
    {
        public MeshData Build(Line line)
        {
            var md = new MeshData();
            md.vertexes = new List<Vertex>();
            md.triangles = new List<int>();
            foreach (var pair in line.PairList)
            {
                
            }
            return md;
        }
    }
}