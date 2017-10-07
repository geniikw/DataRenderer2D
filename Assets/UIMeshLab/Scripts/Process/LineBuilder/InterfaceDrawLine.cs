using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.UIMeshLab {
    
    public interface IBezierDrawer {
        MeshData Build(Line.LinePair pair);
    }

    public interface ICapDrawer
    {
        MeshData Build(Vector3 position, Vector3 direction, float radian);
    }

    public interface IJointDrawer
    {
        MeshData Build(Vector3 center, Vector3 next, Vector3 prev);
    }
}