using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.UIMeshLab {
    
    public interface IBezierDrawer {
        MeshData Build(Node n0, Node n1);
    }

    public interface IEndCapDrawer
    {
        MeshData Build(Vector3 position, Vector3 direction, float radian);
    }

    public interface IJointDrawer
    {
        MeshData Build(Vector3 center, Vector3 next, Vector3 prev);
    }
}