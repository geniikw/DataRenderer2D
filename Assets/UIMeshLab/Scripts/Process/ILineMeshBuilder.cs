using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace geniikw.UIMeshLab {
    public interface ILineMeshBuilder {
        MeshData Build(Line line);
    }
}