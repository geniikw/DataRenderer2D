using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace geniikw.UIMeshLab
{

    public interface IMesh
    {
        IEnumerable<Vertex> Vertices { get; }
        IEnumerable<int> Triangles { get; }
    }

    public interface IMeshDrawer
    {
        IEnumerable<IMesh> Draw();
    }
}