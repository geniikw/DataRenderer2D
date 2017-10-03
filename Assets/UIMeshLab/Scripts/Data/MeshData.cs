using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.UIMeshLab
{
    /// <summary>
    /// vertexes
    ///      position
    ///      normal
    ///      uv
    ///      color
    /// triangles
    /// </summary>


    public struct MeshData
    {
        public List<Vertex> vertexes;
        public List<int> triangles;
    }
}