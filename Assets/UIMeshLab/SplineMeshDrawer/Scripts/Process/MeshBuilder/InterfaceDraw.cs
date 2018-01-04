using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.UIMeshLab {
    
    public interface IJointBuilder
    {
        void Build(Spline.Triple triple);
    }

    public interface IBezierBuilder
    {
        void Build(Spline.LinePair pair);
    }

    public interface ICapBuilder
    {
        void Build(Spline.LinePair pair, bool isEnd);
    }
}