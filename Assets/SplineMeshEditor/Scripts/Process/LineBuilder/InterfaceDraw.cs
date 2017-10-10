using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.UIMeshLab {
    
    public interface IJointBuilder
    {
        MeshData Build(Spline.Triple triple);
    }

    public interface IBezierBuilder
    {
        MeshData Build(Spline.LinePair pair);
    }

    public interface ICapBuilder
    {
        MeshData Build(Spline.LinePair pair, bool isEnd);
    }
}